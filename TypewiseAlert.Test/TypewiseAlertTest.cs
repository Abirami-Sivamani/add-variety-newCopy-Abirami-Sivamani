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

  }
}
