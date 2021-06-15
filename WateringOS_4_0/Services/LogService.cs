using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;
using System.Collections.ObjectModel;

namespace WateringOS_4_0.Services
{
    public class LogService
    {
        public ObservableCollection<LogModel> JournalLog = new ObservableCollection<LogModel>();
        
        public void LoadJournal()
        {
            JournalLog = JsonConvert.DeserializeObject<ObservableCollection<LogModel>>(System.IO.File.ReadAllText(@"usrdata/SavedLogs/JournalLog.json"));
        }
    }
}
