using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace WateringOS_4_0.Models
{
    public class LevelModel
    {
        public DateTime TimeStamp { get; set; }
        public byte Tank { get; set; }
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            Console.WriteLine(errorContext.Error.ToString());
            errorContext.Handled = true;
        }
    }
}
