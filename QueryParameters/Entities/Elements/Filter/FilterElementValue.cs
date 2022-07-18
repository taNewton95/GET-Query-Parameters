using QueryParameters.Entities;
using QueryParameters.Settings;

namespace QueryParameters.Entities
{
    public class FilterElementValue
    {

        public readonly object Value;

        public FilterElementValue(object value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

    }
}