namespace TestCommon.TestConstants;

public static partial class Constants
{
    public static class User
    {
        public static readonly Guid Id = Guid.NewGuid();
        //public static Guid Id = new("2150e333-8fdc-42a3-9474-1a3956d46de8");
        public const string DefaultName = "Test User";
        public const string DefaultEmail = "sss@gmail.com";
        public const string DefaultRole = "Standard";
        public const string DefaultPassword = "1234";
    }
}