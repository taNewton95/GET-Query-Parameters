using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using QueryParameters.AspNetCore.Mvc.Parsers;
using QueryParameters.AspNetCore.Mvc.Settings;
using QueryParameters.Entities;
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

        public static FilterParameter Filter(HttpRequest httpRequest)
        {
            return Filter(httpRequest.Query);
        }

        public static FilterParameter Filter(IQueryCollection queryCollection)
        {
            var newInstance = new FilterParameter();

            

            return newInstance;
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

        public static SortParameter Sort(HttpRequest httpRequest, SortSettings sortSettings = null)
        {
            var sortStringVals = httpRequest.Query[SyntaxSettings.SortName];

            if (!sortStringVals.Any()) return null;

            return Sort(sortStringVals.First(), sortSettings: sortSettings);
        }

        public static SortParameter Sort(string sort, SortSettings sortSettings = null)
        {
            if (string.IsNullOrEmpty(sort)) return null;

            // Use the default settings if not overridden
            sortSettings ??= SortSettings.Default;

            var sortParameter = new SortParameter();

            ISortParser sortParser = new DefaultSortParser();

            sortParameter.Elements.AddRange(sortParser.Parse(sort));

            return sortParameter;
        }

    }
}
