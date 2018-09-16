using System;
using LightBoxApp.Services;

namespace LightBoxApp.ViewModels
{
    public class ControlViewModel : BaseViewModel
    {
        private readonly IOrientationService _orientationService;
        public ControlViewModel(IOrientationService orientationService)
        {
            _orientationService = orientationService;
            //_Hello = _orientationService.GetOrientation().ToString();
            _orientationService.SetOrientation(Enums.EDeviceOrientations.Landscape);
            _XAmount = 5;
            _YAmount = 5;
        }

        private int _XAmount;
        public int XAmount
        {
            get { return _XAmount; }
            set { SetProperty(ref _XAmount, value); }
        }

        private int _YAmount;
        public int YAmount
        {
            get { return _YAmount; }
            set { SetProperty(ref _YAmount, value); }
        }
    }
}
