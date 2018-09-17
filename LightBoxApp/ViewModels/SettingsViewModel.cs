using System;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace LightBoxApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {

        public SettingsViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private ICommand _ConfigureWhenAP;
        public ICommand ConfigureWhenAP => _ConfigureWhenAP ?? (_ConfigureWhenAP = new Command(OnConfigureWhenAP));

        private async void OnConfigureWhenAP(object obj)
        {
            await _navigationService.NavigateAsync("ConfigureAsAPView");
        }

        private ICommand _ManualMAC;
        public ICommand ManualMAC => _ManualMAC ?? (_ManualMAC = new Command(OnManualMAC));

        private void OnManualMAC(object obj)
        {
            Debug.WriteLine("OnManualMAC");
        }

        private ICommand _Autodetect;
        public ICommand Autodetect => _Autodetect ?? (_Autodetect = new Command(OnAutodetect));

        private async void OnAutodetect(object obj)
        {
            await _navigationService.NavigateAsync("AutodetectView");
        }

        private ICommand _Placement;
        public ICommand Placement => _Placement ?? (_Placement = new Command(OnPlacement));

        private void OnPlacement(object obj)
        {
            Debug.WriteLine("OnPlacement");
        }
    }
}

