namespace ClickSharp.Configuration
{
    public static class AppRoles
    {
        private const string _appName = "CS";
        public const string CsAdmin = "CS.Admin";
        public const string CsModerator = "CS.Moderator";
        public class Page
        {
            public const string Update = "CS.Page.Update";
            public const string Delete = "CS.Page.Delete";
        }

        public class Post
        {
            public const string Update = "CS.Post.Update";
            public const string Delete = "CS.Post.Delete";
        }
        public class User
        {
            public const string Read = $"{_appName}.{nameof(User)}.{nameof(Read)}";
            public const string Write = "CS.User.Write";
            public const string Update = "CS.User.Update";
            public const string Delete = "CS.User.Delete";
        }
        public class Role
        {
            public const string Read = "CS.Role.Read";
            public const string Write = "CS.Role.Write";
            public const string Delete = "CS.Role.Delete";
        }
    }
}
