using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.AspNetCore.Mvc.Extensions
{
    public static class ControllerBaseExtensions
    {

        public static IQueryable<T> ApplyGetQueryParameters<T>(this ControllerBase controllerBase, IQueryable<T> items)
        {
            return ParameterFactory.Handler<T>(controllerBase.Request).Apply(items);
        }

    }
}
