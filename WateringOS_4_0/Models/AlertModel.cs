using System;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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
            Console.WriteLine(errorContext.Error.ToString());
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
