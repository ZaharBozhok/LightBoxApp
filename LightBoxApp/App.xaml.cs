using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Navigation;
using LightBoxApp.Views;
using LightBoxApp.Services;
using Prism.Ioc;

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
            containerRegistry.RegisterInstance(Container.Resolve<IOrientationService>());

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ControlView>();
        }
    }
}
