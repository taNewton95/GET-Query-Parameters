using QueryParameters.Entities;
using System.Collections.Generic;

namespace QueryParameters.AspNetCore.Mvc.Settings
{
    public static class SyntaxSettings
    {

        private static readonly Dictionary<string, FilterElementCondition> _FilterElementConditionDictionary = new();
        public static IReadOnlyDictionary<string, FilterElementCondition> FilterElementConditionDictionary
        {
            get
            {
                if (_FilterElementConditionDictionary.Count == 0)
                {
                    PopulateFilterElementConditionDictionary();
                }

                return _FilterElementConditionDictionary;
            }
        }

        private static readonly Dictionary<string, FilterElementOperator> _FilterElementOperatorDictionary = new();
        public static IReadOnlyDictionary<string, FilterElementOperator> FilterElementOperatorDictionary
        {
            get
            {
                if (_FilterElementOperatorDictionary.Count == 0)
                {
                    PopulateFilterElementOperatorDictionary();
                }

                return _FilterElementOperatorDictionary;
            }
        }

        #region "Filter"
        public static string Filter = "filter";

        private static string filterEqual = "eq";
        public static string FilterEqual
        {
            get => filterEqual;
            set
            {
                filterEqual = value;
                PopulateFilterElementConditionDictionary();
            }
        }

        private static string filterNotEqual = "neq";
        public static string FilterNotEqual
        {
            get => filterNotEqual;
            set
            {
                filterNotEqual = value;
                PopulateFilterElementConditionDictionary();
            }
        }

        private static string filterLessThan = "lt";
        public static string FilterLessThan
        {
            get => filterLessThan;
            set
            {
                filterLessThan = value;
                PopulateFilterElementConditionDictionary();
            }
        }

        private static string filterLessThanEqual = "lte";
        public static string FilterLessThanEqual
        {
            get => filterLessThanEqual;
            set
            {
                filterLessThanEqual = value;
                PopulateFilterElementConditionDictionary();
            }
        }

        private static string filterGreaterThan = "gt";
        public static string FilterGreaterThan
        {
            get => filterGreaterThan;
            set
            {
                filterGreaterThan = value;
                PopulateFilterElementConditionDictionary();
            }
        }

        private static string filterGreaterThanEqual = "gte";
        public static string FilterGreaterThanEqual
        {
            get => filterGreaterThanEqual;
            set
            {
                filterGreaterThanEqual = value;
                PopulateFilterElementConditionDictionary();
            }
        }

        private static string filterStarts = "starts";
        public static string FilterStarts
        {
            get => filterStarts;
            set
            {
                filterStarts = value;
                PopulateFilterElementConditionDictionary();
            }
        }

        private static string filterEnds = "ends";
        public static string FilterEnds
        {
            get => filterEnds;
            set
            {
                filterEnds = value;
                PopulateFilterElementConditionDictionary();
            }
        }

        private static string filterContains = "contains";
        public static string FilterContains
        {
            get => filterContains;
            set
            {
                filterContains = value;
                PopulateFilterElementConditionDictionary();
            }
        }

        private static string filterAnd = "and";
        public static string FilterAnd
        {
            get => filterAnd;
            set
            {
                filterAnd = value;
                PopulateFilterElementOperatorDictionary();
            }
        }

        private static string filterOr = "or";
        public static string FilterOr
        {
            get => filterOr;
            set
            {
                filterOr = value;
                PopulateFilterElementOperatorDictionary();
            }
        }

        #endregion

        #region "Pagination"
        public static string Take = "take";
        public static string Skip = "skip";
        #endregion

        #region "Sort"
        public static string Sort = "sort";
        public static string SortAscending = "asc";
        public static string SortDescending = "desc";
        #endregion

        private static void PopulateFilterElementConditionDictionary()
        {
            _FilterElementConditionDictionary.Clear();

            _FilterElementConditionDictionary[FilterEqual] = FilterElementCondition.Equal;
            _FilterElementConditionDictionary[FilterNotEqual] = FilterElementCondition.NotEqual;
            _FilterElementConditionDictionary[FilterLessThan] = FilterElementCondition.LessThan;
            _FilterElementConditionDictionary[FilterLessThanEqual] = FilterElementCondition.LessThanEqual;
            _FilterElementConditionDictionary[FilterGreaterThan] = FilterElementCondition.GreaterThan;
            _FilterElementConditionDictionary[FilterGreaterThanEqual] = FilterElementCondition.GreaterThanEqual;
            _FilterElementConditionDictionary[FilterStarts] = FilterElementCondition.Starts;
            _FilterElementConditionDictionary[FilterEnds] = FilterElementCondition.Ends;
            _FilterElementConditionDictionary[FilterContains] = FilterElementCondition.Contains;
        }

        private static void PopulateFilterElementOperatorDictionary()
        {
            _FilterElementOperatorDictionary.Clear();

            _FilterElementOperatorDictionary[FilterAnd] = FilterElementOperator.And;
            _FilterElementOperatorDictionary[FilterOr] = FilterElementOperator.Or;
        }

    }
}