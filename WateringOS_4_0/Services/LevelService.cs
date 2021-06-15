using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;
using System.Collections.ObjectModel;

namespace WateringOS_4_0.Services
{
    public class LevelService
    {
        public ObservableCollection<LevelModel> LevelLog = new ObservableCollection<LevelModel>();

        public void LoadLevelLog()
        {
            LevelLog = JsonConvert.DeserializeObject<ObservableCollection<LevelModel>>(System.IO.File.ReadAllText(@"usrdata/SavedLogs/LevelLog.json"));
        }
    }
}
