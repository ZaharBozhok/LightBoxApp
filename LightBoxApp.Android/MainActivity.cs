using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism;
using Prism.Ioc;
using LightBoxApp.Services;
using LightBoxApp.Droid.Services;
using Android.Content;
using NControl.Droid;
using NControl.Controls.Droid;
using Acr.UserDialogs;

namespace LightBoxApp.Droid
{
    [Activity(Label = "LightBoxApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            App.ScreenWidth = (int)((double)Resources.DisplayMetrics.WidthPixels / (double)Resources.DisplayMetrics.Density);
            App.ScreenHeight = (int)((double)Resources.DisplayMetrics.HeightPixels / (double)Resources.DisplayMetrics.Density);
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            NControlViewRenderer.Init();
            NControls.Init();
            UserDialogs.Init(this);
            LoadApplication(new App(new AndroidInitializer(this)));
        }
        public class AndroidInitializer : IPlatformInitializer
        {
            private Context _context;

            public AndroidInitializer(Context context)
            {
                _context = context;
            }
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterInstance<IOrientationService>(new OrientationService(_context));
            }
        }
    }
}