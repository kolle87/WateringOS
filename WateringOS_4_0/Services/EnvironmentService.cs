using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WateringOS_4_0.Services
{
    public class EnvironmentService
    {
        public static ObservableCollection<EnvironmentModel> EnvironmentLog = new();

        public static void LoadEnvironmentLog()
        {
            EnvironmentLog = JsonConvert.DeserializeObject<ObservableCollection<EnvironmentModel>>(File.ReadAllText(@"usrdata/SavedLogs/EnvironmentLog.json"));
        }
        public static void SaveEnvironmentLog()
        {
            File.WriteAllText(@"usrdata/SavedLogs/EnvironmentLog.json", JsonConvert.SerializeObject(EnvironmentLog));
        }      
        public static async  Task CleanEnvironmentLog()
        {
            while (EnvironmentLog.Count > 10080) { EnvironmentLog.RemoveAt(0); }
        }  
    }
}
