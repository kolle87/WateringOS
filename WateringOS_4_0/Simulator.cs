using System;
using WateringOS_4_0.Models;

namespace WateringOS_4_0
{
    public static class Simulator
    {
        private readonly static Random Randomize = new();
        public static void SetInitalValues()
        {
            RecentValues.TimeStamp = DateTime.Now;
            RecentValues.Flow1 = 300;
            RecentValues.Flow2 = 600;
            RecentValues.Flow3 = 900;
            RecentValues.Flow4 = 1200;
            RecentValues.Flow5 = 1500;
            RecentValues.Ground = 50;
            RecentValues.Rain = 30;
            RecentValues.LevelInLiter = 38;
            RecentValues.LevelInPercent = 52;
            RecentValues.Pressure = 1.2;
            RecentValues.PowerFail_5V = false;
            RecentValues.PowerFail_12V = false;
            RecentValues.PowerFail_24V = false;
            RecentValues.PowerGood_5V = true;
            RecentValues.PowerGood_12V = true; 
            RecentValues.PowerGood_24V = true;
            RecentValues.WatchdogPrealarm = false;
            RecentValues.Pump = false;
            RecentValues.Valve1 = false;
            RecentValues.Valve2 = false;
            RecentValues.Valve3 = false;
            RecentValues.Valve4 = false;
            RecentValues.Valve5 = false;
            RecentValues.TempAmb = 26.5;
            RecentValues.TempCPU = 62.0;
            RecentValues.TempExp = 35.5;
            RecentValues.TempPCB = 32.0;
        }
        public static void SetNewValues()
        {
            RecentValues.TimeStamp = DateTime.Now;
            RecentValues.Flow1 = Randomize.Next(100,2500);
            RecentValues.Flow2 = Randomize.Next(100, 2500);
            RecentValues.Flow3 = Randomize.Next(100, 2500);
            RecentValues.Flow4 = Randomize.Next(100, 2500);
            RecentValues.Flow5 = Randomize.Next(100, 2500);
            RecentValues.Ground = (byte)Randomize.Next(0, 100);
            RecentValues.Rain = (byte)Randomize.Next(0, 100);
            RecentValues.LevelInPercent = (byte)Randomize.Next(0, 100);
            RecentValues.LevelInLiter = (byte)Math.Floor((80.0 * RecentValues.LevelInPercent)/100);
            RecentValues.Pressure = Randomize.NextDouble() * 9;
            RecentValues.PowerFail_5V = Randomize.NextDouble() > 0.5;
            RecentValues.PowerFail_12V = Randomize.NextDouble() > 0.5;
            RecentValues.PowerFail_24V = Randomize.NextDouble() > 0.5;
            RecentValues.PowerGood_5V = Randomize.NextDouble() > 0.5;
            RecentValues.PowerGood_12V = Randomize.NextDouble() > 0.5;
            RecentValues.PowerGood_24V = Randomize.NextDouble() > 0.5;
            RecentValues.WatchdogPrealarm = Randomize.NextDouble() > 0.5;
            RecentValues.Pump = false;
            RecentValues.Valve1 = false;
            RecentValues.Valve2 = false;
            RecentValues.Valve3 = false;
            RecentValues.Valve4 = false;
            RecentValues.Valve5 = false;
            RecentValues.TempAmb = Randomize.Next(-20, 30);
            RecentValues.TempCPU = Randomize.Next(35, 70);
            RecentValues.TempExp = Randomize.Next(-10, 40);
            RecentValues.TempPCB = Randomize.Next(20, 50);
        }
    }
}
