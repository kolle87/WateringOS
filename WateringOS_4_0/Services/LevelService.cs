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
    public class LevelService
    {
        public static ObservableCollection<LevelModel> LevelLog = new();

        public static void LoadLevelLog()
        {
            LevelLog = JsonConvert.DeserializeObject<ObservableCollection<LevelModel>>(File.ReadAllText(@"usrdata/SavedLogs/LevelLog.json"));
        }
        public static void SaveLevelLog()
        {
            File.WriteAllText(@"usrdata/SavedLogs/JournalLog.json", JsonConvert.SerializeObject(LevelLog));
        }
    }
}
