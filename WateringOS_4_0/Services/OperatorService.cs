using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Interfaces;
using WateringOS_4_0.Models;

namespace WateringOS_4_0.Services
{
    public class OperatorService
    {
        public void ToggleValve_1()
        {
            if (DIOInterface.Valve1Open)
            {
                DIOInterface.CloseValve1();
            }
            else
            {
                DIOInterface.OpenValve1();
            }
        }
        public void ToggleValve_2()
        {
            if (DIOInterface.Valve2Open)
            {
                DIOInterface.CloseValve2();
            }
            else
            {
                DIOInterface.OpenValve2();
            }
        }
        public void ToggleValve_3()
        {
            if (DIOInterface.Valve3Open)
            {
                DIOInterface.CloseValve3();
            }
            else
            {
                DIOInterface.OpenValve3();
            }
        }
        public void ToggleValve_4()
        {
            if (DIOInterface.Valve4Open)
            {
                DIOInterface.CloseValve4();
            }
            else
            {
                DIOInterface.OpenValve4();
            }
        }
        public void ToggleValve_5()
        {
            if (DIOInterface.Valve5Open)
            {
                DIOInterface.CloseValve5();
            }
            else
            {
                DIOInterface.OpenValve5();
            }
        }
        public void TogglePump()
        {
            if (DIOInterface.PumpActive)
            {
                DIOInterface.StopPump();
            }
            else
            {
                DIOInterface.StartPump();
            }
        }
        public void ResetFlow()
        {
            Globals.Interface.SPI.ResetFlow();
        }
    }
}
