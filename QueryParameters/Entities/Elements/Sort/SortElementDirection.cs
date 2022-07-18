using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Entities
{ 
    public class SortElementDirection
    {

        public static readonly SortElementDirection Ascending = new(Constant.Ascending);
        public static readonly SortElementDirection Descending = new(Constant.Descending);

        internal readonly Constant _Constant;

        private SortElementDirection(Constant constant)
        {
            _Constant = constant;
        }

        public override string ToString()
        {
            return _Constant switch
            {
                Constant.Ascending => "asc",
                Constant.Descending => "desc",
                _ => throw new System.NotImplementedException(_Constant.ToString()),
            };
        }

        internal enum Constant
        {
            Ascending,
            Descending,
        }

    }
}
