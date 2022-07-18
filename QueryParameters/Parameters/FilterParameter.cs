using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QueryParameters.Entities;
using QueryParameters.Settings;

namespace QueryParameters.Parameters
{
    public class FilterParameter : BaseParameter
    {

        public readonly FilterElementCollection Elements = new();

        public override bool IsPopulated()
        {
            return Elements.Any();
        }

    }
}