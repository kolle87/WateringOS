using Microsoft.Extensions.Logging;
using System;
using WateringOS_4_0.Services;

namespace WateringOS_4_0.Loggers
{
    public class Logger
    {
        public class LogParent { public ILogger Parent; public string Name; }
        public static LogParent SYS = new() { Name = "SYS" };
        public static LogParent USR = new() { Name = "USR" };
        public static LogParent WEB = new() { Name = "WEB" };
        public static LogParent DIO = new() { Name = "DIO" };
        public static LogParent TWI = new() { Name = "TWI" };
        public static LogParent SPI = new() { Name = "SPI" };
        public static void Post(LogParent _Sender, string _Type, string _Message, string _Details)
        {
            switch (_Type)
            {
                case LogType.Status:
                    _Sender.Parent.LogDebug(_Message);
                    break;
                case LogType.Information:
                    _Sender.Parent.LogInformation(_Message);
                    break;
                case LogType.Warning:
                    _Sender.Parent.LogWarning(_Message);
                    break;
                case LogType.Error:
                    _Sender.Parent.LogError(_Message);
                    break;
                case LogType.Fatal:
                    _Sender.Parent.LogCritical(_Message);
                    break;
                default:
                    _Sender.Parent.LogWarning("LOG TYPE UNKNOWN /// " + _Message);
                    break;
            }
            JournalService _JournalService = new();
            _JournalService.AddLogAndUpdate(new Models.LogModel
            {
                TimeStamp = DateTime.Now,
                App = _Sender.Name,
                Type = _Type,
                Message = _Message,
                Details = _Details
            });
        }
    }
    public class LogType
    {
        public const string Status = "Status";
        public const string Information = "Information";
        public const string Warning = "Warning";
        public const string Error = "Error";
        public const string Fatal = "Fatal";
    }
    public sealed class LoggingEventArgs
    {
        public DateTimeOffset TimeStamp { get; set; }
        public string Instance { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}
