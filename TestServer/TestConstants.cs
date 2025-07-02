namespace TestServer;

/// <summary>
/// Constants used across test classes to avoid magic numbers and improve maintainability
/// </summary>
public static class TestConstants
{
    public const int DefaultUserId = 1;
    public const int NonExistentUserId = 999999;
    public const string DefaultUsername = "TestUser";
    public const string DefaultLanguageLevel = "B2";
    public const string InvalidLanguageLevel = "Z9";
    public const int DefaultNounLimit = 20;
    public const int SmallNounLimit = 5;
    
    public static class HttpStatusCodes
    {
        public const int Ok = 200;
        public const int BadRequest = 400;
        public const int NotFound = 404;
    }
    
    public static class ContentTypes
    {
        public const string ApplicationJson = "application/json";
    }
    
    public static class ApiEndpoints
    {
        public const string UserById = "/user/v1/{0}";
        public const string LearnNouns = "/learn/v1/{0}/noun";
        public const string LearningProgress = "/learn/v1/{0}/progress";
    }
}