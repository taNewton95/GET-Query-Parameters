using Microsoft.AspNetCore.Http;
using QueryParameters.Parameters;

namespace QueryParameters.AspNetCore.Mvc
{
    public static class ParameterFactory
    {

        public static ParameterHandler<T> Handler<T>(HttpRequest httpRequest)
        {
            return new ParameterHandler<T>
            {
                Pagination = Pagination(httpRequest)
            };
        }

        public static PaginationParameter Pagination(HttpRequest httpRequest)
        {
            var newInstance = new PaginationParameter();

            var skip = httpRequest.Query["skip"];
            if (skip.Count > 0)
            {
                _ = int.TryParse(skip[0], out newInstance.Skip);
            }

            var take = httpRequest.Query["take"];
            if (take.Count > 0)
            {
                _ = int.TryParse(take[0], out newInstance.Take);
            }

            return newInstance;
        }

    }
}