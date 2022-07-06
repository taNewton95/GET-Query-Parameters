using QueryParameters.Entities;

namespace QueryParameters.Parameters
{
    public class FilterElementExpression : FilterElement
    {

        public readonly FilterElementIdentifier Identifier;
        public readonly FilterElementCondition Condition;
        public readonly FilterElementValue Value;

        public FilterElementExpression(FilterElementIdentifier identifier, FilterElementCondition condition, FilterElementValue value)
        {
            Identifier = identifier;
            Condition = condition;
            Value = value;
        }

        public override string ToString()
        {
            return Identifier.ToString() + ' ' + Condition.ToString() + ' ' + Value.ToString();
        }

    }
}