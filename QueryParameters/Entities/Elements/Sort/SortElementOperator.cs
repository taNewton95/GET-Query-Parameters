using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.Entities
{
    public class SortElementOperator
    {

        public static readonly SortElementOperator Default = new(Constant.Default);
        public static readonly SortElementOperator Length = new(Constant.Length);

        private readonly Constant _Constant;

        private SortElementOperator(Constant constant)
        {
            _Constant = constant;
        }

        public override string ToString()
        {
            return _Constant switch
            {
                Constant.Default => "",
                Constant.Length => "length",
                _ => throw new System.NotImplementedException(_Constant.ToString()),
            };
        }

        private enum Constant
        {
            Default,
            Length,
        }

    }
}
