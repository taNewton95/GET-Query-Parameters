using QueryParameters.Entities;
using QueryParameters.Parameters;
using QueryParameters.Properties;
using QueryParameters.Settings;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters
{
    public partial class ParameterHandler<T>
    {

        public IQueryable<T> ApplyFilter(IQueryable<T> queryable)
        {
            if (Filter == null) return queryable;

            foreach (var element in Filter.Elements)
            {
                // TODO: Implement the actual filtering
                Console.Write("");
            }

            // foreach (var item in Filter)
            // {
            //     if (!item.IsPopulated()) continue;


            // }

            return queryable;
        }
    }
}
