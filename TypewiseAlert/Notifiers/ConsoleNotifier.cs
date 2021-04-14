using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Notifiers
{
    public class ConsoleNotifier : INotification
    {
        public void TriggerNotification(BreachType breachType)
        {
            Console.WriteLine("Temperature state: " + breachType);
        }
    }
}
