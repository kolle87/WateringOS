using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;
using System.Collections.ObjectModel;

namespace WateringOS_4_0.Services
{
    public class WateringLogService
    {
        public ObservableCollection<WateringModel> WateringLog1 = new ObservableCollection<WateringModel>();
        public ObservableCollection<WateringModel> WateringLog2 = new ObservableCollection<WateringModel>();
        public ObservableCollection<WateringModel> WateringLog3 = new ObservableCollection<WateringModel>();

        public void LoadWateringLog()
        {
            WateringLog1 = JsonConvert.DeserializeObject<ObservableCollection<WateringModel>>(System.IO.File.ReadAllText(@"usrdata/SavedLogs/Watering1Log.json"));
            WateringLog2 = JsonConvert.DeserializeObject<ObservableCollection<WateringModel>>(System.IO.File.ReadAllText(@"usrdata/SavedLogs/Watering2Log.json"));
            WateringLog3 = JsonConvert.DeserializeObject<ObservableCollection<WateringModel>>(System.IO.File.ReadAllText(@"usrdata/SavedLogs/Watering3Log.json"));
        }
    }
}
