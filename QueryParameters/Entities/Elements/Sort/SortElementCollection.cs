using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Entities
{
    public class SortElementCollection : IEnumerable<SortElementExpression>
    {

        private readonly List<SortElementExpression> ChildElements = new();

        public IEnumerator<SortElementExpression> GetEnumerator()
        {
            return ChildElements?.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ChildElements?.GetEnumerator();
        }

        public void Add(SortElementExpression sortElement)
        {
            ChildElements.Add(sortElement);
        }

        public void AddRange(params SortElementExpression[] sortElement)
        {
            AddRange((IEnumerable<SortElementExpression>)sortElement);
        }

        public void AddRange(IEnumerable<SortElementExpression> sortElement)
        {
            ChildElements.AddRange(sortElement);
        }

    }
}
