using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Notifiers
{
    public class AlertNotifierType
    {
        public Dictionary<AlertTarget, Func<INotification>> _NotifierType = new Dictionary<AlertTarget, Func<INotification>>();

        public AlertNotifierType()
        {
            _NotifierType.Add(AlertTarget.TO_CONTROLLER, () => { return new ControllerNotifier(); });
            _NotifierType.Add(AlertTarget.TO_EMAIL, () => { return new EmailNotifier(); });
            _NotifierType.Add(AlertTarget.TO_CONSOLE, () => { return new ConsoleNotifier(); });
        }
    }
}
