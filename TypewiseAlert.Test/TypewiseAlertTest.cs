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
  }
}
