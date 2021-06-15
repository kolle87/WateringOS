using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace WateringOS_4_0.Models
{
    public class EnvironmentModel
    {
        public DateTime TimeStamp { get; set; }
        public byte Rain { get; set; }
        public byte Ground { get; set; }
        public int TempCPU { get; set; }
        public int TempAmb { get; set; }
        public int TempExp { get; set; }
        public int TempPCB { get; set; }

        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            Console.WriteLine(errorContext.Error.ToString());
            errorContext.Handled = true;
        }
    }
}
