using QueryParameters.Entities;

namespace QueryParameters.Entities
{
    public class FilterElementExpression : FilterElement
    {

        public readonly IdentifierElement Identifier;
        public readonly FilterElementCondition Condition;
        public readonly FilterElementValue Value;

        public FilterElementExpression(IdentifierElement identifier, FilterElementCondition condition, FilterElementValue value)
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