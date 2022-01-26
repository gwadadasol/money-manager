namespace AnalyzerService.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public static class  Analyzer
        {
            public const string GetAmountsCategories = Base + "/analyzer/amounts/categories";
        }
    }
}