namespace LangVoyageServer.Configuration;

public static class AppConstants
{
    public static class Database
    {
        public const int DefaultNounLimit = 20;
        public const int MaxNounLimit = 10000;
    }

    public static class Cors
    {
        public const string PolicyName = "LangVoyageAppCorsPolicy";
    }

    public static class Environment
    {
        public const string TestEnvironmentVariable = "IS_TEST_ENVIRONMENT";
    }

    public static class Api
    {
        public static class Routes
        {
            public const string Learning = "/learn/v1";
            public const string UserProfile = "/user/v1";
        }
    }
}