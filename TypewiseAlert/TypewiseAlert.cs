using System;
using System.Collections.Generic;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {       
        public enum BreachType {
            NORMAL,
            TOO_LOW,
            TOO_HIGH
        };
        
        public enum CoolingType
        {
            PASSIVE_COOLING,
            HI_ACTIVE_COOLING,
            MED_ACTIVE_COOLING
        };

        public enum AlertTarget
        {
            TO_CONTROLLER,
            TO_EMAIL,
            TO_CONSOLE,
            TO_FAKE_EMAIL
        };

        public class CoolingLimitDictionaryInitializer
        {
            public Dictionary<CoolingType, Func<ILimitInitializer>> _CoolingLimitType = new Dictionary<CoolingType, Func<ILimitInitializer>>();

            public CoolingLimitDictionaryInitializer()
            {
                _CoolingLimitType.Add(CoolingType.PASSIVE_COOLING, () => { return new PassiveCoolingLimit(); });
                _CoolingLimitType.Add(CoolingType.HI_ACTIVE_COOLING, () => { return new HighCoolingLimit(); });
                _CoolingLimitType.Add(CoolingType.MED_ACTIVE_COOLING, () => { return new MediumCoolingLimit(); });
            }
        }

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

        public class AlertTargetType
        {
            public Dictionary<AlertTarget, Action> _AlertTargetType = new Dictionary<AlertTarget, Action>();
            public AlertTargetType(BreachType breachType)
            {
                _AlertTargetType.Add(AlertTarget.TO_CONTROLLER, (() => SendToController(breachType)));
                _AlertTargetType.Add(AlertTarget.TO_EMAIL, (() => SendToEmail(breachType)));
                _AlertTargetType.Add(AlertTarget.TO_CONSOLE, (() => SendToConsole(breachType)));
                _AlertTargetType.Add(AlertTarget.TO_FAKE_EMAIL, (() => SendToFakeEmail(breachType)));
            }
        }

        public static BreachType InferBreach(double value, double lowerLimit, double upperLimit) {
            return (value < lowerLimit) ? BreachType.TOO_LOW : (value > upperLimit) ? BreachType.TOO_HIGH : BreachType.NORMAL;
        }

        public static BreachType ClassifyTemperatureBreach(CoolingType coolingType, double temperatureInC) 
        {
          ExtremeLimit _extremeLimit = new CoolingLimitDictionaryInitializer()._CoolingLimitType[coolingType]().SetExtremeLimit(coolingType);
          return InferBreach(temperatureInC, _extremeLimit.lowerLimit, _extremeLimit.upperLimit);
        }

        public class ExtremeLimit
        {
            public int lowerLimit { get; set; }
            public int upperLimit { get; set; }
        }

        public interface ILimitInitializer
        {
            ExtremeLimit SetExtremeLimit(CoolingType coolingType);
        }

        class PassiveCoolingLimit : ILimitInitializer
        {
            public ExtremeLimit SetExtremeLimit(CoolingType coolingType)
            {
                return new ExtremeLimit()
                {
                    lowerLimit = 0,
                    upperLimit = 35
                };
            }
        }

        class HighCoolingLimit : ILimitInitializer
        {
            public ExtremeLimit SetExtremeLimit(CoolingType coolingType)
            {
                return new ExtremeLimit()
                {
                    lowerLimit = 0,
                    upperLimit = 45
                };
            }
        }

        class MediumCoolingLimit : ILimitInitializer
        {
            public ExtremeLimit SetExtremeLimit(CoolingType coolingType)
            {
                return new ExtremeLimit()
                {
                    lowerLimit = 0,
                    upperLimit = 40
                };
            }
        }

        public struct BatteryCharacter {
          public CoolingType coolingType;
          public string brand;
        }

        public static void CheckAndAlert(AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC) 
        {
          BreachType breachType = ClassifyTemperatureBreach(batteryChar.coolingType, temperatureInC);
          new AlertTargetType(breachType)._AlertTargetType[alertTarget]();
        }

        public static void SendToController(BreachType breachType) {
          const ushort header = 0xfeed;
          Console.WriteLine("{} : {}\n", header, breachType);
        }

        public static void SendToEmail(BreachType breachType) {
          string recepient = "a.b@c.com";
          new EmailMessageInitializer()._Email[breachType]().TriggerEmail(recepient, breachType);
        }
        
        public static void SendToConsole(BreachType breachType)
        {
            Console.WriteLine("Temperature state: " + breachType);
        }
        
        public static void SendToFakeEmail(BreachType breachType)
        {
            string recepient = "a.b@c.com";
            new FakeEmailMessage().TriggerEmail(recepient, breachType);
        }

        public interface IEmailTrigger
        {
             void TriggerEmail(string Recepient, BreachType _BreachType);
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
}
