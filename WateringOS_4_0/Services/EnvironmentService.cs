using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;
using System.Collections.ObjectModel;

namespace WateringOS_4_0.Services
{
    public class EnvironmentService
    {
        public ObservableCollection<EnvironmentModel> EnvironmentLog = new ObservableCollection<EnvironmentModel>();

        public void LoadEnvironmentLog()
        {
            EnvironmentLog = JsonConvert.DeserializeObject<ObservableCollection<EnvironmentModel>>(System.IO.File.ReadAllText(@"usrdata/SavedLogs/EnvironmentLog.json"));
        }
    }
}
