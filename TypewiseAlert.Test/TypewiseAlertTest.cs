using System;
using Xunit;

namespace TypewiseAlert.Test
{
  public class TypewiseAlertTest
  {
    [Fact]
    public void InfersBreachAsPerLimits()
    {
       Assert.True(TypewiseAlert.InferBreach(12, 20, 30) == TypewiseAlert.BreachType.TOO_LOW);
       Assert.True(TypewiseAlert.InferBreach(35, 20, 30) == TypewiseAlert.BreachType.TOO_HIGH);
       Assert.True(TypewiseAlert.InferBreach(22, 20, 30) == TypewiseAlert.BreachType.NORMAL);
    }
    
    [Fact]
    public void ClassifyTemperatureBreachForPassiveCooling()
    {
        Assert.True(ClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, -5) == BreachType.TOO_LOW);
        Assert.True(ClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 40) == BreachType.TOO_HIGH);
        Assert.True(ClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 30) == BreachType.NORMAL);
    }

    [Fact]
    public void ClassifyTemperatureBreachForMediumCooling()
    {
        Assert.True(ClassifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING, -5) == BreachType.TOO_LOW);
        Assert.True(ClassifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING, 45) == BreachType.TOO_HIGH);
        Assert.True(ClassifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING, 38) == BreachType.NORMAL);
    }

    [Fact]
    public void ClassifyTemperatureBreachForHighCooling()
    {
        Assert.True(ClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, -5) == BreachType.TOO_LOW);
        Assert.True(ClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 50) == BreachType.TOO_HIGH);
        Assert.True(ClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 42) == BreachType.NORMAL);
    }
  }
}
