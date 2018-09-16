using System;
using LightBoxApp.Enums;

namespace LightBoxApp.Services
{
    public interface IOrientationService
    {
        EDeviceOrientations GetOrientation();
        void SetOrientation(EDeviceOrientations orientation);
    }
}
