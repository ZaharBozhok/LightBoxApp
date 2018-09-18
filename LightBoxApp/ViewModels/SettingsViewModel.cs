using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
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

        private async void OnManualMAC(object obj)
        {
            NavigationParameters pairs = new NavigationParameters();
            pairs.Add("Path", FormattedMac + Constants.ConfigsPath);
            await _navigationService.NavigateAsync("ConfigureAsAPView",pairs);
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
        private string _ManualMac;
        public string ManualMac
        {
            get { return _ManualMac; }
            set { SetProperty(ref _ManualMac, value); RaisePropertyChanged(nameof(FormattedMac)); }
        }

        public string FormattedMac
        {
            get { return "http://lightbox" + MacToPath(_ManualMac) + ".local"; }
        }

        private string MacToPath(string mac)
        {
            string res = default(String);
            if(!string.IsNullOrEmpty(mac))
                res = Regex.Replace(mac, ":", "").ToLower();
            return res;
        }

    }
}

