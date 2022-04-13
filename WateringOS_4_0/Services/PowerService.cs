using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;
using System.Collections.ObjectModel;

namespace WateringOS_4_0.Services
{
    public class PowerService    
    {
        public static ObservableCollection<PowerModel> PowerLog = new ObservableCollection<PowerModel>();
        public static event Action<ObservableCollection<PowerModel>> PushLog;
        public static event Action<PowerModel> AddToLog;

        public static void LoadPowerLog()
        {
            PowerLog = JsonConvert.DeserializeObject<ObservableCollection<PowerModel>>(File.ReadAllText(@"usrdata/SavedLogs/PowerLog.json"));
        }    
        public static void SavePowerLog()
        {
            File.WriteAllText(@"usrdata/SavedLogs/PowerLog.json", JsonConvert.SerializeObject(PowerLog));
        }
        public static void RequestLog()
        {
            PushLog?.Invoke(PowerLog);
        }
        public static async  Task CleanPowerLog()
        {
            while (PowerLog.Count > 10080) { PowerLog.RemoveAt(0); }
        }
    }
}
