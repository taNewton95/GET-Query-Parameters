using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using QueryParameters.Entities;
using System.Text;
using QueryParameters.Entities.Exceptions;

namespace QueryParameters.Entities
{
    public class FilterElementCollection : FilterElement, IEnumerable<FilterElement>
    {

        private readonly List<FilterElement> ChildElements = new();

        /// <summary>
        /// Whether the element collection is currently active in scope. I.e. <see cref="EndScope"/> has not yet been called on this collection.
        /// </summary>
        private bool IsInActiveScope = true;

        public FilterElement this[int i]
        {
            get => ChildElements[i];
        }

        public FilterElementCollection()
        {

        }

        public void Add(FilterElement filterElement)
        {
            if (ChildElements.LastOrDefault() is FilterElementCollection filterElementCollection && filterElementCollection.IsInActiveScope)
            {
                filterElementCollection.Add(filterElement);
                return;
            }

            if (ChildElements.Count > 0 && filterElement is not FilterElementOperator && ChildElements.Last() is not FilterElementOperator) throw new Exception("An operator must be set between elements");

            ChildElements.Add(filterElement);
        }

        public void BeginScope()
        {
            var lastElement = ChildElements.LastOrDefault();

            if (lastElement != null)
            {
                // Begin scope called multiple times in succession
                if (lastElement is FilterElementCollection filterElementCollection)
                {
                    filterElementCollection.BeginScope();
                    return;
                }

                if (lastElement is not FilterElementOperator) throw new MissingOperatorException();
            }

            ChildElements.Add(new FilterElementCollection());
        }

        public void EndScope()
        {
            if (ChildElements.LastOrDefault() is FilterElementCollection filterElementCollection)
            {
                filterElementCollection.EndScope();
                return;
            }

            if (!ChildElements.Any()) throw new NoElementsException();

            IsInActiveScope = false;
        }

        public IEnumerator<FilterElement> GetEnumerator()
        {
            return ChildElements?.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ChildElements?.GetEnumerator();
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append('(');

            foreach (var element in ChildElements)
            {
                if (str.Length > 1)
                {
                    str.Append(' ');
                }

                str.Append(element.ToString());
            }

            str.Append(')');

            return str.ToString();
        }

    }
}