namespace ClickSharp.Configuration
{
    public static class AppRoles
    {

        private const string _appName = "CS";
        private static readonly List<string> _getList = new()
        {
            Admin,
            Moderator,
            Page.Delete,
            Page.Update,
            Post.Update,
            Post.Delete,
            User.Read,
            User.Write,
            User.Update,
            User.Delete,
            Role.Read,
            Role.Write,
            Role.Delete
        };
        //public const string Admin = $"{_appName}.{nameof(Admin)}";
        public const string Admin = $"admin";
        public const string Moderator = $"{_appName}.{nameof(Moderator)}";
        public class Page
        {
            public const string Update = $"{_appName}.{nameof(Page)}.{nameof(Update)}";
            public const string Delete = $"{_appName}.{nameof(Page)}.{nameof(Delete)}";
        }

        public class Post
        {
            public const string Update = $"{_appName}.{nameof(Post)}.{nameof(Update)}";
            public const string Delete = $"{_appName}.{nameof(Post)}.{nameof(Delete)}";
        }
        public class User
        {
            public const string Read = $"{_appName}.{nameof(User)}.{nameof(Read)}";
            public const string Write = $"{_appName}.{nameof(User)}.{nameof(Write)}";
            public const string Update = $"{_appName}.{nameof(User)}.{nameof(Update)}";
            public const string Delete = $"{_appName}.{nameof(User)}.{nameof(Delete)}";
        }
        public class Role
        {
            public const string Read = $"{_appName}.{nameof(Role)}.{nameof(Read)}";
            public const string Write = $"{_appName}.{nameof(Role)}.{nameof(Write)}";
            public const string Delete = $"{_appName}.{nameof(Role)}.{nameof(Delete)}";
        }
        public static ICollection<string> GetList
        {
            get
            {
                return _getList;
            }
        }
    }
}
