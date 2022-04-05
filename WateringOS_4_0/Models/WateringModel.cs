using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WateringOS_4_0.Loggers;

namespace WateringOS_4_0.Models
{
    public class WateringModel
    {
        public DateTime TimeStamp { get; set; }
        public int Flow1 { get; set; }
        public int Flow2 { get; set; }
        public int Flow3 { get; set; }
        public int Flow4 { get; set; }
        public int Flow5 { get; set; }
        public byte Pump { get; set; }
        public byte Valve1 { get; set; }
        public byte Valve2 { get; set; }
        public byte Valve3 { get; set; }
        public byte Valve4 { get; set; }
        public byte Valve5 { get; set; }
        public double Pressure { get; set; }
        public int TempCPU { get; set; }

        public byte PowerGood_5V { get; set; }
        public byte PowerGood_12V { get; set; }
        public byte PowerGood_24V { get; set; }
        public byte PowerFail_5V { get; set; }
        public byte PowerFail_12V { get; set; }
        public byte PowerFail_24V { get; set; }
        public byte WatchdogPrealarm { get; set; }
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            Logger.Post(Logger.SYS, LogType.Error, "JSON Serialization Error in WateringModel.cs[public class WateringModel]", errorContext.Error.ToString());
            errorContext.Handled = true;
        }
    }
}
