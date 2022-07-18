using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Entities
{
    public class SortElementExpression : SortElement
    {

        public readonly IdentifierElement Identifier;
        public readonly SortElementOperator Operator;
        public readonly SortElementDirection Direction;

        public SortElementExpression(IdentifierElement identifier, SortElementOperator @operator, SortElementDirection direction)
        {
            Identifier = identifier;
            Operator = @operator;
            Direction = direction;
        }

        public override string ToString()
        {
            return Identifier.ToString() + ' ' + Operator.ToString() + ' ' + Direction.ToString();
        }

    }
}
