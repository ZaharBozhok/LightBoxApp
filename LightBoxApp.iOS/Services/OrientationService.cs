using System;
using LightBoxApp.Enums;
using LightBoxApp.Services;
using UIKit;
using LightBoxApp.iOS.Services;
using Xamarin.Forms;
using Foundation;

[assembly: Dependency(typeof(OrientationService))]
namespace LightBoxApp.iOS.Services
{
    public class OrientationService : IOrientationService
    {
        public OrientationService(){}
        public EDeviceOrientations GetOrientation()
        {
            var currentOrientation = UIApplication.SharedApplication.StatusBarOrientation;
            bool isPortrait = currentOrientation == UIInterfaceOrientation.Portrait
                || currentOrientation == UIInterfaceOrientation.PortraitUpsideDown;

            return isPortrait ? EDeviceOrientations.Portrait : EDeviceOrientations.Landscape;
        }

        public void SetOrientation(EDeviceOrientations orientation)
        {
            switch (orientation)
            {
                case EDeviceOrientations.Landscape:
                    UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
                    break;
                case EDeviceOrientations.Portrait:
                    UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
                    break;
            }
            //UIDevice.CurrentDevice.SetValueForKey(newNSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
        }
    }
}
