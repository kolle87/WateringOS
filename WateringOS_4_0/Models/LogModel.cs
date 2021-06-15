using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace WateringOS_4_0.Models
{
    public class LogModel
    {
        public DateTime TimeStamp { get; set; }
        public string Type        { get; set; }
        public string App         { get; set; }
        public string Message     { get; set; }
        public string Details     { get; set; }
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            Console.WriteLine(errorContext.Error.ToString());
            errorContext.Handled = true;
        }
    }

    public class LogType
    {
        public const string Status = "Status";
        public const string Information = "Information";
        public const string Warning = "Warning";
        public const string Error = "Error";
        public const string Fatal = "Fatal";
    }
}
