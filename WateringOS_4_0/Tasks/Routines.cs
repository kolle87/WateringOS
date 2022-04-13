using Prometheus;
using System;
using System.Linq;
using System.Timers;
using WateringOS_4_0.Loggers;
using WateringOS_4_0.Models;
using WateringOS_4_0.Services;

namespace WateringOS_4_0
{
	public class Routines
	{
		public static void MainTask(object sender, ElapsedEventArgs e)
		{
			using (PrometheusService.Duration_ReadTWI.NewTimer())
			{
				Globals.Interface.TWI.Read();
			}
			
			// Main task for watering
            #region Morning
            if ((DateTime.Now.Hour == Globals.Preferences.General.TimeMorning) && (DateTime.Now.Minute == 0) && (DateTime.Now.Second == 0))
            {
                Logger.Post(Logger.WAT, LogType.Information, "Watering trigger morning", "The routine on mornings for watering of plants was triggered.");
                Watering.DoWatering((Globals.Preferences.Plants[0].Monday && Globals.Preferences.Plants[0].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[1].Monday && Globals.Preferences.Plants[1].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[2].Monday && Globals.Preferences.Plants[2].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[3].Monday && Globals.Preferences.Plants[3].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[4].Monday && Globals.Preferences.Plants[4].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Tuesday && Globals.Preferences.Plants[0].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[1].Tuesday && Globals.Preferences.Plants[1].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[2].Tuesday && Globals.Preferences.Plants[2].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[3].Tuesday && Globals.Preferences.Plants[3].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[4].Tuesday && Globals.Preferences.Plants[4].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Wednesday && Globals.Preferences.Plants[0].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[1].Wednesday && Globals.Preferences.Plants[1].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[2].Wednesday && Globals.Preferences.Plants[2].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[3].Wednesday && Globals.Preferences.Plants[3].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[4].Wednesday && Globals.Preferences.Plants[4].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Thursday && Globals.Preferences.Plants[0].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[1].Thursday && Globals.Preferences.Plants[1].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[2].Thursday && Globals.Preferences.Plants[2].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[3].Thursday && Globals.Preferences.Plants[3].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[4].Thursday && Globals.Preferences.Plants[4].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Friday && Globals.Preferences.Plants[0].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[1].Friday && Globals.Preferences.Plants[1].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[2].Friday && Globals.Preferences.Plants[2].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[3].Friday && Globals.Preferences.Plants[3].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[4].Friday && Globals.Preferences.Plants[4].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Saturday && Globals.Preferences.Plants[0].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[1].Saturday && Globals.Preferences.Plants[1].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[2].Saturday && Globals.Preferences.Plants[2].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[3].Saturday && Globals.Preferences.Plants[3].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[4].Saturday && Globals.Preferences.Plants[4].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Sunday && Globals.Preferences.Plants[0].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[1].Sunday && Globals.Preferences.Plants[1].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[2].Sunday && Globals.Preferences.Plants[2].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[3].Sunday && Globals.Preferences.Plants[3].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[4].Sunday && Globals.Preferences.Plants[4].Morning) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
									);
            }
            #endregion Morning
            #region Noon
            if ((DateTime.Now.Hour == Globals.Preferences.General.TimeNoon) && (DateTime.Now.Minute == 0) && (DateTime.Now.Second == 0))
            {
                Logger.Post(Logger.WAT, LogType.Information, "Watering trigger noon time", "The routine on noon time for watering of plants was triggered.");
                Watering.DoWatering((Globals.Preferences.Plants[0].Monday && Globals.Preferences.Plants[0].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[1].Monday && Globals.Preferences.Plants[1].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[2].Monday && Globals.Preferences.Plants[2].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[3].Monday && Globals.Preferences.Plants[3].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[4].Monday && Globals.Preferences.Plants[4].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Tuesday && Globals.Preferences.Plants[0].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[1].Tuesday && Globals.Preferences.Plants[1].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[2].Tuesday && Globals.Preferences.Plants[2].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[3].Tuesday && Globals.Preferences.Plants[3].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[4].Tuesday && Globals.Preferences.Plants[4].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Wednesday && Globals.Preferences.Plants[0].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[1].Wednesday && Globals.Preferences.Plants[1].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[2].Wednesday && Globals.Preferences.Plants[2].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[3].Wednesday && Globals.Preferences.Plants[3].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[4].Wednesday && Globals.Preferences.Plants[4].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Thursday && Globals.Preferences.Plants[0].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[1].Thursday && Globals.Preferences.Plants[1].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[2].Thursday && Globals.Preferences.Plants[2].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[3].Thursday && Globals.Preferences.Plants[3].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[4].Thursday && Globals.Preferences.Plants[4].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Friday && Globals.Preferences.Plants[0].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[1].Friday && Globals.Preferences.Plants[1].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[2].Friday && Globals.Preferences.Plants[2].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[3].Friday && Globals.Preferences.Plants[3].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[4].Friday && Globals.Preferences.Plants[4].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Saturday && Globals.Preferences.Plants[0].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[1].Saturday && Globals.Preferences.Plants[1].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[2].Saturday && Globals.Preferences.Plants[2].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[3].Saturday && Globals.Preferences.Plants[3].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[4].Saturday && Globals.Preferences.Plants[4].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Sunday && Globals.Preferences.Plants[0].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[1].Sunday && Globals.Preferences.Plants[1].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[2].Sunday && Globals.Preferences.Plants[2].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[3].Sunday && Globals.Preferences.Plants[3].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[4].Sunday && Globals.Preferences.Plants[4].Noon) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
									);
            }
            #endregion Noon
            #region Evening
            if ((DateTime.Now.Hour == Globals.Preferences.General.TimeEvening) && (DateTime.Now.Minute == 0) && (DateTime.Now.Second == 0))
            {
                Logger.Post(Logger.WAT, LogType.Information, "Watering trigger evening", "The routine on evening for watering of plants was triggered.");
                Watering.DoWatering((Globals.Preferences.Plants[0].Monday && Globals.Preferences.Plants[0].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[1].Monday && Globals.Preferences.Plants[1].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[2].Monday && Globals.Preferences.Plants[2].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[3].Monday && Globals.Preferences.Plants[3].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday),
									(Globals.Preferences.Plants[4].Monday && Globals.Preferences.Plants[4].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Tuesday && Globals.Preferences.Plants[0].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[1].Tuesday && Globals.Preferences.Plants[1].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[2].Tuesday && Globals.Preferences.Plants[2].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[3].Tuesday && Globals.Preferences.Plants[3].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday),
									(Globals.Preferences.Plants[4].Tuesday && Globals.Preferences.Plants[4].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Wednesday && Globals.Preferences.Plants[0].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[1].Wednesday && Globals.Preferences.Plants[1].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[2].Wednesday && Globals.Preferences.Plants[2].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[3].Wednesday && Globals.Preferences.Plants[3].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday),
									(Globals.Preferences.Plants[4].Wednesday && Globals.Preferences.Plants[4].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Thursday && Globals.Preferences.Plants[0].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[1].Thursday && Globals.Preferences.Plants[1].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[2].Thursday && Globals.Preferences.Plants[2].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[3].Thursday && Globals.Preferences.Plants[3].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday),
									(Globals.Preferences.Plants[4].Thursday && Globals.Preferences.Plants[4].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Friday && Globals.Preferences.Plants[0].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[1].Friday && Globals.Preferences.Plants[1].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[2].Friday && Globals.Preferences.Plants[2].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[3].Friday && Globals.Preferences.Plants[3].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday),
									(Globals.Preferences.Plants[4].Friday && Globals.Preferences.Plants[4].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Saturday && Globals.Preferences.Plants[0].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[1].Saturday && Globals.Preferences.Plants[1].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[2].Saturday && Globals.Preferences.Plants[2].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[3].Saturday && Globals.Preferences.Plants[3].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday),
									(Globals.Preferences.Plants[4].Saturday && Globals.Preferences.Plants[4].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
									);
                
                Watering.DoWatering((Globals.Preferences.Plants[0].Sunday && Globals.Preferences.Plants[0].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[1].Sunday && Globals.Preferences.Plants[1].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[2].Sunday && Globals.Preferences.Plants[2].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[3].Sunday && Globals.Preferences.Plants[3].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday),
									(Globals.Preferences.Plants[4].Sunday && Globals.Preferences.Plants[4].Evening) && (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
									);
            }
            #endregion Evening
			Globals.GUI.Refresh_slow();
			PrometheusService.UpdateMetrics();
		}
		public static void FastTask(object sender, ElapsedEventArgs e)
		{
			using (PrometheusService.Duration_ReadSPI.NewTimer())
			{
				Globals.Interface.SPI.Read();
			}

			if (Watering.IsActive)
			{
				WateringLogService.WateringLogTmp.Add(new WateringModel
				{
					TimeStamp = RecentValues.TimeStamp,
					Flow1 = RecentValues.Flow1,
					Flow2 = RecentValues.Flow2,
					Flow3 = RecentValues.Flow3,
					Flow4 = RecentValues.Flow4,
					Flow5 = RecentValues.Flow5,
					Pump = RecentValues.Pump ? (byte)1 : (byte)0,
					Valve1 = RecentValues.Valve1 ? (byte)1 : (byte)0,
					Valve2 = RecentValues.Valve2 ? (byte)1 : (byte)0,
					Valve3 = RecentValues.Valve3 ? (byte)1 : (byte)0,
					Valve4 = RecentValues.Valve4 ? (byte)1 : (byte)0,
					Valve5 = RecentValues.Valve5 ? (byte)1 : (byte)0,
					Pressure = RecentValues.Pressure,
					TempCPU = RecentValues.TempCPU,
					PowerFail_5V = RecentValues.PowerFail_5V ? (byte)1 : (byte)0,
					PowerFail_12V = RecentValues.PowerFail_12V ? (byte)1 : (byte)0,
					PowerFail_24V = RecentValues.PowerFail_24V ? (byte)1 : (byte)0,
					PowerGood_5V = RecentValues.PowerGood_5V ? (byte)1 : (byte)0,
					PowerGood_12V = RecentValues.PowerGood_12V ? (byte)1 : (byte)0,
					PowerGood_24V = RecentValues.PowerGood_24V ? (byte)1 : (byte)0,
					WatchdogPrealarm = RecentValues.WatchdogPrealarm ? (byte)1 : (byte)0
				});
			}
			
			Globals.UpdateRecents(false);
			Globals.GUI.Refresh_fast();
		}
		public static void SaveCycle(object sender, ElapsedEventArgs e) 
		{
			using (PrometheusService.Duration_EnvSave.NewTimer())
			{ EnvironmentService.SaveEnvironmentLog(); }
			using (PrometheusService.Duration_JournalSave.NewTimer()) 
			{ JournalService.SaveJournal();}
			using (PrometheusService.Duration_LevelSave.NewTimer()) 
			{ LevelService.SaveLevelLog();}
			using (PrometheusService.Duration_PowerSave.NewTimer()) 
			{ PowerService.SavePowerLog();}
			JournalService.CleanJournalLog();
		}
		public static void LogLevel(object sender, ElapsedEventArgs e) 
		{
			LevelService.LevelLog.Add(new LevelModel { 
				TimeStamp = DateTime.Now, 
				Tank = RecentValues.LevelInPercent 
			});
			LevelService.CleanLevelLog();
		}
		public static void LogEnvironment(object sender, ElapsedEventArgs e)
		{
			EnvironmentService.EnvironmentLog.Add(new EnvironmentModel
			{
				TimeStamp = DateTime.Now,
				TempAmb = RecentValues.TempAmb,
				TempExp = RecentValues.TempExp,
				TempPCB = RecentValues.TempPCB,
				TempCPU = RecentValues.TempCPU,
				Rain = RecentValues.Rain,
				Ground = RecentValues.Ground
			});
			EnvironmentService.CleanEnvironmentLog();
		}
		public static void LogPower(object sender, ElapsedEventArgs e)
		{
			PowerService.PowerLog.Add(new PowerModel
			{
				TimeStamp = DateTime.Now,
				PowerGood_5V = RecentValues.PowerGood_5V,
				PowerGood_12V = RecentValues.PowerGood_12V,
				PowerGood_24V = RecentValues.PowerGood_24V,
				PowerFail_5V = RecentValues.PowerFail_5V,
				PowerFail_12V = RecentValues.PowerFail_12V,
				PowerFail_24V = RecentValues.PowerFail_24V,
				WatchdogPrealarm = RecentValues.WatchdogPrealarm
			}) ;
			PowerService.CleanPowerLog();
		}
	}
	
}
