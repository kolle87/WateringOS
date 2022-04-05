using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Iot.Device.CpuTemperature;
using WateringOS_4_0.Models;
using WateringOS_4_0.Interfaces;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace WateringOS_4_0
{
    public class Globals
    {
        #region Helpers
        public static void UpdateRecents()
        {
            using (Services.PrometheusService.Duration_UpdateRecents.NewTimer())
            {
                RecentValues.TimeStamp = DateTime.Now;
                RecentValues.Pump = DIOInterface.PumpActive;
                RecentValues.Valve1 = DIOInterface.Valve1Open;
                RecentValues.Valve2 = DIOInterface.Valve2Open;
                RecentValues.Valve3 = DIOInterface.Valve3Open;
                RecentValues.Valve4 = DIOInterface.Valve4Open;
                RecentValues.Valve5 = DIOInterface.Valve5Open;
                RecentValues.PowerGood_5V = DIOInterface.PowerGood_5V;
                RecentValues.PowerGood_12V = DIOInterface.PowerGood_12V;
                RecentValues.PowerGood_24V = DIOInterface.PowerGood_24V;
                RecentValues.PowerFail_5V = DIOInterface.PowerFail_5V;
                RecentValues.PowerFail_12V = DIOInterface.PowerFail_12V;
                RecentValues.PowerFail_24V = DIOInterface.PowerFail_24V;
                RecentValues.WatchdogPrealarm = DIOInterface.WatchDog_Prewarn;
                RecentValues.Flow1 = SPIInterface.Flow1;
                RecentValues.Flow2 = SPIInterface.Flow2;
                RecentValues.Flow3 = SPIInterface.Flow3;
                RecentValues.Flow4 = SPIInterface.Flow4;
                RecentValues.Flow5 = SPIInterface.Flow5;
                RecentValues.LevelInPercent = CalculatePercentage(
                    SPIInterface.LevelRaw,
                    ParameterValue("Tank_Min", 100),
                    ParameterValue("Tank_Max", 1023)
                );
                RecentValues.LevelInLiter = (byte)CalculateScale(
                    ParameterValue("Tank_Volume", 80),
                    RecentValues.LevelInPercent
                );
                RecentValues.Pressure = SPIInterface.Pressure;
                RecentValues.Rain = CheckSignalValidityByte(RecentValues.Rain, SPIInterface.Rain, (byte)10);
                RecentValues.Ground = CheckSignalValidityDouble(RecentValues.Ground, TWIInterface.SoilMoisture.Value, (double)50);
                RecentValues.TempAmb = CheckSignalValidityDouble(RecentValues.TempAmb, TWIInterface.AmbientTemperature.Value, (double)5);
                RecentValues.TempExp = CheckSignalValidityDouble(RecentValues.TempExp, TWIInterface.ExposedTemperature.Value, (double)5);
                RecentValues.TempPCB = CheckSignalValidityDouble(RecentValues.TempPCB, TWIInterface.BoardTemperature.Value, (double)5);
                RecentValues.TempCPU = CPUTemperature();
            };
        }
        public static byte CheckSignalValidityByte(byte PreviousValue, byte NewValue, byte MaxDeviation)
        {
            if ((NewValue < (PreviousValue-MaxDeviation)) || (NewValue > (PreviousValue+MaxDeviation)))
            {
                return PreviousValue;
            }
            else
            {
                return NewValue;
            }
        }
        public static int CheckSignalValidityInt(int PreviousValue, int NewValue, int MaxDeviation)
        {
            if ((NewValue < (PreviousValue - MaxDeviation)) || (NewValue > (PreviousValue + MaxDeviation)))
            {
                return PreviousValue;
            }
            else
            {
                return NewValue;
            }
        }
        public static double CheckSignalValidityDouble(double PreviousValue, double NewValue, double MaxDeviation)
        {
            if ((NewValue < (PreviousValue - MaxDeviation)) || (NewValue > (PreviousValue + MaxDeviation)))
            {
                return PreviousValue;
            }
            else
            {
                return NewValue;
            }
        }
        public static byte CalculatePercentage(int Value, int Min, int Max)
        {
            if (Value < Min)
            {
                return 0;
            }
            else
            {
            double z = (Value - Min)*100;
            double n = Max - Min;
            return (byte)Math.Floor(z/n);
            }
            
        }
        public static int CalculateScale(int Value, int Percentage)
        {
            double v = Value * Percentage;
            return (byte)Math.Floor(v / 100);
        }
        #endregion
        // --------------------------------------------------------------------------------
        #region Interfaces
        public static class Interface
        {
            public static TWIInterface TWI = new();
            public static SPIInterface SPI = new();
            public static DIOInterface DIO = new();
            public static void Initialize()
            {
                TWI.Initialize();
                SPI.Initialize();
                DIO.Initialize();
            }
        }
        
        #endregion
        // --------------------------------------------------------------------------------
        #region GUI
        public class GUI
        {
            public static event Action HasChanged_fast;
            public static event Action HasChanged_slow;
            public static void Refresh_slow(object sender, ElapsedEventArgs args)
            {
                Server.RefreshUptime();
                if (HasChanged_slow != null) { HasChanged_slow?.Invoke(); }
            }
            public static void Refresh_fast(object sender, ElapsedEventArgs args)
            {
                if (HasChanged_fast != null) { HasChanged_fast?.Invoke(); }
            }
        }
        #endregion
        // --------------------------------------------------------------------------------
        #region Scheduler
        public static class Schedulers
		{
            public static Timer Fast = new(200);      // Default: 200ms
            public static Timer Main = new(1000);     // Default: 1000 ms
            public static Timer SaveCycle = new(1200000);
            public static Timer LogLevel = new(1200000);
            public static Timer LogEnvironment = new(1200000);
            public static Timer LogPower = new(1200000);

            public static void Initialize()
            {
                // Main Task
                Schedulers.Main.Interval = ParameterValue("Task_Cycle",1000);
                Schedulers.Main.Elapsed += new ElapsedEventHandler(Routines.MainTask);
                Schedulers.Main.Elapsed += new ElapsedEventHandler(GUI.Refresh_slow);
                Schedulers.Main.Enabled = true;

                // Fast Task
                Schedulers.Fast.Interval = ParameterValue("Fast_Cycle",200);
                Schedulers.Fast.Elapsed += new ElapsedEventHandler(Routines.FastTask);
                Schedulers.Fast.Elapsed += new ElapsedEventHandler(GUI.Refresh_fast);
                Schedulers.Fast.Enabled = true;

                // Save Cycle
                Schedulers.SaveCycle.Interval = ParameterValue("Save_Cycle", 20)*60000;
                Schedulers.SaveCycle.Elapsed += new ElapsedEventHandler(Routines.SaveCycle);
                Schedulers.SaveCycle.Enabled = true;

                // Log Level
                Schedulers.LogLevel.Interval = ParameterValue("Log_Level", 20)*60000;
                Schedulers.LogLevel.Elapsed += new ElapsedEventHandler(Routines.LogLevel);
                Schedulers.LogLevel.Enabled = true;

                // Log Environment
                Schedulers.LogEnvironment.Interval = ParameterValue("Log_Environment", 20)*60000;
                Schedulers.LogEnvironment.Elapsed += new ElapsedEventHandler(Routines.LogEnvironment);
                Schedulers.LogEnvironment.Enabled = true;

                // Log Power
                Schedulers.LogPower.Interval = ParameterValue("Log_Power", 20)*60000;
                Schedulers.LogPower.Elapsed += new ElapsedEventHandler(Routines.LogPower);
                Schedulers.LogPower.Enabled = true;
            }
        }
        
        #endregion
        // --------------------------------------------------------------------------------
        #region General
        public class Version
		{
            public static int Major { get; set; } = 4;
            public static int Minor { get; set; } = 0;
            public static int Build { get; set; } = 1;
            public static string Target { get; set; } = "Debug";
            public static string Display { get; set; } = "4.0.1(Debug)";
            public static void Load()
            {
                var lines = File.ReadAllLines(@"./version.dat");
                Version.Major = int.Parse(lines[0]);
                Version.Minor = int.Parse(lines[1]);
                Version.Build = int.Parse(lines[2]);
                Version.Target = lines[3];
                Version.Display = lines[0] + "." + lines[1] + "." + lines[2] + "(" + lines[3] + ")";
            }
		}
        public class Server 
        {
            public static IHost Host;
            public static DateTime ServerStartTime = DateTime.Now;
            public static string Uptime { get; set; } = "--d --:--:--";
            public static void RefreshUptime()
            {
                TimeSpan vUptime = DateTime.Now - Server.ServerStartTime;
                int vDays = vUptime.Days;
                int vHours = vUptime.Hours;
                int vMinutes = vUptime.Minutes;
                int vSeconds = vUptime.Seconds;
                Server.Uptime = vDays + "d " + vHours.ToString("00") + ":" + vMinutes.ToString("00") + ":" + vSeconds.ToString("00");
            }
        }
        public static double CPUTemperature()
        {
            CpuTemperature temperature = new CpuTemperature();
            if (temperature.IsAvailable)
            {
                return temperature.Temperature.DegreesCelsius;
            }
            else
            { return -10; }
        }
        #endregion
        // --------------------------------------------------------------------------------
        #region System Settings
        public static ObservableCollection<SettingsModel> SystemSettings = new();
        public static void LoadSettings()
        {
            SystemSettings = JsonConvert.DeserializeObject<ObservableCollection<SettingsModel>>(File.ReadAllText(@"usrdata/systemsettings.json"));
        }
        public static void SaveSettings()
        {
            File.WriteAllText(@"usrdata/systemsettings.json", JsonConvert.SerializeObject(SystemSettings, Formatting.Indented));
        }
        public static int ParameterValue(string ParameterName, int DefaultIfNotFound)
        {
            int val = DefaultIfNotFound;
            try { val = SystemSettings.First(x => x.Name == ParameterName).Value; }
            catch { Loggers.Logger.Post(Loggers.Logger.SYS, Loggers.LogType.Fatal, "SystemSettings - Parameter not found", "Requested Parameter Value could not be found in SystemSettings"); }
            return val;
        }
        #endregion
        // --------------------------------------------------------------------------------
        #region Preferences
        public static PreferencesModel Preferences = new();
        public static void LoadPreferences()
        {
            Preferences = JsonConvert.DeserializeObject<PreferencesModel>(File.ReadAllText(@"usrdata/watering_settings.json"));
        }
        public static void SavePreferences()
        {
            File.WriteAllText(@"usrdata/watering_settings.json", JsonConvert.SerializeObject(Preferences, Formatting.Indented));
        }
        #endregion
        // --------------------------------------------------------------------------------
    }
}
