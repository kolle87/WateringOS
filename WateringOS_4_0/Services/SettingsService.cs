using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;
using System.Collections.ObjectModel;

namespace WateringOS_4_0.Services
{
    public class SettingsService
    {
        public ObservableCollection<SettingsModel> Settings = new ObservableCollection<SettingsModel>();

        public void LoadSettings()
        {
            Settings = JsonConvert.DeserializeObject<ObservableCollection<SettingsModel>>(System.IO.File.ReadAllText(@"usrdata/systemsettings.json"));
        }
    }
}
