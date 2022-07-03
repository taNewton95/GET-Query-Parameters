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

        public int DefaultTake = Default?.DefaultTake ?? 0; // Null handler for the constructor for default settings, set to arbitrary value, this should be set post constructor by the initialiser.
        public int DefaultSkip = Default?.DefaultSkip ?? 0; // Null handler for the constructor for default settings, set to arbitrary value, this should be set post constructor by the initialiser.

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}