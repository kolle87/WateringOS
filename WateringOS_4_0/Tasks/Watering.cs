using System;
using System.Threading.Tasks;
using Syncfusion.Blazor.Charts.Internal;
using WateringOS_4_0.Loggers;
using WateringOS_4_0.Models;
using WateringOS_4_0.Services;

namespace WateringOS_4_0;

public class Watering
{
    public static bool IsActive { get; private set; }
    public static void DoWatering(bool Out1_active, bool Out2_active, bool Out3_active, bool Out4_active, bool Out5_active)
    {
        WateringLogService.WateringLogTmp.Clear();
        IsActive = true;
        #region Variables
        byte wt1 = 0;
        byte wt2 = 0;
        byte wt3 = 0;
        byte wt4 = 0;
        byte wt5 = 0;

        bool WasWatered1 = false;
        bool WasWatered2 = false;
        bool WasWatered3 = false;
        bool WasWatered4 = false;
        bool WasWatered5 = false;

        int MinTankLvl = Globals.ParameterValue("Wat_min_tank", 3);
        int MinWatTime = Globals.ParameterValue("Wat_min_time", 15);
        int MaxWatTime = Globals.ParameterValue("Wat_max_time", 120);
        int MinWatVol = Globals.ParameterValue("Wat_min_vol", 200);
        int MaxWatVol = Globals.ParameterValue("Wat_max_vol", 2400);
        int DlyAfterValveOpen = Globals.ParameterValue("DLY_ValveOpen", 2000);
        int DlyAfterPumpStop = Globals.ParameterValue("DLY_PumpStop", 5000);

        int tmp_Vol1 = Globals.Preferences.Plants[0].Volume;
        int tmp_Vol2 = Globals.Preferences.Plants[1].Volume;
        int tmp_Vol3 = Globals.Preferences.Plants[2].Volume;
        int tmp_Vol4 = Globals.Preferences.Plants[3].Volume;
        int tmp_Vol5 = Globals.Preferences.Plants[4].Volume;
        #endregion Variables

        if (Out1_active || Out2_active || Out3_active || Out4_active || Out5_active)
        {
            Globals.Interface.SPI.ResetFlow();
        }
        
        if (Out1_active && CheckTankLevel(MinTankLvl))
        {
            Logger.Post(Logger.WAT, LogType.Information, "Watering Plant 1", "The routine activated watering of plant #1 for " + tmp_Vol1 + " ml");
            Interfaces.DIOInterface.OpenValve1();                                                    // Open Valve #1
            var t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterValveOpen); });
            t_wait.Wait();                                                                           // wait before pump start
            var t_Water1 = Task.Run(async delegate
            {
                Interfaces.DIOInterface.StartPump();                                                 // Start Pump
                while (Interfaces.SPIInterface.Flow1 < tmp_Vol1)
                {
                    await Task.Delay(1000);
                    wt1++;
                    if (wt1 >= MaxWatTime)
                    {
                        Logger.Post(Logger.WAT, LogType.Warning, $"Watering 1 reached max time ({wt1}sec)", "The watering procedure on line #1 reached the maximum time for watering and was aborted after " + wt1 + "sec");
                        break;
                    }
                }
                Interfaces.DIOInterface.StopPump();                                                 // Stop Pump
            });
            t_Water1.Wait();                                                                        // Check flow every second and wait for full volume watering
            Logger.Post(Logger.WAT, LogType.Information, $"Watering Plant 1 finished ({Interfaces.SPIInterface.Flow1}ml)", "The watering procedure of Plant 1 ended after " + wt1 + "sec and " + Interfaces.SPIInterface.Flow1 + " ml");
            t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterPumpStop); });
            t_wait.Wait();                                                                          // wait to depressurize
            Interfaces.DIOInterface.CloseValve1();                                                  // Close Valve #1
            WasWatered1 = true;
        }
        if (Out2_active && CheckTankLevel(MinTankLvl))
        {
            Logger.Post(Logger.WAT, LogType.Information, "Watering Plant 2", "The routine activated watering of plant #2 for " + tmp_Vol2 + " ml");
            Interfaces.DIOInterface.OpenValve2();                                                    // Open Valve #2
            var t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterValveOpen); });
            t_wait.Wait();                                                                           // wait before pump start
            var t_Water2 = Task.Run(async delegate
            {
                Interfaces.DIOInterface.StartPump();                                                 // Start Pump
                while (Interfaces.SPIInterface.Flow2 < tmp_Vol2)
                {
                    await Task.Delay(1000);
                    wt2++;
                    if (wt2 >= MaxWatTime)
                    {
                        Logger.Post(Logger.WAT, LogType.Warning, $"Watering 2 reached max time ({wt2}sec)", "The watering procedure on line #2 reached the maximum time for watering and was aborted after " + wt2 + "sec");
                        break;
                    }
                }
                Interfaces.DIOInterface.StopPump();                                                 // Stop Pump
            });
            t_Water2.Wait();                                                                        // Check flow every second and wait for full volume watering
            Logger.Post(Logger.WAT, LogType.Information, $"Watering Plant 2 finished ({Interfaces.SPIInterface.Flow2}ml)", "The watering procedure of Plant 2 ended after " + wt2 + "sec and " + Interfaces.SPIInterface.Flow2 + " ml");
            t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterPumpStop); });
            t_wait.Wait();                                                                          // wait to depressurize
            Interfaces.DIOInterface.CloseValve2();                                                  // Close Valve #2
            WasWatered2 = true;
        }
        if (Out3_active && CheckTankLevel(MinTankLvl))
        {
            Logger.Post(Logger.WAT, LogType.Information, "Watering Plant 3", "The routine activated watering of plant #3 for " + tmp_Vol3 + " ml");
            Interfaces.DIOInterface.OpenValve3();                                                    // Open Valve #3
            var t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterValveOpen); });
            t_wait.Wait();                                                                           // wait before pump start
            var t_Water3 = Task.Run(async delegate
            {
                Interfaces.DIOInterface.StartPump();                                                 // Start Pump
                while (Interfaces.SPIInterface.Flow3 < tmp_Vol3)
                {
                    await Task.Delay(1000);
                    wt3++;
                    if (wt3 >= MaxWatTime)
                    {
                        Logger.Post(Logger.WAT, LogType.Warning, $"Watering 3 reached max time ({wt3}sec)", "The watering procedure on line #3 reached the maximum time for watering and was aborted after " + wt3 + "sec");
                        break;
                    }
                }
                Interfaces.DIOInterface.StopPump();                                                 // Stop Pump
            });
            t_Water3.Wait();                                                                        // Check flow every second and wait for full volume watering
            Logger.Post(Logger.WAT, LogType.Information, $"Watering Plant 3 finished ({Interfaces.SPIInterface.Flow3}ml)", "The watering procedure of Plant 3 ended after " + wt3 + "sec and " + Interfaces.SPIInterface.Flow3 + " ml");
            t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterPumpStop); });
            t_wait.Wait();                                                                          // wait to depressurize
            Interfaces.DIOInterface.CloseValve3();                                                  // Close Valve #3
            WasWatered3 = true;
        }
        if (Out4_active && CheckTankLevel(MinTankLvl))
        {
            Logger.Post(Logger.WAT, LogType.Information, "Watering Plant 4", "The routine activated watering of plant #4 for " + tmp_Vol4 + " ml");
            Interfaces.DIOInterface.OpenValve4();                                                    // Open Valve #4
            var t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterValveOpen); });
            t_wait.Wait();                                                                           // wait before pump start
            var t_Water4 = Task.Run(async delegate
            {
                Interfaces.DIOInterface.StartPump();                                                 // Start Pump
                while (Interfaces.SPIInterface.Flow4 < tmp_Vol4)
                {
                    await Task.Delay(1000);
                    wt4++;
                    if (wt4 >= MaxWatTime)
                    {
                        Logger.Post(Logger.WAT, LogType.Warning, $"Watering 4 reached max time ({wt4})", "The watering procedure on line #4 reached the maximum time for watering and was aborted after " + wt4 + "sec");
                        break;
                    }
                }
                Interfaces.DIOInterface.StopPump();                                                 // Stop Pump
            });
            t_Water4.Wait();                                                                        // Check flow every second and wait for full volume watering
            Logger.Post(Logger.WAT, LogType.Information, $"Watering Plant 4 finished ({Interfaces.SPIInterface.Flow4}ml)", "The watering procedure of Plant 4 ended after " + wt4 + "sec and " + Interfaces.SPIInterface.Flow4 + " ml");
            t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterPumpStop); });
            t_wait.Wait();                                                                          // wait to depressurize
            Interfaces.DIOInterface.CloseValve4();                                                  // Close Valve #4
            WasWatered4 = true;
        }
        if (Out5_active && CheckTankLevel(MinTankLvl))
        {
            Logger.Post(Logger.WAT, LogType.Information, "Watering Plant 5", "The routine activated watering of plant #5 for " + tmp_Vol5 + " ml");
            Interfaces.DIOInterface.OpenValve5();                                                    // Open Valve #5
            var t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterValveOpen); });
            t_wait.Wait();                                                                           // wait before pump start
            var t_Water5 = Task.Run(async delegate
            {
                Interfaces.DIOInterface.StartPump();                                                 // Start Pump
                while (Interfaces.SPIInterface.Flow5 < tmp_Vol5)
                {
                    await Task.Delay(1000);
                    wt5++;
                    if (wt5 >= MaxWatTime)
                    {
                        Logger.Post(Logger.WAT, LogType.Warning, $"Watering 5 reached max time ({wt5}sec)", "The watering procedure on line #5 reached the maximum time for watering and was aborted after " + wt5 + "sec");
                        break;
                    }
                }
                Interfaces.DIOInterface.StopPump();                                                 // Stop Pump
            });
            t_Water5.Wait();                                                                        // Check flow every second and wait for full volume watering
            Logger.Post(Logger.WAT, LogType.Information, $"Watering Plant 5 finished ({Interfaces.SPIInterface.Flow5}ml)", "The watering procedure of Plant 5 ended after " + wt5 + "sec and " + Interfaces.SPIInterface.Flow5 + " ml");
            t_wait = Task.Run(async delegate { await Task.Delay(DlyAfterPumpStop); });
            t_wait.Wait();                                                                          // wait to depressurize
            Interfaces.DIOInterface.CloseValve5();                                                  // Close Valve #5
            WasWatered5 = true;
        }

        IsActive = false;
        var t_wait_final = Task.Run(async delegate { await Task.Delay(10000); });
        t_wait_final.Wait(); 
        if (WasWatered1 || WasWatered2 || WasWatered3 || WasWatered4 || WasWatered5)
        {
            WateringLogService.ShiftLogs();
        }
    }

    public static bool CheckTankLevel(int MinimumLevel)
    {
        if (RecentValues.LevelInPercent <= MinimumLevel)
        {
            Logger.Post(Logger.WAT, LogType.Fatal, "Tank level to low for watering", "Watering was suspended due to low tank level of "+RecentValues.LevelInPercent+"%.");
            return false;
        }
        else
        {
            return true;
        }
    }
}