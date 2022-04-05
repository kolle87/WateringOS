using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WateringOS_4_0.Loggers;

namespace WateringOS_4_0.Models
{
    public class PowerModel
    {
        public DateTime TimeStamp { get; set; }
        public bool PowerGood_5V { get; set; }
        public bool PowerGood_12V { get; set; }
        public bool PowerGood_24V { get; set; }
        public bool PowerFail_5V { get; set; }
        public bool PowerFail_12V { get; set; }
        public bool PowerFail_24V { get; set; }
        public bool WatchdogPrealarm { get; set; }
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            Logger.Post(Logger.SYS, LogType.Error, "JSON Serialization Error in PowerModel.cs[public class PowerModel]", errorContext.Error.ToString());
            errorContext.Handled = true;
        }
    }
}