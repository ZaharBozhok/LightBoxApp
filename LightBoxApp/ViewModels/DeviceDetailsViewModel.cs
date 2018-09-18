using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using LightBoxApp.Models;
using LightBoxApp.Services.AppSettingsManager;
using Prism.Navigation;
using Xamarin.Forms;

namespace LightBoxApp.ViewModels
{
    public class DeviceDetailsViewModel : BaseViewModel
    {
        private readonly IAppSettingsManager _appSettingsManager;
        private readonly IUserDialogs _userDialogs;
        public DeviceDetailsViewModel(INavigationService navigationService, IAppSettingsManager appSettingsManager, IUserDialogs userDialogs) : base(navigationService)
        {
            _appSettingsManager = appSettingsManager;
            _userDialogs = userDialogs;
        }

        private DeviceModel _deviceModel;
        public DeviceModel DeviceModel
        {
            get { return _deviceModel; }
            set { SetProperty(ref _deviceModel, value); }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (parameters.TryGetValue("DeviceModel", out DeviceModel deviceModel))
            {
                DeviceModel = deviceModel;
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);


        }

        public async override void OnNavigatedFrom(NavigationParameters parameters)
        {
            await _appSettingsManager.UpdateDeviceAsync(_deviceModel);
            base.OnNavigatedFrom(parameters);
        }

        private ICommand _RemoveCommand;
        public ICommand RemoveCommand => _RemoveCommand ?? (_RemoveCommand = new Command(OnRemoveCommand));

        public async override void OnBackCommand(object obj)
        {
            await _appSettingsManager.UpdateDeviceAsync(_deviceModel);
            base.OnBackCommand(obj);
        }

        private async void OnRemoveCommand(object obj)
        {
             bool ans = await _userDialogs.ConfirmAsync("Remove the selected item?");
            if (!ans)
                return;
            await _appSettingsManager.RemoveDeviceAsync(_deviceModel);
            await _navigationService.GoBackAsync();
        }

        private ICommand _ConfigureWhenAP;
        public ICommand ConfigureWhenAP => _ConfigureWhenAP ?? (_ConfigureWhenAP = new Command(OnConfigureWhenAP));

        private async void OnConfigureWhenAP(object obj)
        {
            NavigationParameters pairs = new NavigationParameters();
            pairs.Add("Path", _deviceModel.Site + "configs");
            await _navigationService.NavigateAsync("ConfigureAsAPView", pairs);
        }
    }
}
