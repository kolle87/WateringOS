using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WateringOS_4_0.Loggers;

namespace WateringOS_4_0.Models
{
    public class EnvironmentModel
    {
        public DateTime TimeStamp { get; set; }
        public byte Rain { get; set; }
        public double Ground { get; set; }
        public double TempCPU { get; set; }
        public double TempAmb { get; set; }
        public double TempExp { get; set; }
        public double TempPCB { get; set; }

        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            Logger.Post(Logger.SYS, LogType.Error, "JSON Serialization Error in EnvironmentModel.cs[public class EnvironmentModel]", errorContext.Error.ToString());
            errorContext.Handled = true;
        }
    }
}
