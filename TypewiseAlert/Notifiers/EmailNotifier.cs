using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Notifiers
{
    public class EmailMessageInitializer
    {
        public Dictionary<BreachType, Func<IEmailTrigger>> _Email = new Dictionary<BreachType, Func<IEmailTrigger>>();

        public EmailMessageInitializer()
        {
            _Email.Add(BreachType.TOO_HIGH, () => { return new HighLimitMessageEmail(); });
            _Email.Add(BreachType.TOO_LOW, () => { return new LowLimitMessageEmail(); });
            _Email.Add(BreachType.NORMAL, () => { return new NormalLimitMessageEmail(); });
        }
    }

    public class EmailNotifier : INotification
    {
        public void TriggerNotification(BreachType breachType)
        {
            string recepient = "a.b@c.com";
            new EmailMessageInitializer()._Email[breachType]().TriggerEmail(recepient, breachType);
        }

    }

    class LowLimitMessageEmail : IEmailTrigger
    {
        public void TriggerEmail(string Recepient, BreachType _BreachType)
        {
            Console.WriteLine("To: {}\n", Recepient);
            Console.WriteLine("Hi, the temperature is too low\n");
        }
    }

    class HighLimitMessageEmail : IEmailTrigger
    {
        public void TriggerEmail(string Recepient, BreachType _BreachType)
        {
            Console.WriteLine("To: {}\n", Recepient);
            Console.WriteLine("Hi, the temperature is too high\n");
        }
    }

    class NormalLimitMessageEmail : IEmailTrigger
    {
        public void TriggerEmail(string Recepient, BreachType _BreachType)
        {
            Console.WriteLine("To: {}\n", Recepient);
            Console.WriteLine("Hi, the temperature is in normal state\n");
        }
    }
}
