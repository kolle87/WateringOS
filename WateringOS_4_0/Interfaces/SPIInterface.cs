using System;
using System.Device.Spi;
using WateringOS_4_0.Loggers;
using WateringOS_4_0.Services;

namespace WateringOS_4_0.Interfaces
{
    public class SPIInterface
    {
        private static SpiDevice AtMega32A;
        private static SpiConnectionSettings AtMega_Settings;
        private static bool IsBusy = false;
        private static int FailureCount = 0;
        public static bool IsInitialized { get; private set; } = false;
        public static byte Flow1 { get; private set; }
        public static byte Flow2 { get; private set; }
        public static byte Flow3 { get; private set; }
        public static byte Flow4 { get; private set; }
        public static byte Flow5 { get; private set; }
        public static byte Rain { get; private set; }
        public static byte Ground { get; private set; }
        public static byte LevelRaw { get; private set; }
        public static double Pressure { get; private set; }
        
        public void Initialize()
        {
            Logger.Post(Logger.SPI, LogType.Information, "Start initialization", "The intialization of the SPI communication class has started.");

            try
            {
                IsInitialized = false;
                AtMega_Settings = new SpiConnectionSettings(0, 0); // (Bus_ID, CS_ID)
                AtMega_Settings.ClockFrequency = 10000;  // 10kHz            
                AtMega_Settings.Mode = SpiMode.Mode3;    // Mode3: CPOL = 1, CPHA = 1
                AtMega32A = SpiDevice.Create(AtMega_Settings);
                FailureCount = 0;
                AlarmService.SPI_InitialisationFail.Deactivate();
                AlarmService.SPI_InterfaceFail.Deactivate();
                IsInitialized = true;
            }
            catch (Exception e)
            {
                IsInitialized = false;
                Logger.Post(Logger.SPI, LogType.Error, "Class Erro SPI Initializing", e.Message);
                AlarmService.SPI_InitialisationFail.Activate();
            }
        }
        public void Read()
        {
            if (IsBusy)
            {
                Logger.Post(Logger.SPI, LogType.Warning, "Read() - SPI busy", "Read() - SPI busy");
            }
            else if (!IsInitialized)
            {
                Logger.Post(Logger.SPI, LogType.Warning, "Read() - SPI Interface is not initialized", "SPI Interface is not ready to read.");
            }
            else
            {
                IsBusy = true;
                byte[] ReadBuf = new byte[13];
                byte[] ReqData = new byte[] { 0x55, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0xFF };
                try
                {
                    AtMega32A.TransferFullDuplex(ReqData, ReadBuf);
                    byte vCRC = CRC8.ComputeChecksum(ReadBuf[2], ReadBuf[3], ReadBuf[4], ReadBuf[5], ReadBuf[6], ReadBuf[7], ReadBuf[8], ReadBuf[9], ReadBuf[10]);

                    if (ReadBuf[12] == vCRC)
                    {
                        Flow1 = ReadBuf[2];
                        Flow2 = ReadBuf[3];
                        Flow3 = ReadBuf[4];
                        Flow4 = ReadBuf[5];
                        Flow5 = ReadBuf[6];
                        Rain = ReadBuf[7];
                        LevelRaw = ReadBuf[8];
                        Pressure = ReadBuf[9] * 0.049;
                        Ground = ReadBuf[10];

                        FailureCount--;
                    }
                    else
                    {
                        Logger.Post(Logger.SPI, LogType.Error, "CRC failure", "The calculated CRC was not corresponding to the received one.");
                    }
                    
                }
                catch (Exception e)
                {
                    Logger.Post(Logger.SPI, LogType.Error, "Error reading SPI data", e.Message);
                    FailureCount++;
                }
                if (FailureCount > 5)
                {
                    IsInitialized = false;
                    Logger.Post(Logger.SPI, LogType.Error, "Failure count high - deinitialized", "Several read attemps failed. SPI Controller has been deinitialized.");
                    AlarmService.SPI_InterfaceFail.Activate();
                }
                IsBusy = false;
            }
        }
        public void ResetFlow()
        {
            if (IsBusy)
            {
                Logger.Post(Logger.SPI, LogType.Warning, "ResetFlow() - SPI busy", "Read() - SPI busy");
            }
            else if (!IsInitialized)
            {
                Logger.Post(Logger.SPI, LogType.Warning, "ResetFlow() - SPI Interface is not initialized", "SPI Interface is not ready to read.");
            }
            else
            {
                IsBusy = true;
                byte[] SendCmd = new byte[3];
                byte[] AckData = new byte[3];

                try
                {
                    SendCmd[0] = 0x11;
                    AtMega32A.TransferFullDuplex(SendCmd, AckData);
                    if (AckData[2] == 0x06)
                    {
                        FailureCount--;
                    }
                    else
                    {
                        Logger.Post(Logger.SPI, LogType.Error, "Not received acknowledge", "The command was not acknoledged by the slave.");
                    }
                }
                catch (Exception e)
                {
                    Logger.Post(Logger.SPI, LogType.Error, "Error sending Atmega reset command", e.Message);
                    FailureCount++;
                }
            }
        }

        // CRC Calculation Helper
        public sealed class CRC8
        {
            static byte[] table = new byte[256];
            const byte poly = 0x07;

            public static byte ComputeChecksum(params byte[] bytes)
            {
                byte crc = 0xFF;
                if (bytes != null && bytes.Length > 0)
                {
                    foreach (byte b in bytes)
                    {
                        crc = table[crc ^ b];
                    }
                }
                return crc;
            }

            static CRC8()
            {
                for (int i = 0; i < 256; i++)
                {
                    int temp = i;
                    for (int j = 0; j < 8; j++)
                    {
                        if ((temp & 0x80) != 0)
                        {
                            temp <<= 1;
                            temp ^= poly;
                        }
                        else
                        {
                            temp <<= 1;
                        }
                    }
                    table[i] = (byte)temp;
                }
            }
        }
    }
}
