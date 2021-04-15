using System;
using Xunit;
using TypewiseAlert.Notifiers;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Test
{
  public class TypewiseAlertTest
  {

    [Fact]
    public void InfersBreachForLowLimit()
    {
        Assert.True(InferBreach(12, 20, 30) == BreachType.TOO_LOW);
    }

    [Fact]
    public void InfersBreachForHighLimit()
    {
        Assert.True(InferBreach(35, 20, 30) == BreachType.TOO_HIGH);
    }

    [Fact]
    public void InfersBreachForNormal()
    {
        Assert.True(InferBreach(22, 20, 30) == BreachType.NORMAL);
    }

    [Fact]
    public void ClassifyTemperatureBreachForPassiveCooling()
    {
        Assert.True(ClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, -5) == BreachType.TOO_LOW);
    }

    [Fact]
    public void ClassifyTemperatureBreachForMediumCooling()
    {
        Assert.True(ClassifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING, 45) == BreachType.TOO_HIGH);
    }

    [Fact]
    public void ClassifyTemperatureBreachForHighCooling()
    {
        Assert.True(ClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 42) == BreachType.NORMAL);
    }

    [Fact]
    public void EmailNotificationTest()
    {
        var Email = new FakeEmailNotifier();
        Email.TriggerNotification(BreachType.TOO_HIGH);
        Assert.True(Email.IsEmailTriggerNotificationCalled);
    }

    [Fact]
    public void ConsoleNotificationTest()
    {
        var Console = new FakeConsoleNotifier();
        Console.TriggerNotification(BreachType.TOO_LOW);
        Assert.True(Console.IsConsoleTriggerNotificationCalled);
    }

    [Fact]
    public void ControllerNotificationTest()
    {
        var Controller = new FakeControllerNotifier();
        Controller.TriggerNotification(BreachType.NORMAL);
        Assert.True(Controller.IsControllerTriggerNotificationCalled);
    }
    
    [Fact]
    public void CheckAndAlertTest()
    {
        var _exception = Record.Exception(() => CheckAndAlert(AlertTarget.TO_CONSOLE,new BatteryCharacter { brand = "XYZ", coolingType = CoolingType.MED_ACTIVE_COOLING},-5));
        Assert.Null(_exception);
    }
    
    [Fact]
    public void EmailMessageTypeInitializerTest()
    {
        var _emailClassType = new EmailMessageInitializer()._Email[BreachType.TOO_HIGH]();
        Assert.NotNull(_emailClassType);
    }

    [Fact]
    public void CoolingTypeInitializerTest()
    {
        var _coolingClassType = new CoolingLimitDictionaryInitializer()._CoolingLimitType[CoolingType.PASSIVE_COOLING]();
        Assert.NotNull(_coolingClassType);
    }
    
    [Fact]
    public void AlertNotifierTypeTest()
    {
        Assert.NotNull(new AlertNotifierType()._NotifierType[AlertTarget.TO_CONSOLE]());
    }
    
    [Fact]
    public void PassiveCoolingForExtremeLimitTest()
    {
        Assert.NotNull(new PassiveCoolingLimit().SetExtremeLimit(CoolingType.PASSIVE_COOLING));
    }

    [Fact]
    public void MediumCoolingForExtremeLimitTest()
    {
        Assert.NotNull(new PassiveCoolingLimit().SetExtremeLimit(CoolingType.MED_ACTIVE_COOLING));
    }

    [Fact]
    public void HighCoolingForExtremeLimitTest()
    {
        Assert.NotNull(new PassiveCoolingLimit().SetExtremeLimit(CoolingType.HI_ACTIVE_COOLING));
    }
    
    [Fact]
    public void EmailNotifierNoExceptionTest()
    {
        var _exception = Record.Exception(() => new EmailNotifier().TriggerNotification(BreachType.NORMAL));
        Assert.Null(_exception);
    }

    [Fact]
    public void HighLimitEmailNoExceptionTest()
    {
        var _exception = Record.Exception(() => new HighLimitMessageEmail().TriggerEmail("user@bcn.com", BreachType.NORMAL));
        Assert.Null(_exception);
    }

    [Fact]
    public void LowLimitEmailNoExceptionTest()
    {          
        var _exception = Record.Exception(() => new LowLimitMessageEmail().TriggerEmail("user@bcn.com", BreachType.TOO_HIGH));
        Assert.Null(_exception);
    }

    [Fact]
    public void NormalLimitEmailNoExceptionTest()
    {
        var _exception = Record.Exception(() => new NormalLimitMessageEmail().TriggerEmail("user@bcn.com", BreachType.TOO_LOW));
        Assert.Null(_exception);
    }

  }
}
