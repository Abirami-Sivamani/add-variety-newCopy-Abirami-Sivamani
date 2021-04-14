using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Notifiers
{
    public class FakeConsoleNotifier : INotification
    {
        public bool IsFakeConsoleTriggerNotificationCalled = false;
        public void TriggerNotification(BreachType breachType)
        {
            IsFakeConsoleTriggerNotificationCalled = true;
        }
    }
}
