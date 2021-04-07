using System;
using Xunit;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Test
{
  public class TypewiseAlertTest
  {
    [Fact]
    public void InfersBreachAsPerLimits()
    {
        Assert.True(InferBreach(12, 20, 30) == BreachType.TOO_LOW);
        Assert.True(InferBreach(35, 20, 30) == BreachType.TOO_HIGH);
        Assert.True(InferBreach(22, 20, 30) == BreachType.NORMAL);
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
