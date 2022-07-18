using System;
using QueryParameters.Entities;

namespace QueryParameters.Settings
{
    public class SortSettings : ICloneable
    {

        public static readonly SortSettings Default = new SortSettings()
        {
            DefaultDirection = SortOperator.Ascending
        };

        public SortOperator DefaultDirection = SortSettings.Default?.DefaultDirection ?? SortOperator.Ascending; // Null handler for the constructor for default settings, set to arbitrary value, this should be set post constructor by the initialiser.

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}