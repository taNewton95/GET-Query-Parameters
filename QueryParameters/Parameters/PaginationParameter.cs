using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryParameters.Settings;

namespace QueryParameters.Parameters
{
    public class PaginationParameter : BaseParameter
    {

        public int Take = PaginationSettings.Default.DefaultTake;
        public int Skip = PaginationSettings.Default.DefaultSkip;

        public override bool IsPopulated()
        {
            return Skip > 0 | Take > 0;
        }

    }
}
