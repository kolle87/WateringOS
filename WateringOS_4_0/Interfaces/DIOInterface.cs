using System;
using System.Device.Gpio;
using WateringOS_4_0.Loggers;
using WateringOS_4_0.Services;

namespace WateringOS_4_0.Interfaces
{
    public class DIOInterface
    {
        private static GpioController gpio;
        public static bool IsInitialized = false;
        private static int FailureCount = 0;
        private const byte PIN_DO_Pump = 18;
        private const byte PIN_DO_Valve1 = 23;
        private const byte PIN_DO_Valve2 = 24;
        private const byte PIN_DO_Valve3 = 25;
        private const byte PIN_DO_Valve4 = 12;
        private const byte PIN_DO_Valve5 = 16;
        private const byte PIN_DI_PF5 = 6;
        private const byte PIN_DI_PF12 = 5;
        private const byte PIN_DI_PF24 = 4;
        private const byte PIN_DI_PG5 = 20;
        private const byte PIN_DI_PG12 = 19;
        private const byte PIN_DI_PG24 = 17;
        private const byte PIN_DI_WD_Warn = 26;
        private const byte PIN_DO_WD_Enable = 27;

        public void Initialize() 
        {
            Logger.Post(Logger.DIO, LogType.Information, "Start initialization", "The intialization of the Raspberry GPIO communication class has started.");
            try
            {   
                gpio = new GpioController();

                gpio.OpenPin(PIN_DO_Pump, PinMode.Output); // DO_Pump
                gpio.OpenPin(PIN_DO_Valve1, PinMode.Output); // DO_Valve1
                gpio.OpenPin(PIN_DO_Valve2, PinMode.Output); // DO_Valve2
                gpio.OpenPin(PIN_DO_Valve3, PinMode.Output); // DO_Valve3
                gpio.OpenPin(PIN_DO_Valve4, PinMode.Output); // DO_Valve4
                gpio.OpenPin(PIN_DO_Valve5, PinMode.Output); // DO_Valve5
                gpio.OpenPin(PIN_DI_PF5, PinMode.Input);  // DI_PF5
                gpio.OpenPin(PIN_DI_PF12, PinMode.Input);  // DI_PF12
                gpio.OpenPin(PIN_DI_PF24, PinMode.Input);  // DI_PF24
                gpio.OpenPin(PIN_DI_PG5, PinMode.Input);  // DI_PG5
                gpio.OpenPin(PIN_DI_PG12, PinMode.Input);  // DI_PG12
                gpio.OpenPin(PIN_DI_PG24, PinMode.Input);  // DI_PG24
                gpio.OpenPin(PIN_DI_WD_Warn, PinMode.Input);  // DI_WD_Warn
                gpio.OpenPin(PIN_DO_WD_Enable, PinMode.Output); // DO_WD_Enable

                SetPin(PIN_DO_Pump, false); // DO_Pump
                SetPin(PIN_DO_Valve1, false); // DO_Valve1
                SetPin(PIN_DO_Valve2, false); // DO_Valve2
                SetPin(PIN_DO_Valve3, false); // DO_Valve3
                SetPin(PIN_DO_Valve4, false); // DO_Valve4
                SetPin(PIN_DO_Valve5, false); // DO_Valve5
                FailureCount = 0;   
                AlarmService.DIO_InitialisationFail.Deactivate();
                AlarmService.DIO_InterfaceFail.Deactivate();
                IsInitialized = true;
            }
            catch (Exception e)
            {
                Logger.Post(Logger.DIO, LogType.Error, "Error while GPIO Class Initialization", e.Message);
                AlarmService.DIO_InitialisationFail.Activate();
                IsInitialized = false;
            }
        }
        public static void StartPump()
        {
            if (Valve1Open || Valve2Open || Valve3Open || Valve4Open || Valve5Open)
            {
                SetPin(PIN_DO_Pump, true);
            }
            else
            {
                Logger.Post(Logger.DIO, LogType.Error, "Pump interlock - activation suppressed", "The activation of pump was suppressed, because no open valve detected.");
            }
        }
        public static void StopPump() { SetPin(PIN_DO_Pump, false); }
        public static void OpenValve1() { SetPin(PIN_DO_Valve1, true); }
        public static void CloseValve1() { SetPin(PIN_DO_Valve1, false); }
        public static void OpenValve2() { SetPin(PIN_DO_Valve2, true); }
        public static void CloseValve2() { SetPin(PIN_DO_Valve2, false); }
        public static void OpenValve3() { SetPin(PIN_DO_Valve3, true); }
        public static void CloseValve3() { SetPin(PIN_DO_Valve3, false); }
        public static void OpenValve4() { SetPin(PIN_DO_Valve4, true); }
        public static void CloseValve4() { SetPin(PIN_DO_Valve4, false); }
        public static void OpenValve5() { SetPin(PIN_DO_Valve5, true); }
        public static void CloseValve5() { SetPin(PIN_DO_Valve5, false); }
        public static void EnableWD() { SetPin(PIN_DO_WD_Enable, true); }
        public static void DisableWD() { SetPin(PIN_DO_WD_Enable, false); }
        public static bool WatchDog_Prewarn { get { return (GetPin(PIN_DI_WD_Warn)); } }
        public static bool PowerGood_5V { get { return (GetPin(PIN_DI_PG5)); } }
        public static bool PowerGood_12V { get { return (GetPin(PIN_DI_PG12)); } }
        public static bool PowerGood_24V { get { return (GetPin(PIN_DI_PG24)); } }
        public static bool PowerFail_5V { get { return (GetPin(PIN_DI_PF5)); } }
        public static bool PowerFail_12V { get { return (GetPin(PIN_DI_PF12)); } }
        public static bool PowerFail_24V { get { return (GetPin(PIN_DI_PF24)); } }
        public static bool PumpActive { get { return (GetPin(PIN_DO_Pump)); } }
        public static bool Valve1Open { get { return (GetPin(PIN_DO_Valve1)); } }
        public static bool Valve2Open { get { return (GetPin(PIN_DO_Valve2)); } }
        public static bool Valve3Open { get { return (GetPin(PIN_DO_Valve3)); } }
        public static bool Valve4Open { get { return (GetPin(PIN_DO_Valve4)); } }
        public static bool Valve5Open { get { return (GetPin(PIN_DO_Valve5)); } }
        private static void SetPin(byte Pin, bool Active)
        {
            if (!IsInitialized) { return; }
            try
            {
                if (gpio.IsPinOpen(Pin))
                {
                    gpio.Write(Pin, (Active == PinValue.High));
                }
                else
                {
                    FailureCount++;
                    Logger.Post(Logger.DIO, LogType.Error, "GPIO_DI pin not open", "The requested pin action is not available, since the pin is not open. Ensure the controller have been initialized.");
                }
            }
            catch (Exception e)
            {
                FailureCount++;
                Logger.Post(Logger.DIO, LogType.Error, "Error while setting IO pin status", e.Message);
            }
            if (FailureCount > 5) { AlarmService.DIO_InterfaceFail.Activate(); IsInitialized = false; }
        }
        private static bool GetPin(byte Pin)
        {
            if (!IsInitialized) { return false;}
            bool _return;
            try
            {   
                if (gpio.IsPinOpen(Pin))
                {
                    _return = (PinValue.High == gpio.Read(Pin));
                }
                else
                {
                    FailureCount++;
                    Logger.Post(Logger.DIO, LogType.Error, "GPIO_DO pin not open", "The requested pin action is not available, since the pin is not open. Ensure the controller have been initialized.");
                    _return = false;
                }
            }
            catch (Exception e)
            {
                FailureCount++;
                Logger.Post(Logger.DIO, LogType.Error, "Error while reading IO pin status", e.Message);
                _return = false;
            }
            if (FailureCount > 5) { AlarmService.DIO_InterfaceFail.Activate(); IsInitialized = false; }
            return _return;
        }
    }
}
