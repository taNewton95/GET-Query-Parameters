using System;
using QueryParameters.Parameters;

namespace QueryParameters.Settings
{
    public class SortSettings : ICloneable
    {

        public static readonly SortSettings Default = new SortSettings()
        {
            DefaultDirection = SortDirection.Ascending
        };

        public SortDirection DefaultDirection = SortSettings.Default?.DefaultDirection ?? SortDirection.Ascending; // Null handler for the constructor for default settings, set to arbitrary value, this should be set post constructor by the initialiser.

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}