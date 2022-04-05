using Prometheus;
using System;
using System.Linq;
using System.Timers;
using WateringOS_4_0.Models;
using WateringOS_4_0.Services;

namespace WateringOS_4_0
{
	public class Routines
	{
		public static void MainTask(object sender, ElapsedEventArgs e)
		{
			using (PrometheusService.Duration_MainTask.NewTimer())
            {
				
            }
			PrometheusService.UpdateMetrics();
		}
		public static void FastTask(object sender, ElapsedEventArgs e)
		{
			using (PrometheusService.Duration_FastTask.NewTimer())
            {
			//Simulator.SetNewValues();
			Globals.UpdateRecents();

            }
		}
		public static void SaveCycle(object sender, ElapsedEventArgs e) 
		{
			using (PrometheusService.Duration_EnvSave.NewTimer())
			{ EnvironmentService.SaveEnvironmentLog(); }
			using (PrometheusService.Duration_EnvSave.NewTimer()) 
			{ JournalService.SaveJournal();}
			using (PrometheusService.Duration_EnvSave.NewTimer()) 
			{ LevelService.SaveLevelLog();}
			using (PrometheusService.Duration_EnvSave.NewTimer()) 
			{ PowerService.SavePowerLog();}
			using (PrometheusService.Duration_EnvSave.NewTimer()) 
			{ WateringLogService.SaveWateringLogs();}
		}
		public static void LogLevel(object sender, ElapsedEventArgs e) 
		{
			LevelService.LevelLog.Add(new LevelModel { 
				TimeStamp = DateTime.Now, 
				Tank = RecentValues.LevelInPercent 
			});
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
		}
	}
	
}
