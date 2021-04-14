using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Notifiers
{
    public class FakeAlertNotifierType
    {
        public Dictionary<AlertTarget, Func<INotification>> _NotifierType = new Dictionary<AlertTarget, Func<INotification>>();

        public FakeAlertNotifierType()
        {
            _NotifierType.Add(AlertTarget.TO_CONTROLLER, () => { return new FakeControllerNotifier(); });
            _NotifierType.Add(AlertTarget.TO_EMAIL, () => { return new FakeEmailNotifier(); });
            _NotifierType.Add(AlertTarget.TO_CONSOLE, () => { return new FakeConsoleNotifier(); });
        }
    }
}
