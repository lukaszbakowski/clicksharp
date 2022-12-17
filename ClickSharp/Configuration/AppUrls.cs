namespace ClickSharp.Configuration
{
    public static class AppUrls
    {
        private const string _appName = "CS";

        public const string AdminLogin = $"/{_appName}-Admin";
        public class EN
        {
            public class Admin
            {
                public const string ManageUsers = $"/{nameof(Admin)}/{nameof(ManageUsers)}";
                public const string EditProfile = $"/{nameof(Admin)}/{nameof(EditProfile)}";
                public const string DashBoard = $"/{nameof(Admin)}/{nameof(DashBoard)}";
                public const string EditPage = $"/{nameof(Admin)}/{nameof(EditPage)}";
                public const string AddPage = $"/{nameof(Admin)}/{nameof(AddPage)}";
                public const string ImageUpload = $"/{nameof(Admin)}/{nameof(ImageUpload)}";
            }
        }
        public class PL
        {
            public class Admin
            {
                public const string ZarzadzajUzytkownikami = $"/{nameof(Admin)}/{nameof(ZarzadzajUzytkownikami)}";
                public const string EdytujProfile = $"/{nameof(Admin)}/{nameof(EdytujProfile)}";
                public const string DashBoard = $"/{nameof(Admin)}/{nameof(DashBoard)}";
                public const string EdytujStrone = $"/{nameof(Admin)}/{nameof(EdytujStrone)}";
                public const string DodajStrone = $"/{nameof(Admin)}/{nameof(DodajStrone)}";
                public const string ZaladujObraz = $"/{nameof(Admin)}/{nameof(ZaladujObraz)}";
            }
        }
    }
}
