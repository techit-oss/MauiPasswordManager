namespace MauiPasswordManager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MauiPasswordManager.Pages.MainPage());
        }
    }

}
