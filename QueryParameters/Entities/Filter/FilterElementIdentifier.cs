using QueryParameters.Entities;

namespace QueryParameters.Parameters
{
    public class FilterElementIdentifier
    {

        public readonly string Identifier;

        public FilterElementIdentifier(string identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return Identifier;
        }

    }
}