using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WateringOS_4_0.Loggers;
using WateringOS_4_0.Services;
using static WateringOS_4_0.Globals;

namespace WateringOS_4_0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Server.Host = CreateHostBuilder(args).Build();

            Logger.SYS.Parent = Server.Host.Services.GetRequiredService<ILogger<Program>>();
            Logger.USR.Parent = Server.Host.Services.GetRequiredService<ILogger<AccountService>>();
            Logger.WEB.Parent = Server.Host.Services.GetRequiredService<ILogger<Pages.WebInterface>>();
            Logger.DIO.Parent = Server.Host.Services.GetRequiredService<ILogger<Interfaces.DIOInterface>>();
            Logger.TWI.Parent = Server.Host.Services.GetRequiredService<ILogger<Interfaces.TWIInterface>>();
            Logger.SPI.Parent = Server.Host.Services.GetRequiredService<ILogger<Interfaces.SPIInterface>>();

            Simulator.SetInitalValues();

            JournalService.LoadJournal();
            EnvironmentService.LoadEnvironmentLog();
            LevelService.LoadLevelLog();
            PowerService.LoadPowerLog();
            WateringLogService.LoadWateringLogs();
            LoadSettings();
            LoadPreferences();

            Version.Load();
            Interface.Initialize();
            Schedulers.Initialize();

            Server.Host.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
