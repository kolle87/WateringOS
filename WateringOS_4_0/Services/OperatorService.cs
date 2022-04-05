using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WateringOS_4_0.Models;

namespace WateringOS_4_0.Services
{
    public class OperatorService
    {
        public void ToggleValve_1()
        {
            RecentValues.Valve1 = !RecentValues.Valve1;
            // DIO_Server.Toggle(1)
        }
        public void ToggleValve_2()
        {
            RecentValues.Valve2 = !RecentValues.Valve2;
            // DIO_Server.Toggle(2)
        }
        public void ToggleValve_3()
        {
            RecentValues.Valve3 = !RecentValues.Valve3;
            // DIO_Server.Toggle(3)
        }
        public void ToggleValve_4()
        {
            RecentValues.Valve4 = !RecentValues.Valve4;
            // DIO_Server.Toggle(4)
        }
        public void ToggleValve_5()
        {
            RecentValues.Valve5 = !RecentValues.Valve5;
            // DIO_Server.Toggle(5)
        }
        public void TogglePump()
        {
            RecentValues.Pump = !RecentValues.Pump;
            // DIO_Server.Toggle(0)
        }
        public void ResetFlow()
        {
            RecentValues.Flow1 = 0;
            RecentValues.Flow2 = 0;
            RecentValues.Flow3 = 0;
            RecentValues.Flow4 = 0;
            RecentValues.Flow5 = 0;
            // SPIServer.ResetFlow()
        }
    }
}
