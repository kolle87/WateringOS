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
    public class WateringLogService
    {
        public static ObservableCollection<WateringModel> WateringLog1 = new ObservableCollection<WateringModel>();
        public static ObservableCollection<WateringModel> WateringLog2 = new ObservableCollection<WateringModel>();
        public static ObservableCollection<WateringModel> WateringLog3 = new ObservableCollection<WateringModel>();
        public static event Action<ObservableCollection<WateringModel>> PushLog1;
        public static event Action<ObservableCollection<WateringModel>> PushLog2;
        public static event Action<ObservableCollection<WateringModel>> PushLog3;
        public static event Action<WateringModel> AddToLog1;

        public static void LoadWateringLogs()
        {
            WateringLog1 = JsonConvert.DeserializeObject<ObservableCollection<WateringModel>>(File.ReadAllText(@"usrdata/SavedLogs/Watering1Log.json"));
            WateringLog2 = JsonConvert.DeserializeObject<ObservableCollection<WateringModel>>(File.ReadAllText(@"usrdata/SavedLogs/Watering2Log.json"));
            WateringLog3 = JsonConvert.DeserializeObject<ObservableCollection<WateringModel>>(File.ReadAllText(@"usrdata/SavedLogs/Watering3Log.json"));
        }
        public static void SaveWateringLogs()
        {
            File.WriteAllText(@"usrdata/SavedLogs/Watering1Log.json", JsonConvert.SerializeObject(WateringLog1));
            File.WriteAllText(@"usrdata/SavedLogs/Watering2Log.json", JsonConvert.SerializeObject(WateringLog2));
            File.WriteAllText(@"usrdata/SavedLogs/Watering3log.json", JsonConvert.SerializeObject(WateringLog3));
        }
        public static void ShiftLogs()
        {
            WateringLog3 = WateringLog2;
            WateringLog2 = WateringLog1;
            WateringLog1.Clear();
            RequestLog();
        }
        public static void RequestLog()
        {
            PushLog1?.Invoke(WateringLog1);
            PushLog2?.Invoke(WateringLog2);
            PushLog3?.Invoke(WateringLog3);
        }

        public static void AddLogAndUpdate(WateringModel _WateringModel)
        {
            WateringLog1.Add(_WateringModel);
            AddToLog1?.Invoke(_WateringModel);
        }
    }
}
