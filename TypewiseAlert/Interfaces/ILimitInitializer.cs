using System;
using System.Collections.Generic;
using System.Text;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Interfaces
{
    public interface ILimitInitializer
    {
        ExtremeLimit SetExtremeLimit(CoolingType coolingType);
    }
}
