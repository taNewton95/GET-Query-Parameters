using QueryParameters.AspNetCore.Mvc.Settings;
using QueryParameters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.AspNetCore.Mvc.Parsers
{
    public class DefaultSortParser : ISortParser
    {

        public SortElementExpression[] Parse(string sort)
        {
            var sortElements = new List<SortElementExpression>();
            var stringParser = new StringParser(sort);

            IdentifierElement identifierElement = null;
            SortElementDirection directionElement = null;

            while (stringParser.Next(new HashSet<char>(new[] { ' ', ',' })))
            {
                if (string.IsNullOrEmpty(stringParser.CurrentString)) continue;

                switch (stringParser.MatchedDelimiter)
                {
                    case null:
                    case ',':

                        if (identifierElement == null)
                        {
                            identifierElement = new IdentifierElement(stringParser.CurrentString);
                        }
                        else if (directionElement == null)
                        {
                            if (stringParser.CurrentString.Equals(SyntaxSettings.SortAscending, StringComparison.CurrentCultureIgnoreCase))
                            {
                                directionElement = SortElementDirection.Ascending;
                            }
                            else if (stringParser.CurrentString.Equals(SyntaxSettings.SortDescending, StringComparison.CurrentCultureIgnoreCase))
                            {
                                directionElement = SortElementDirection.Descending;
                            }
                        }

                        sortElements.Add(new SortElementExpression(identifierElement, directionElement ?? SortElementDirection.Ascending));
                        identifierElement = null;
                        directionElement = null;
                        break;

                    case ' ':
                        if (identifierElement == null)
                        {
                            identifierElement = new IdentifierElement(stringParser.CurrentString);
                        }
                        break;
                }
            }

            return sortElements.Count == 0 ? null : sortElements.ToArray();
        }

    }
}
