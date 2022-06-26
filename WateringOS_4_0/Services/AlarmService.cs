using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WateringOS_4_0.Loggers;

namespace WateringOS_4_0.Services
{
    public class AlarmService
    {
        public static event Action<Alarm> OnAlarm;
        public static event Action<ObservableCollection<Alarm>> OnUpdate;
        public static List<Alarm> ActiveAlarms = new();
        public class Alarm
        {
            public int ID;
            public string Name;
            public string Description;
            public bool IsActive;
            public void Activate()
            {
                this.IsActive = true;
                ActiveAlarms.Add(this);
                OnAlarm?.Invoke(this);
                Logger.Post(Logger.SYS, LogType.Information, "ALARM ACTIVATED - "+this.Name, this.Description);
            }
            public void Deactivate()
            {
                if (this.IsActive)
                { 
                    this.IsActive = false;
                    ActiveAlarms.Remove(this);
                    OnAlarm?.Invoke(this);
                    Logger.Post(Logger.SYS, LogType.Information, "ALARM DEACTIVATED - "+this.Name, this.Description);
                }
            }
        }
       
        public static void RequestUpdate()
        {
            ObservableCollection<Alarm> AlarmList = new();
            foreach (Alarm vItem in ActiveAlarms)
                AlarmList.Add(vItem);
            OnUpdate?.Invoke(AlarmList);
        }

        #region TWI Alarms
        public static Alarm TWI_InitialisationFail = new()
        {
            ID = 1010,
            IsActive = false,
            Name = "Initialization Fail - TWI Interface",
            Description = "A failure occured while trying to initialize the TWI Interface"
        }; 
        public static Alarm TWI_InterfaceFail = new()
        {
            ID = 1011,
            IsActive = false,
            Name = "Interface Fail - TWI Interface",
            Description = "A failure while using the TWI Interface. Several read attempts failed."
        };
        public static Alarm TWI_SensFail_BoardTemperatur = new() {
            ID = 1012,
            IsActive = false,
            Name = "Sensor Fail - Board Temperature",
            Description = "A failure occured while trying to read the Board temperature"
        };
        public static Alarm TWI_SensFail_AmbientTemperatur = new()
        {
            ID = 1013,
            IsActive = false,
            Name = "Sensor Fail - Ambient Temperature",
            Description = "A failure occured while trying to read the Board temperature"
        };
        public static Alarm TWI_SensFail_ExposedTemperatur = new()
        {
            ID = 1014,
            IsActive = false,
            Name = "Sensor Fail - Exposed Temperature",
            Description = "A failure occured while trying to read the Board temperature"
        };
        public static Alarm TWI_SensFail_SoilMoisture = new()
        {
            ID = 1015,
            IsActive = false,
            Name = "Sensor Fail - Soil Moisture Temperature",
            Description = "A failure occured while trying to read the Board temperature"
        };
        #endregion
        #region SPI Alarms
        public static Alarm SPI_InitialisationFail = new()
        {
            ID = 1021,
            IsActive = false,
            Name = "Initialization Fail - SPI Interface",
            Description = "A failure occured while trying to initialize the SPI Interface"
        };
        public static Alarm SPI_InterfaceFail = new()
        {
            ID = 1022,
            IsActive = false,
            Name = "Interface Fail - SPI Interface",
            Description = "A failure while using the SPI Interface. Several read attempts failed."
        };
        #endregion
        #region DIO Alarms
        public static Alarm DIO_InitialisationFail = new()
        {
            ID = 1021,
            IsActive = false,
            Name = "Initialization Fail - DIO Interface",
            Description = "A failure occured while trying to initialize the DIO Interface"
        };
        public static Alarm DIO_InterfaceFail = new()
        {
            ID = 1022,
            IsActive = false,
            Name = "Interface Fail - DIO Interface",
            Description = "A failure while using the DIO Interface. Several operations failed."
        };
        #endregion
    }
}
