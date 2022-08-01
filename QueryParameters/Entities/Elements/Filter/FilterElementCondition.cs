using QueryParameters.Entities;

namespace QueryParameters.Entities
{
    public class FilterElementCondition
    {

        public static readonly FilterElementCondition Equal = new(Constant.Equal);
        public static readonly FilterElementCondition NotEqual = new(Constant.NotEqual);
        public static readonly FilterElementCondition LessThan = new(Constant.LessThan);
        public static readonly FilterElementCondition LessThanEqual = new(Constant.LessThanEqual);
        public static readonly FilterElementCondition GreaterThan = new(Constant.GreaterThan);
        public static readonly FilterElementCondition GreaterThanEqual = new(Constant.GreaterThanEqual);
        public static readonly FilterElementCondition Between = new(Constant.Between);
        public static readonly FilterElementCondition In = new(Constant.In);
        public static readonly FilterElementCondition NotIn = new(Constant.NotIn);
        public static readonly FilterElementCondition Starts = new(Constant.Starts);
        public static readonly FilterElementCondition Ends = new(Constant.Ends);
        public static readonly FilterElementCondition Contains = new(Constant.Contains);

        internal readonly Constant _Constant;

        private FilterElementCondition(Constant constant)
        {
            _Constant = constant;
        }

        public override string ToString()
        {
            return _Constant switch
            {
                Constant.Equal => "==",
                Constant.NotEqual => "!=",
                Constant.LessThan => "<",
                Constant.LessThanEqual => "<=",
                Constant.GreaterThan => ">",
                Constant.GreaterThanEqual => ">=",
                Constant.Between => "between",
                Constant.In => "in",
                Constant.NotIn => "not in",
                Constant.Starts => "starts",
                Constant.Ends => "ends",
                Constant.Contains => "contains",
                _ => throw new System.NotImplementedException(_Constant.ToString()),
            };
        }

        internal enum Constant
        {
            Equal,
            NotEqual,
            LessThan,
            LessThanEqual,
            GreaterThan,
            GreaterThanEqual,
            Between,
            In,
            NotIn,
            Starts,
            Ends,
            Contains,
        }

    }
}