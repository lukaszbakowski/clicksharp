namespace ClickSharp.Configuration
{
    public static class AppUrls
    {
        private const string _appName = "CS";

        public const string AdminLogin = $"/{_appName}-Admin";
        public const string AppImages = $"/{_appName}-Images";

        public class Admin
        {
            public const string Main = $"/{nameof(Admin)}/";
            public class EN
            {
                public const string ManageUsers = $"/{nameof(Admin)}/{nameof(ManageUsers)}";
                public const string EditProfile = $"/{nameof(Admin)}/{nameof(EditProfile)}";
                public const string DashBoard = $"/{nameof(Admin)}/{nameof(DashBoard)}";
                public const string EditPage = $"/{nameof(Admin)}/{nameof(EditPage)}";
                public const string ImageUpload = $"/{nameof(Admin)}/{nameof(ImageUpload)}";
                public const string EditMenu = $"/{nameof(Admin)}/{nameof(EditMenu)}";
            }
            public class PL
            {
                public const string ZarzadzajUzytkownikami = $"/{nameof(Admin)}/{nameof(ZarzadzajUzytkownikami)}";
                public const string EdytujProfil = $"/{nameof(Admin)}/{nameof(EdytujProfil)}";
                public const string PanelZarzadzania = $"/{nameof(Admin)}/{nameof(PanelZarzadzania)}";
                public const string EdytujStrone = $"/{nameof(Admin)}/{nameof(EdytujStrone)}";
                public const string ZaladujObraz = $"/{nameof(Admin)}/{nameof(ZaladujObraz)}";
                public const string EdytujMenu = $"/{nameof(Admin)}/{nameof(EdytujMenu)}";
            }
        }
    }
}
