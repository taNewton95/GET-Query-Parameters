
using QueryParameters.Settings;

namespace QueryParameters.Parameters
{
    public class SortParameter : BaseParameter
    {

        public SortDirection Direction = SortSettings.Default.DefaultDirection;
        public string Field;

        public override bool IsPopulated()
        {
            return !string.IsNullOrEmpty(Field);
        }

    }
}