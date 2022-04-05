using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace WateringOS_4_0.Models
{
    public class SettingsModel
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public int Value { get; set; }
        public int Default { get; set; }
        public string Unit { get; set; }
        public int Scale { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Step { get; set; }
    }

    public class Settings
    {/*
        public static ObservableCollection<SettingsModel> Current = new ObservableCollection<SettingsModel>();
        public static void Load()
        {
            Current = JsonConvert.DeserializeObject<ObservableCollection<SettingsModel>>(System.IO.File.ReadAllText(@"usrdata/systemsettings.json"));
        }
        public static void Save()
        {
            System.IO.File.WriteAllText(@"usrdata/systemsettings.json", JsonConvert.SerializeObject(Current, Formatting.Indented));
        }
*/
    }    
}
