using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WateringOS_4_0.Services
{
    public class CounterService
    {
        public event Action CounterChanged;
        public int CurrentCount = 0;

        public void IncreaseCount()
        {
            CurrentCount++;
            if (CounterChanged != null) { CounterChanged?.Invoke(); }
        }
    }
}
