using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;
using System.Collections.ObjectModel;

namespace WateringOS_4_0.Services
{
    public class PowerService    
    {
        public ObservableCollection<PowerModel> PowerLog = new ObservableCollection<PowerModel>();

        public void LoadPowerLog()
        {
            PowerLog = JsonConvert.DeserializeObject<ObservableCollection<PowerModel>>(System.IO.File.ReadAllText(@"usrdata/SavedLogs/PowerLog.json"));
        }    
    }
}
