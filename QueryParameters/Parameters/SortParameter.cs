using QueryParameters.Entities;
using QueryParameters.Settings;
using System.Linq;

namespace QueryParameters.Parameters
{
    public class SortParameter : BaseParameter
    {

        public readonly SortElementCollection Elements = new();

        public override bool IsPopulated()
        {
            return Elements.Any();
        }

    }
}