namespace QueryParameters.AspNetCore.Mvc.Settings
{
    public static class SyntaxSettings
    {

        public static string OperatorDelimiter = ":";

        #region "Pagination"
        public static string PaginationTakeName = "take";
        public static string PaginationSkipName = "skip";
        #endregion

        #region "Sort"
        public static string SortName = "sort";
        public static string SortAscendingOperator = "asc";
        public static string SortDescendingOperator = "desc";
        #endregion

    }
}