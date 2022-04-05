using System;
using Prometheus;
using WateringOS_4_0.Models;

namespace WateringOS_4_0.Services
{
    public class PrometheusService
    {
        private static readonly GaugeConfiguration GaugeSuppressInitialValue = new() { SuppressInitialValue = true };
        private static readonly HistogramConfiguration HistogramSuppressInitialValue = new() { SuppressInitialValue = true };

        public static readonly Histogram Duration_UpdateRecents = Metrics.CreateHistogram("watering_sys_taskduration_updaterecents", "Records the task duration of UpdateRecents()", HistogramSuppressInitialValue);
        public static readonly Histogram Duration_MainTask = Metrics.CreateHistogram("watering_sys_taskduration_maintask", "Records the task duration of MainTask()", HistogramSuppressInitialValue);
        public static readonly Histogram Duration_FastTask = Metrics.CreateHistogram("watering_system_taskduration_fasttask", "Records the task duration of FastTask()", HistogramSuppressInitialValue);
        public static readonly Histogram Duration_EnvSave = Metrics.CreateHistogram("watering_system_taskduration_envlogsave", "Records the duration taken to store the Environment Log", HistogramSuppressInitialValue);
        public static readonly Histogram Duration_LevelSave = Metrics.CreateHistogram("watering_system_taskduration_levellogsave", "Records the duration taken to store the Level Log", HistogramSuppressInitialValue);
        public static readonly Histogram Duration_JournalSave = Metrics.CreateHistogram("watering_system_taskduration_journalsave", "Records the duration taken to store the Journal", HistogramSuppressInitialValue);
        public static readonly Histogram Duration_PowerSave = Metrics.CreateHistogram("watering_system_taskduration_powerlogsave", "Records the duration taken to store the Power Log", HistogramSuppressInitialValue);
        public static readonly Histogram Duration_WateringSave = Metrics.CreateHistogram("watering_system_taskduration_wateringlogsave", "Records the duration taken to store the Watering Log", HistogramSuppressInitialValue);

        private static readonly Gauge Flow1 = Metrics.CreateGauge("watering_wat_flow1", "Flow meter #1 readings", GaugeSuppressInitialValue);
        private static readonly Gauge Flow2 = Metrics.CreateGauge("watering_wat_flow2", "Flow meter #2 readings", GaugeSuppressInitialValue);
        private static readonly Gauge Flow3 = Metrics.CreateGauge("watering_wat_flow3", "Flow meter #3 readings", GaugeSuppressInitialValue);
        private static readonly Gauge Flow4 = Metrics.CreateGauge("watering_wat_flow4", "Flow meter #4 readings", GaugeSuppressInitialValue);
        private static readonly Gauge Flow5 = Metrics.CreateGauge("watering_wat_flow5", "Flow meter #5 readings", GaugeSuppressInitialValue);
        private static readonly Gauge Pressure = Metrics.CreateGauge("watering_wat_pressure", "Watering Pressure", GaugeSuppressInitialValue);
        private static readonly Gauge LevelInLiters = Metrics.CreateGauge("watering_wat_level_liter", "Tank Level in Liters", GaugeSuppressInitialValue);
        private static readonly Gauge LevelInPercent = Metrics.CreateGauge("watering_wat_level_perc", "Tank Level in Percent", GaugeSuppressInitialValue);

        private static readonly Gauge Pump   = Metrics.CreateGauge("watering_dio_pump",   "Pump Status", GaugeSuppressInitialValue);
        private static readonly Gauge Valve1 = Metrics.CreateGauge("watering_dio_valve1", "Valve #1 status 0=CLOSE 1=OPEN", GaugeSuppressInitialValue);
        private static readonly Gauge Valve2 = Metrics.CreateGauge("watering_dio_valve2", "Valve #2 status 0=CLOSE 1=OPEN", GaugeSuppressInitialValue);
        private static readonly Gauge Valve3 = Metrics.CreateGauge("watering_dio_valve3", "Valve #3 status 0=CLOSE 1=OPEN", GaugeSuppressInitialValue);
        private static readonly Gauge Valve4 = Metrics.CreateGauge("watering_dio_valve4", "Valve #4 status 0=CLOSE 1=OPEN", GaugeSuppressInitialValue);
        private static readonly Gauge Valve5 = Metrics.CreateGauge("watering_dio_valve5", "Valve #5 status 0=CLOSE 1=OPEN", GaugeSuppressInitialValue);

