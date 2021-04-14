using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert
{
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

    public class ExtremeLimit
    {
        public int lowerLimit { get; set; }
        public int upperLimit { get; set; }
    }

    public class PassiveCoolingLimit : ILimitInitializer
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

    public class HighCoolingLimit : ILimitInitializer
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

    public class MediumCoolingLimit : ILimitInitializer
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
}
