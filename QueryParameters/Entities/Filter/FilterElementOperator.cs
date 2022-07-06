using QueryParameters.Entities;
using QueryParameters.Settings;

namespace QueryParameters.Parameters
{
    public class FilterElementOperator : FilterElement
    {

        public static readonly FilterElementOperator Or = new(Constant.Or);
        public static readonly FilterElementOperator And = new(Constant.And);

        private readonly Constant _Constant;

        private FilterElementOperator(Constant constant)
        {
            _Constant = constant;
        }

        public override string ToString()
        {
            return _Constant switch
            {
                Constant.Or => "or",
                Constant.And => "and",
                _ => throw new System.NotImplementedException(_Constant.ToString()),
            };
        }

        private enum Constant
        {
            Or,
            And,
        }

    }
}