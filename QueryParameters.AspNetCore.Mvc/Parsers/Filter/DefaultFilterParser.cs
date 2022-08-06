using QueryParameters.AspNetCore.Mvc.Exceptions;
using QueryParameters.AspNetCore.Mvc.Settings;
using QueryParameters.Entities;
using QueryParameters.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                if (stringParser.MatchedDelimiter == '(')
                {
                    filterCollection.BeginScope();
                    continue;
                }

                if (stringParser.MatchedDelimiter == ')')
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
                        throw new UnknownOperatorException($"Unknown operator '{stringParser.CurrentString}'", stringParser.CurrentString);
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

        /// <summary>
        /// Parse the input string based on the property/field it is being applied to.
        /// </summary>
        /// <param name="identifierElement"></param>
        /// <param name="conditionElement"></param>
        /// <param name="stringParser"></param>
        /// <returns></returns>
        private FilterElementValue ParseValueElement(IdentifierElement identifierElement, FilterElementCondition conditionElement, StringParser stringParser)
        {
            var expectedType = PropertyFactory.FieldOrPropertyType<T>(identifierElement.Identifier);

            switch (expectedType)
            {
                case Type boolType when boolType == typeof(bool):
                case Type boolNullType when boolNullType == typeof(bool?):
                    if (bool.TryParse(stringParser.CurrentString, out var boolResult)) return new FilterElementValue(boolResult);
                    break;

                case Type ushortType when ushortType == typeof(ushort):
                case Type ushortNullType when ushortNullType == typeof(ushort?):
                    if (ushort.TryParse(stringParser.CurrentString, out var ushortResult)) return new FilterElementValue(ushortResult);
                    break;

                case Type shortType when shortType == typeof(short):
                case Type shortNullType when shortNullType == typeof(short?):
                    if (short.TryParse(stringParser.CurrentString, out var shortResult)) return new FilterElementValue(shortResult);
                    break;

                case Type uintType when uintType == typeof(uint):
                case Type uintNullType when uintNullType == typeof(uint?):
                    if (uint.TryParse(stringParser.CurrentString, out var uintResult)) return new FilterElementValue(uintResult);
                    break;

                case Type intType when intType == typeof(int):
                case Type intNullType when intNullType == typeof(int?):
                    if (int.TryParse(stringParser.CurrentString, out var intResult)) return new FilterElementValue(intResult);
                    break;

                case Type ulongType when ulongType == typeof(ulong):
                case Type ulongNullType when ulongNullType == typeof(ulong?):
                    if (ulong.TryParse(stringParser.CurrentString, out var ulongResult)) return new FilterElementValue(ulongResult);
                    break;

                case Type longType when longType == typeof(long):
                case Type longNullType when longNullType == typeof(long?):
                    if (long.TryParse(stringParser.CurrentString, out var longResult)) return new FilterElementValue(longResult);
                    break;

                case Type decimalType when decimalType == typeof(decimal):
                case Type decimalNullType when decimalNullType == typeof(decimal?):
                    if (decimal.TryParse(stringParser.CurrentString, out var decResult)) return new FilterElementValue(decResult);
                    break;

                case Type doubleType when doubleType == typeof(double):
                case Type doubleNullType when doubleNullType == typeof(double?):
                    if (double.TryParse(stringParser.CurrentString, out var doubleResult)) return new FilterElementValue(doubleResult);
                    break;

                case Type floatType when floatType == typeof(float):
                case Type floatNullType when floatNullType == typeof(float?):
                    if (float.TryParse(stringParser.CurrentString, out var floatResult)) return new FilterElementValue(floatResult);
                    break;

                case Type charType when charType == typeof(char):
                case Type charNullType when charNullType == typeof(char?):
                    if (char.TryParse(stringParser.CurrentString, out var charResult)) return new FilterElementValue(charResult);
                    break;

                case Type stringType when stringType == typeof(string):
                    {
                        var stringResult = stringParser.CurrentString;

                        if (stringResult.StartsWith('\'') && stringResult.EndsWith('\''))
                        {
                            stringResult = stringResult.Substring(1, stringResult.Length - 2);
                        }

                        return new FilterElementValue(stringResult);
                    }

                case Type dateTimeType when dateTimeType == typeof(DateTime):
                case Type dateTimeNullType when dateTimeNullType == typeof(DateTime?):
                    {
                        var stringResult = stringParser.CurrentString;

                        if (stringResult.StartsWith('\'') && stringResult.EndsWith('\''))
                        {
                            stringResult = stringResult.Substring(1, stringResult.Length - 2);
                        }

                        if (DateTime.TryParse(stringResult, out var dateTimeResult)) return new FilterElementValue(dateTimeResult);
                        break;
                    }
            }

            return null;
        }

    }
}
