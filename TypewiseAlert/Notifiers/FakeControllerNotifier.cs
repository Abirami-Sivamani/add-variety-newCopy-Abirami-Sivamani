using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Notifiers
{
    public class FakeControllerNotifier : INotification
    {
        public bool IsControllerTriggerNotificationCalled = false;
        public void TriggerNotification(BreachType breachType)
        {
            IsControllerTriggerNotificationCalled = true;
        }
    }
}
