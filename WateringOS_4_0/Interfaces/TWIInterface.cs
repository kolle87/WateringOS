using System;
using System.Device.I2c;
using System.Threading.Tasks;
using WateringOS_4_0.Services;
using WateringOS_4_0.Loggers;

namespace WateringOS_4_0.Interfaces
{
    public class TWIInterface
    {   
        private static I2cDevice TWI_TempBoard;     // 0x48
        private static I2cDevice TWI_TempAmbient;   // 0x4F
        private static I2cDevice TWI_TempExposed;   // 0x4B
        private static I2cDevice TWI_Ground;        // 0x28
        private static I2cConnectionSettings settings1;
        private static I2cConnectionSettings settings2;
        private static I2cConnectionSettings settings3;
        private static I2cConnectionSettings settings4;
        private static bool IsBusy = false;

        public static bool IsInitialized { get; private set; } = false;
        public class Sensor
        {
            public double Value = 0;
            public bool Available = false;
            public int FailureCount = 0;
        }
        public static Sensor AmbientTemperature = new();
        public static Sensor ExposedTemperature = new();
        public static Sensor BoardTemperature = new();
        public static Sensor SoilMoisture = new();

        public void Initialize()
        {
            if (IsInitialized) {return; }
            Logger.Post(Logger.TWI, LogType.Information, "Start initialization", "The initialization of the TWI communication class has started.");
            try
            {
                int FailCount = 0;
                
                // Initialize Board Temperature Sensor (DS1621)
                try
                {
                    Logger.Post(Logger.TWI, LogType.Debug, "0x48: Loading slave", "Loading slave settings for 0x48 (PCB temperature sensor) and starting interface");
                    settings1 = new I2cConnectionSettings(1, 0x48); // CPU Temperature
                    TWI_TempBoard = I2cDevice.Create(settings1);
                    Wait(100);

                    Logger.Post(Logger.TWI, LogType.Debug, "0x48: Setting configuration register", "Setting the configuration register to continues measuring");
                    var vACh = new byte[] { 0xAC, 0x00 };    // Access config set 0x00 (continues meas)
                    TWI_TempBoard.Write(vACh);
                    Wait(100);

                    Logger.Post(Logger.TWI, LogType.Debug, "0x48: Start temperature conversion", "Sending command to start continues temperature conversion");
                    var vEEh = new byte[] { 0xEE };    // Send 0xEE for start conversion
                    TWI_TempBoard.Write(vEEh);
                    Wait(100);
                    BoardTemperature.FailureCount = 0;
                    BoardTemperature.Available = true;
                }
                catch (Exception e)
                {
                    Logger.Post(Logger.TWI, LogType.Error, "0x48: Error initializing CPU temperature sensor", e.Message);
                    FailCount++;
                }

                // Initialize Ambient Temperature Sensor (LM75)
                try
                {
                    Logger.Post(Logger.TWI, LogType.Debug, "0x4F: Loading slave", "Loading slave settings for 0x4F (ambient temperature sensor) and starting interface");
                    settings2 = new I2cConnectionSettings(1, 0x4F);   // Ambient Temperature
                    TWI_TempAmbient = I2cDevice.Create(settings2);
                    Wait(100);
                    AmbientTemperature.FailureCount = 0;
                    AmbientTemperature.Available = true;
                }
                catch (Exception e)
                {
                    Logger.Post(Logger.TWI, LogType.Error, "0x4F: Error initializing ambient temperature sensor", e.Message);
                    FailCount++;
                }

                // Initialize Exposed Temperature Sensor (LM75)
                try
                {
                    Logger.Post(Logger.TWI, LogType.Debug, "0x4B: Loading slave", "Loading slave settings for 0x4B (exposed temperature sensor) and starting interface");
                    settings3 = new I2cConnectionSettings(1, 0x4B);   // Exposed Temperature
                    TWI_TempExposed = I2cDevice.Create(settings3);
                    Wait(100);
                    ExposedTemperature.FailureCount = 0;
                    ExposedTemperature.Available = true;
                }
                catch (Exception e)
                {
                    Logger.Post(Logger.TWI, LogType.Error, "0x4B: Error initializing exposed temperature sensor", e.Message);
                    FailCount++;
                }

                // Initialize Ground Sensor (Arduino Nano)
                try
                {
                    Logger.Post(Logger.TWI, LogType.Debug, "0x56: Loading slave", "Loading slave settings for 0x56 (ground sensor) and starting interface");
                    settings4 = new I2cConnectionSettings(1, 0x56);
                    TWI_Ground = I2cDevice.Create(settings4);
                    Wait(100);
                    SoilMoisture.FailureCount = 0;
                    SoilMoisture.Available = true;
                }
                catch (Exception e) 
                { 
                    Logger.Post(Logger.TWI, LogType.Error, "0x56: Error initializing ground sensor", e.Message);
                    FailCount++;
                }

                if (FailCount > 3) throw new Exception("Neither sensor could be initialized.");

                // First read to confirm initialization 
                ReadAmbientTemp();  Wait(100);
                ReadExposedTemp();  Wait(100);
                ReadGround();       Wait(100);
                ReadBoardTemp();    Wait(100);

                Logger.Post(Logger.TWI, LogType.Information, "Initialization finished", String.Format("Initialization of TWI interface finished with {0} failures and status {1}",FailCount,IsInitialized));
                IsInitialized = true;
                AlarmService.TWI_InitialisationFail.Deactivate();
                AlarmService.TWI_InterfaceFail.Deactivate();
            }
            catch (Exception e)
            {
                Logger.Post(Logger.TWI, LogType.Error, "TWI Class Error Initializing", e.Message);
                AlarmService.TWI_InitialisationFail.Activate();
            }
        }
        public void Read()
        {
            if (IsInitialized)
            {

                CheckSensorStatus();

                ReadAmbientTemp(); Wait(100);
                
                ReadExposedTemp(); Wait(100);
                
                ReadGround(); Wait(100);
                
                ReadBoardTemp();
            }
            else
            {
                //Logger.Post(Logger.TWI, LogType.Error, "TWIRead() - Controller not initialized", "TWI Interface is not ready to read.");
            }
        }
        private void ReadBoardTemp()
        {
            var _BoardTemp = BoardTemperature.Value;
            try
            {
                if (IsBusy)
                {
                    BoardTemperature.FailureCount++;
                    Logger.Post(Logger.TWI, LogType.Warning, $"ReadBoardTemp() - TWI busy ({BoardTemperature.FailureCount})", $"ReadBoardTemp() - TWI busy \n FailureCount: {BoardTemperature.FailureCount}");
                }
                else if (!BoardTemperature.Available)
                {
                    Logger.Post(Logger.TWI, LogType.Warning, "ReadBoardTemp() - Sensor not OK", "TWI Sensor is not ready to read.");
                }
                else
                {
                    IsBusy = true;
                
                    var vASr = new byte[] { 0xAA };    // 0xAA = DS1621 read temp
                    var vASa = new byte[2];
                    TWI_TempBoard.Write(vASr);
                    TWI_TempBoard.Read(vASa);
                    _BoardTemp = ConvertTemperature(vASa[0], vASa[1]);
                    
                    IsBusy = false;
                    BoardTemperature.FailureCount = 0;
                    BoardTemperature.Available = true;
                }                
            }
            catch (Exception e)
            {
                BoardTemperature.FailureCount++;
                Logger.Post(Logger.TWI, LogType.Error, $"Error reading Board temperature ({BoardTemperature.FailureCount})", e.Message);
            }
            BoardTemperature.Value = _BoardTemp;
        }
        private void ReadAmbientTemp()
        {
            var _AmbientTemp = AmbientTemperature.Value;
            try
            {
                if (IsBusy)
                {
                    AmbientTemperature.FailureCount++;
                    Logger.Post(Logger.TWI, LogType.Warning, $"ReadAmbientTemp() - TWI busy ({AmbientTemperature.FailureCount})", $"ReadAmbientTemp() - TWI busy \n FailureCount: {AmbientTemperature.FailureCount}");
                }
                else if (!AmbientTemperature.Available)
                {
                    Logger.Post(Logger.TWI, LogType.Warning, "ReadAmbientTemp() - Sensor not OK", "TWI Sensor is not ready to read.");
                }
                else
                {
                    IsBusy = true;
                    var vASr = new byte[] { 0x00 };    // 0x00 = LM75 Read Temp
                    var vASa = new byte[2];
                    TWI_TempAmbient.Write(vASr);
                    TWI_TempAmbient.Read(vASa);
                    _AmbientTemp = ConvertTemperature(vASa[0], vASa[1]);
                    
                    IsBusy = false;
                    AmbientTemperature.FailureCount = 0;
                    AmbientTemperature.Available = true;
                }                
            }
            catch (Exception e)
            {
                AmbientTemperature.FailureCount++;
                Logger.Post(Logger.TWI ,LogType.Error, $"Error reading ambient temperature ({AmbientTemperature.FailureCount})", e.Message);
            }
            AmbientTemperature.Value = _AmbientTemp;
        }
        private void ReadExposedTemp()
        {
            var _ExposedTemp = ExposedTemperature.Value;
            try
            {
                if (IsBusy)
                {
                    ExposedTemperature.FailureCount++;
                    Logger.Post(Logger.TWI, LogType.Warning, $"ReadExposedTemp() - TWI busy ({ExposedTemperature.FailureCount})", $"ReadExposedTemp() - TWI busy \n FailureCount: {ExposedTemperature.FailureCount}");
                }
                else if (!ExposedTemperature.Available)
                {
                    Logger.Post(Logger.TWI, LogType.Warning, "ReadExposedTemp() - Sensor not OK", "TWI Sensor is not ready to read.");
                }
                else
                {
                    IsBusy = true;
                    var vASr = new byte[] { 0x00 };    // 0x00 = LM75 Read Temp
                    var vASa = new byte[2];
                    TWI_TempExposed.Write(vASr);
                    TWI_TempExposed.Read(vASa);
                    _ExposedTemp = ConvertTemperature(vASa[0], vASa[1]);

                    IsBusy = false;
                    ExposedTemperature.FailureCount = 0;
                    ExposedTemperature.Available = true;
                }                
            }
            catch (Exception e)
            {
                ExposedTemperature.FailureCount++;
                Logger.Post(Logger.TWI, LogType.Error, $"Error reading exposed temperature ({ExposedTemperature.FailureCount})", e.Message);
            }
            ExposedTemperature.Value = _ExposedTemp;
        }
        private void ReadGround()
        {
            var _Ground = SoilMoisture.Value;
            try
            {
                if (IsBusy)
                {
                    SoilMoisture.FailureCount++;
                    Logger.Post(Logger.TWI, LogType.Warning, $"ReadGround() - TWI busy ({SoilMoisture.FailureCount})", $"ReadGround() - TWI busy \n FailureCount: {SoilMoisture.FailureCount}");
                }
                else if (!SoilMoisture.Available)
                {
                    Logger.Post(Logger.TWI, LogType.Warning, "ReadGround() - Sensor not OK", "TWI Sensor is not ready to read.");
                }
                else
                {
                    IsBusy = true; 
                    var vASa = new byte[2];
                    TWI_Ground.Read(vASa);
                    _Ground = ToInt16(vASa[0], vASa[1]);
                    
                    IsBusy = false;
                    SoilMoisture.FailureCount = 0;
                    SoilMoisture.Available = true;
                }
            }
            catch (Exception e)
            {
                SoilMoisture.FailureCount++;
                Logger.Post(Logger.TWI, LogType.Error, $"Error reading ground sensor ({SoilMoisture.FailureCount})", e.Message);
            }
            SoilMoisture.Value = _Ground;
        }
        private double ConvertTemperature(int MSB, int LSB)
        {
            var temp_signed = -(MSB & 0x80) | (MSB & 0x7f);
            var half_deg = 0.5 * ((LSB & 128) >> 7);
            return (temp_signed + half_deg);
        }
        private int ToInt16(int MSB, int LSB)
        {
            return (MSB * 256 + LSB);
        }
        private void CheckSensorStatus()
        {
            if(BoardTemperature.FailureCount > 5)
            {
                BoardTemperature.Available = false;
                AlarmService.TWI_SensFail_BoardTemperatur.Activate();
            }
            else
            {
                AlarmService.TWI_SensFail_BoardTemperatur.Deactivate();
            }

            if (AmbientTemperature.FailureCount > 5)
            {
                AmbientTemperature.Available = false;
                AlarmService.TWI_SensFail_AmbientTemperatur.Activate();
            }
            else
            {
                AlarmService.TWI_SensFail_AmbientTemperatur.Deactivate();
            }
            
            if (ExposedTemperature.FailureCount > 5)
            {
                ExposedTemperature.Available = false;
                AlarmService.TWI_SensFail_ExposedTemperatur.Activate();
            }
            else
            {
                AlarmService.TWI_SensFail_ExposedTemperatur.Deactivate();
            }
            
            if (SoilMoisture.FailureCount > 5)
            {
                SoilMoisture.Available = false;
                AlarmService.TWI_SensFail_SoilMoisture.Activate();
            }
            else
            {
                AlarmService.TWI_SensFail_SoilMoisture.Deactivate();
            }
            
            if (( BoardTemperature.FailureCount + AmbientTemperature.FailureCount + ExposedTemperature.FailureCount + SoilMoisture.FailureCount) > 15)
            {
                IsInitialized = false;
                AlarmService.TWI_InterfaceFail.Activate();
            }
            else
            {
                AlarmService.TWI_InterfaceFail.Deactivate();
            }
        }
        private void Wait(int WaitingTime)
        {
            using Task t_wait = Task.Run(
                async delegate {
                    await Task.Delay(WaitingTime);
                });
            t_wait.Wait();
        }
    }
}
