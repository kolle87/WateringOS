using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WateringOS_4_0.Loggers;

namespace WateringOS_4_0.Models
{
    public class LogModel
    {
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }
        public string App { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            Logger.Post(Logger.SYS, LogType.Error, "JSON Serialization Error in LogModel.cs[public class LogModel]", errorContext.Error.ToString());
            errorContext.Handled = true;
        }
    }
}
