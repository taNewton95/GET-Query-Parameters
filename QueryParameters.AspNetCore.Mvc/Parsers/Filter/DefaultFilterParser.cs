using QueryParameters.AspNetCore.Mvc.Exceptions;
using QueryParameters.AspNetCore.Mvc.Settings;
using QueryParameters.Entities;
using QueryParameters.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.AspNetCore.Mvc.Parsers
{
    public class DefaultFilterParser<T> : IFilterParser
    {

        public FilterElementCollection Parse(string filter)
        {
            var filterCollection = new FilterElementCollection();
            var stringParser = new StringParser(filter);

            IdentifierElement identifierElement = null;
            FilterElementCondition conditionElement = null;
            FilterElementValue valueElement = null;

            bool expectingOperator = false;

            void AddFilterElement()
            {
                filterCollection.Add(new FilterElementExpression(identifierElement, conditionElement, ParseValueElement(identifierElement, conditionElement, stringParser)));
                identifierElement = null;
                conditionElement = null;
                valueElement = null;
                expectingOperator = true;
            };

            while (stringParser.Next(new HashSet<char>(new[] { '(', ')', ' ' })))
            {
                if (stringParser.MatchedChar == '(')
                {
                    filterCollection.BeginScope();
                    continue;
                }

                if (stringParser.MatchedChar == ')')
                {
                    if (identifierElement != null)
                    {
                        AddFilterElement();
                    }

                    filterCollection.EndScope();
                    continue;
                }

                if (string.IsNullOrEmpty(stringParser.CurrentString)) continue;

                if (expectingOperator)
                {
                    if (!SyntaxSettings.FilterElementOperatorDictionary.TryGetValue(stringParser.CurrentString, out var operatorElement))
                    {
                        throw new UnknownConditionException($"Unknown operator '{stringParser.CurrentString}'", stringParser.CurrentString);
                    }

                    filterCollection.Add(operatorElement);
                    expectingOperator = false;
                }
                else if (identifierElement == null)
                {
                    identifierElement = new IdentifierElement(stringParser.CurrentString);
                }
                else if (conditionElement == null)
                {
                    if (!SyntaxSettings.FilterElementConditionDictionary.TryGetValue(stringParser.CurrentString, out conditionElement))
                    {
                        throw new UnknownConditionException($"Unknown condition '{stringParser.CurrentString}'", stringParser.CurrentString);
                    }
                }
                else if (valueElement == null)
                {
                    AddFilterElement();
                }
            }

            return filterCollection;
        }

        private FilterElementValue ParseValueElement(IdentifierElement identifierElement, FilterElementCondition conditionElement, StringParser stringParser)
        {
            var expectedType = PropertyFactory.FieldOrPropertyType<T>(identifierElement.Identifier);

            if (stringParser.CurrentString.StartsWith('\''))
            {
                return new FilterElementValue(stringParser.CurrentString.Substring(1, stringParser.CurrentString.Length - 2));
            }
            else
            {
                if (stringParser.CurrentString.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new FilterElementValue(true);
                }

                if (stringParser.CurrentString.Equals("false", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new FilterElementValue(false);
                }

                if (stringParser.CurrentString.Contains('.'))
                {
                    if (decimal.TryParse(stringParser.CurrentString, out var decResult)) return new FilterElementValue(decResult);
                }
                else
                {
                    if (expectedType == typeof(short))
                    {
                        if (short.TryParse(stringParser.CurrentString, out var shortResult)) return new FilterElementValue(shortResult);
                    }
                    else if (expectedType == typeof(int))
                    {
                        if (int.TryParse(stringParser.CurrentString, out var intResult)) return new FilterElementValue(intResult);
                    }
                    else if (expectedType == typeof(long))
                    {
                        if (long.TryParse(stringParser.CurrentString, out var longResult)) return new FilterElementValue(longResult);
                    }
                }
            }

            return null;
        }

    }
}
