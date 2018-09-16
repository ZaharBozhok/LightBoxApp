using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using LightBoxApp.Droid.Services;
using LightBoxApp.Enums;
using LightBoxApp.Services;
using Xamarin.Forms;

namespace LightBoxApp.Droid.Services
{
	public class OrientationService : IOrientationService
    {
        private readonly Context _context;

        public OrientationService(Context context)
        {
            _context = context;
        }

        public EDeviceOrientations GetOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            var rotation = windowManager.DefaultDisplay.Rotation;
            bool isLandscape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;
            return isLandscape ? EDeviceOrientations.Landscape : EDeviceOrientations.Portrait;
        }

        public void SetOrientation(EDeviceOrientations orientation)
        {
            switch(orientation)
            {
                case EDeviceOrientations.Landscape:
                    ((MainActivity)_context).RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
                    break;
                case EDeviceOrientations.Portrait:
                    ((MainActivity)_context).RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
                    break;
            }
        }
    }
}
