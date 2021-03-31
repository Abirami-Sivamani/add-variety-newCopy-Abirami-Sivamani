using System;

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
        TO_EMAIL
    };

  public static BreachType inferBreach(double value, double lowerLimit, double upperLimit) {
      if(value < lowerLimit) {
        return BreachType.TOO_LOW;
      }
      if(value > upperLimit) {
        return BreachType.TOO_HIGH;
      }
      return BreachType.NORMAL;
    }

    public static BreachType classifyTemperatureBreach(CoolingType coolingType, double temperatureInC) {
      ExtremeLimit _extremeLimit = new PassiveCoolingLimit().SetExtremeLimit(coolingType);
      return inferBreach(temperatureInC, _extremeLimit.lowerLimit, _extremeLimit.upperLimit);
    }

    class ExtremeLimit
    {
        public int lowerLimit { get; set; }
        public int upperLimit { get; set; }
    }
    
    interface ILimitInitializer
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
                upperLimit = 35
            };
        }
    }

    public struct BatteryCharacter {
      public CoolingType coolingType;
      public string brand;
    }
    
    public static void checkAndAlert(AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC) 
    {
      BreachType breachType = classifyTemperatureBreach(batteryChar.coolingType, temperatureInC);

      switch(alertTarget) {
        case AlertTarget.TO_CONTROLLER:
          sendToController(breachType);
          break;
        case AlertTarget.TO_EMAIL:
          sendToEmail(breachType);
          break;
      }
    }
    
    public static void sendToController(BreachType breachType) {
      const ushort header = 0xfeed;
      Console.WriteLine("{} : {}\n", header, breachType);
    }
    
    public static void sendToEmail(BreachType breachType) {
      string recepient = "a.b@c.com";
      switch(breachType) {
        case BreachType.TOO_LOW:
          Console.WriteLine("To: {}\n", recepient);
          Console.WriteLine("Hi, the temperature is too low\n");
          break;
        case BreachType.TOO_HIGH:
          Console.WriteLine("To: {}\n", recepient);
          Console.WriteLine("Hi, the temperature is too high\n");
          break;
        case BreachType.NORMAL:
          break;
      }
    }
  }
}
