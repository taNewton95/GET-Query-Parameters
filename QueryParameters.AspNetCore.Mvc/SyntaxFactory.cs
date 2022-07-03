using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using QueryParameters.AspNetCore.Mvc.Settings;
using QueryParameters.Parameters;
using QueryParameters.Settings;

namespace QueryParameters.AspNetCore.Mvc
{
    public static class SyntaxFactory
    {

        public static ParameterHandler<T> Handler<T>(HttpRequest httpRequest, PaginationSettings paginationSettings = null, SortSettings sortSettings = null)
        {
            return new ParameterHandler<T>
            {
                Pagination = Pagination(httpRequest, paginationSettings: paginationSettings),
                Sort = Sort(httpRequest, sortSettings: sortSettings),
            };
        }

        public static PaginationParameter Pagination(HttpRequest httpRequest, PaginationSettings paginationSettings = null)
        {           
            return Pagination(httpRequest.Query, paginationSettings: paginationSettings);
        }

        public static PaginationParameter Pagination(IQueryCollection queryCollection, PaginationSettings paginationSettings = null)
        {
            // Use the default settings if not overridden
            paginationSettings ??= PaginationSettings.Default;

            var newInstance = new PaginationParameter();
            newInstance.Skip = paginationSettings.DefaultSkip;
            newInstance.Take = paginationSettings.DefaultTake;

            var skip = queryCollection[SyntaxSettings.PaginationSkipName];
            if (skip.Count > 0)
            {
                if (!int.TryParse(skip[0], out newInstance.Skip))
                {
                    newInstance.Skip = paginationSettings.DefaultSkip;
                }
            }

            var take = queryCollection[SyntaxSettings.PaginationTakeName];
            if (take.Count > 0)
            {
                if (!int.TryParse(take[0], out newInstance.Take))
                {
                    newInstance.Take = paginationSettings.DefaultTake;
                }
            }

            return newInstance;
        }

        public static IEnumerable<SortParameter> Sort(HttpRequest httpRequest, SortSettings sortSettings = null)
        {
            return Sort(httpRequest.Query[SyntaxSettings.SortName], sortSettings: sortSettings);
        }

        public static IEnumerable<SortParameter> Sort(StringValues sort, SortSettings sortSettings = null)
        {
            if (sort.Count == 0) return Enumerable.Empty<SortParameter>();

            // Use the default settings if not overridden
            sortSettings ??= SortSettings.Default;

            var sortParameters = new List<SortParameter>();

            foreach (var field in sort)
            {
                var sortParameter = new SortParameter();
                sortParameter.Direction = sortSettings.DefaultDirection;

                var directionOperatorIndex = field.IndexOf(SyntaxSettings.OperatorDelimiter);

                if (directionOperatorIndex == -1) // There is no operator
                {
                    sortParameter.Field = field;
                }
                else
                {
                    sortParameter.Field = field.Substring(0, directionOperatorIndex);

                    var parsedSortDirection = SortDirection(field.Substring(directionOperatorIndex + 1));
                    if (parsedSortDirection != null)
                    {
                        sortParameter.Direction = parsedSortDirection.Value;
                    }
                }

                sortParameters.Add(sortParameter);
            }

            return sortParameters;
        }

        public static SortDirection? SortDirection(string sortOperator)
        {
            if (sortOperator.Equals(SyntaxSettings.SortAscendingOperator, StringComparison.CurrentCultureIgnoreCase))
            {
                return QueryParameters.Settings.SortDirection.Ascending;
            }
            else if (sortOperator.Equals(SyntaxSettings.SortDescendingOperator, StringComparison.CurrentCultureIgnoreCase))
            {
                return QueryParameters.Settings.SortDirection.Descending;
            }
            else
            {
                return null;
            }
        }

    }
}