        private static readonly Gauge AmbientTemperature = Metrics.CreateGauge("watering_env_tempamb","Ambient Temperature", GaugeSuppressInitialValue);
        private static readonly Gauge ExposedTemperature = Metrics.CreateGauge("watering_env_tempexp", "Exposed Temperature", GaugeSuppressInitialValue);
        private static readonly Gauge BoardTemperature   = Metrics.CreateGauge("watering_env_temppcb", "Board Temperature", GaugeSuppressInitialValue);
        private static readonly Gauge Rain               = Metrics.CreateGauge("watering_env_rain", "Rain Sensor", GaugeSuppressInitialValue);
        private static readonly Gauge Ground             = Metrics.CreateGauge("watering_env_ground", "Soil Moisture Sensor", GaugeSuppressInitialValue);

        private static readonly Gauge CPUTemperature = Metrics.CreateGauge("watering_sys_tempcpu", "CPU Temperature", GaugeSuppressInitialValue);
        private static readonly Gauge PowerGood_5V = Metrics.CreateGauge("watering_sys_pg5", "5V not LOW", GaugeSuppressInitialValue);
        private static readonly Gauge PowerGood_12V = Metrics.CreateGauge("watering_sys_pg12", "12V not LOW", GaugeSuppressInitialValue);
        private static readonly Gauge PowerGood_24V = Metrics.CreateGauge("watering_sys_pg24", "24V not LOW", GaugeSuppressInitialValue);
        private static readonly Gauge PowerFail_5V = Metrics.CreateGauge("watering_sys_pf5", "5V power supply failure", GaugeSuppressInitialValue);
        private static readonly Gauge PowerFail_12V = Metrics.CreateGauge("watering_sys_pf5", "12V power supply failure", GaugeSuppressInitialValue);
        private static readonly Gauge PowerFail_24V = Metrics.CreateGauge("watering_sys_pf5", "24V power supply failure", GaugeSuppressInitialValue);
        private static readonly Gauge WatchdogPrewarn = Metrics.CreateGauge("watering_sys_wd", "Watchdog reports pre-warning", GaugeSuppressInitialValue);

        public static void UpdateMetrics()
        {
            Flow1.Set(RecentValues.Flow1);
            Flow2.Set(RecentValues.Flow2);
            Flow3.Set(RecentValues.Flow3);
            Flow4.Set(RecentValues.Flow4);
            Flow5.Set(RecentValues.Flow5);
            Pressure.Set(RecentValues.Pressure);
            LevelInLiters.Set(RecentValues.LevelInLiter);
            LevelInPercent.Set(RecentValues.LevelInPercent);

            Pump.Set(RecentValues.Pump ? 1 : 0);
            Valve1.Set(RecentValues.Valve1 ? 1:0);
            Valve2.Set(RecentValues.Valve2 ? 1:0);
            Valve3.Set(RecentValues.Valve3 ? 1:0);
            Valve4.Set(RecentValues.Valve4 ? 1 : 0);
            Valve5.Set(RecentValues.Valve5 ? 1 : 0);

            AmbientTemperature.Set(RecentValues.TempAmb);
            ExposedTemperature.Set(RecentValues.TempExp);
            BoardTemperature.Set(RecentValues.TempPCB);
            Rain.Set(RecentValues.Rain);
            Ground.Set(RecentValues.Ground);

            CPUTemperature.Set(RecentValues.TempCPU);
            PowerGood_5V.Set(RecentValues.PowerGood_5V ? 1 : 0);
            PowerGood_12V.Set(RecentValues.PowerGood_12V ? 1 : 0);
            PowerGood_24V.Set(RecentValues.PowerGood_24V ? 1 : 0);
            PowerFail_5V.Set(RecentValues.PowerFail_5V ? 1 : 0);
            PowerFail_12V.Set(RecentValues.PowerFail_12V ? 1 : 0);
            PowerFail_24V.Set(RecentValues.PowerFail_24V ? 1 : 0);
            WatchdogPrewarn.Set(RecentValues.WatchdogPrealarm ? 1 : 0);
        }
    }
}
