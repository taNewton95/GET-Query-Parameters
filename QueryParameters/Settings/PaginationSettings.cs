using System;
using QueryParameters.Parameters;

namespace QueryParameters.Settings
{
    public class PaginationSettings : ICloneable
    {

        public static readonly PaginationSettings Default = new PaginationSettings()
        {
            DefaultSkip = 0,
            DefaultTake = 0,
        };

        public int DefaultTake;
        public int DefaultSkip;

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}