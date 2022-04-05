using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WateringOS_4_0.Models
{
    public class General
    {
        public int TimeMorning { get; set; }
        public int TimeNoon { get; set; }
        public int TimeEvening { get; set; }
    }

    public class Plant
    {
        public string Name { get; set; }
        public int Volume { get; set; }
        public bool RainReduction { get; set; }
        public bool ConsiderSoil { get; set; }
        public bool Cooling { get; set; }  
        public bool Morning { get; set; }
        public bool Noon { get; set; }
        public bool Evening { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }

    public class PreferencesModel
    {
        public General General { get; set; }
        public List<Plant> Plants { get; set; }
    }

    public class Preferences
    {/*
        public static PreferencesModel Current = new PreferencesModel();
        public static void Load()
        {
            Current = JsonConvert.DeserializeObject<PreferencesModel>(System.IO.File.ReadAllText(@"usrdata/watering_settings.json"));
        }
        public static void Save()
        {
            System.IO.File.WriteAllText(@"usrdata/watering_settings.json", JsonConvert.SerializeObject(Current, Formatting.Indented));
        }
*/
    }    
}
