using Newtonsoft.Json.Serialization;
using System;
using System.Runtime.Serialization;

namespace WateringOS_4_0.Models
{
    public static class RecentValues
    {
        public static DateTime TimeStamp { get; set; }
        public static int Flow1 { get; set; }
        public static int Flow2 { get; set; }
        public static int Flow3 { get; set; }
        public static int Flow4 { get; set; }
        public static int Flow5 { get; set; }
        public static bool Pump { get; set; }
        public static bool Valve1 { get; set; }
        public static bool Valve2 { get; set; }
        public static bool Valve3 { get; set; }
        public static bool Valve4 { get; set; }
        public static bool Valve5 { get; set; }

        public static byte Rain { get; set; }
        public static double Ground { get; set; }
        public static double Pressure { get; set; }
        public static byte LevelInPercent { get; set; }
        public static byte LevelInLiter { get; set; }
        public static double TempCPU { get; set; }
        public static double TempPCB { get; set; }
        public static double TempAmb { get; set; }
        public static double TempExp { get; set; }

        public static bool PowerGood_5V { get; set; }
        public static bool PowerGood_12V { get; set; }
        public static bool PowerGood_24V { get; set; }
        public static bool PowerFail_5V { get; set; }
        public static bool PowerFail_12V { get; set; }
        public static bool PowerFail_24V { get; set; }
        public static bool WatchdogPrealarm { get; set; }
    }
}
