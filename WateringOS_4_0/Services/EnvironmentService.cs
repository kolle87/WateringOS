using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WateringOS_4_0.Services
{
    public class EnvironmentService
    {
        public static TimeSpan IntenseSunDuration { get; private set; } = new TimeSpan(0);
        public static double IntenseSunDelta { get; private set; } = 0.0;
        public static bool IntenseSun { get; private set; } = false;
        public static ObservableCollection<EnvironmentModel> EnvironmentLog = new();
        
        public static void LoadEnvironmentLog()
        {
            EnvironmentLog = JsonConvert.DeserializeObject<ObservableCollection<EnvironmentModel>>(File.ReadAllText(@"usrdata/SavedLogs/EnvironmentLog.json"));
        }
        public static void SaveEnvironmentLog()
        {
            File.WriteAllText(@"usrdata/SavedLogs/EnvironmentLog.json", JsonConvert.SerializeObject(EnvironmentLog));
        }      
        public static async  Task CleanEnvironmentLog()
        {
            while (EnvironmentLog.Count > 10080) { EnvironmentLog.RemoveAt(0); }
        }

        public static void CheckSunIntensity(double TempAmb, double TempExp)
        {
            IntenseSunDelta = TempExp - TempAmb;
            if (IntenseSunDelta > Globals.ParameterValue("SunIntense_On",10)) { IntenseSun = true; } 
            if (IntenseSunDelta < Globals.ParameterValue("SunIntense_Off",8)) { IntenseSun = false; }

            if (IntenseSun)
            {
                IntenseSunDuration = IntenseSunDuration.Add(TimeSpan.FromMilliseconds(Globals.ParameterValue("Task_Cycle",1000)));
            }
        }
        public static void ResetSunIntensityTime()
        {
            IntenseSunDuration = TimeSpan.Zero;
        }

        public static void RecordSunIntense(bool IsIntense)
        {
            string FileName = "usrdata/SunIntense.csv";
            string record = String.Format("{0},{1},{2},{3},{4}\n",DateTime.Now,RecentValues.TempAmb,RecentValues.TempExp,(RecentValues.TempExp-RecentValues.TempAmb),IsIntense);

            if (!File.Exists(FileName))
            {
                string header = "Timestamp,TempAmb,TempExp,TempDiff,IsIntense\n";
                File.WriteAllText(FileName, header);
            }

            File.AppendAllText(FileName, record);
        }
    }
}
