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
    public class JournalService
    {
        public static ObservableCollection<LogModel> JournalLog = new ();
        public event Action<ObservableCollection<LogModel>> PushJournal;
        public event Action<LogModel> AddToJournal;
        
        public static void LoadJournal()
        {
            JournalLog = JsonConvert.DeserializeObject<ObservableCollection<LogModel>>(File.ReadAllText(@"usrdata/SavedLogs/JournalLog.json"));
        }
        public static void SaveJournal()
        {
            File.WriteAllText( @"usrdata/SavedLogs/JournalLog.json", JsonConvert.SerializeObject(JournalLog));
        }
        public void RequestJournal()
        {
            PushJournal?.Invoke(JournalLog);
        }
        public void AddLogAndUpdate(LogModel _LogModel)
        {
            JournalLog.Add(_LogModel);
            AddToJournal?.Invoke(_LogModel);
        }
        public static async  Task CleanJournalLog()
        {
            while (JournalLog.Count > 800) { JournalLog.RemoveAt(0); }
        }
    }
}
