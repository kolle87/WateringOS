using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace WateringOS_4_0.Models
{
    public class PowerModel
    {
        public DateTime TimeStamp { get; set; }
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
            Console.WriteLine(errorContext.Error.ToString());
            errorContext.Handled = true;
        }
    }
}