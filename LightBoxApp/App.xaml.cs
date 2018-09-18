using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Navigation;
using LightBoxApp.Views;
using LightBoxApp.Services;
using Prism.Ioc;
using Acr.UserDialogs;
using LightBoxApp.Services.Storage;
using LightBoxApp.Services.AppSettingsManager;
using Plugin.Settings.Abstractions;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LightBoxApp
{
    public partial class App : PrismApplication
    {
        public static T Resolve<T>()
        {
            return (Current as App).Container.Resolve<T>();
        }

        public App(Prism.IPlatformInitializer initializer = null) : base(initializer)
        {

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ControlView()) { BackgroundColor = Color.White };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
            containerRegistry.RegisterInstance(Container.Resolve<IOrientationService>());
            containerRegistry.RegisterInstance<ISettings>(Plugin.Settings.CrossSettings.Current);
            containerRegistry.RegisterInstance<IStorageService>(Container.Resolve<NativeStorageService>());
            containerRegistry.RegisterInstance<IAppSettingsManager>(Container.Resolve<AppSettingsManager>());


            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ControlView>();
            containerRegistry.RegisterForNavigation<SettingsView>();
            containerRegistry.RegisterForNavigation<ConfigureAsAPView>();
            containerRegistry.RegisterForNavigation<AutodetectView>();
            containerRegistry.RegisterForNavigation<DeviceDetailsView>();
            containerRegistry.RegisterForNavigation<PresetsView>();
            containerRegistry.RegisterForNavigation<PresetDetailsView>();
        }

        public static int ScreenWidth;
        public static int ScreenHeight;
    }
}
