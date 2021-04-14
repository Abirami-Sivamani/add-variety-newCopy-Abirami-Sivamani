using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Notifiers
{
    public class FakeEmailNotifier : INotification
    {
        public bool IsEmailTriggerNotificationCalled = false;
        public void TriggerNotification(BreachType breachType)
        {
            IsEmailTriggerNotificationCalled = true;
        }
    }
}
