using System;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WateringOS_4_0.Loggers;

namespace WateringOS_4_0.Models
{
    public class Alert
    {
        public string Id { get; set; }
        public AlertType Type { get; set; }
        public string Message { get; set; }
        public bool AutoClose { get; set; }
        public bool KeepAfterRouteChange { get; set; }
        public bool Fade { get; set; }
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            Logger.Post(Logger.SYS, LogType.Error, "JSON Serialization Error in AlertModel.cs[public class Alert]", errorContext.Error.ToString());
            errorContext.Handled = true;
        }
    }

    public enum AlertType
    {
        Success,
        Error,
        Info,
        Warning
    }
}
