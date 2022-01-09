namespace CategoryService.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public static class  Rule
        {
            public const string GetAll = Base + "/rules";
            public const string Delete = Base + "/rules/{ruleId}";
            public const string Get = Base + "/rules/{categoryName}";
            public const string Create = Base + "/rules";            
        }

        public static class Category
        {
            public const string GetAll = Base + "/categories";
            //public const string Update = Base + "/categories/{categoryId}";
            public const string Delete = Base + "/categories/{categoryId}";
            public const string Get = Base + "/categories/{categoryId}";
            public const string GetByDescription = Base + "/categories/description";
            public const string Create = Base + "/categories/{categoryName}";

        }

        
    }
}