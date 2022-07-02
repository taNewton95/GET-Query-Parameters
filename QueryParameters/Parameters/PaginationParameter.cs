using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Parameters
{
    public class PaginationParameter : BaseParameter
    {

        public int Take;
        public int Skip;

        public override bool IsPopulated()
        {
            return Skip > 0 | Take > 0;
        }

    }
}
