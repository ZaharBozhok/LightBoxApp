using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using LightBoxApp.Models;
using LightBoxApp.Services.AppSettingsManager;
using LightBoxApp.Services.Storage;
using Prism.Navigation;
using Xamarin.Forms;
using System.Linq;

namespace LightBoxApp.ViewModels
{
    public class AutodetectViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IAppSettingsManager _appSettingsManager;

        public AutodetectViewModel(INavigationService navigationService, IUserDialogs userDialogs, IAppSettingsManager appSettingsManager):base(navigationService)
        {
            _userDialogs = userDialogs;
            _appSettingsManager = appSettingsManager;
        }

        private ObservableCollection<DeviceModel> _Devices;
        public ObservableCollection<DeviceModel> Devices
        {
            get { return _Devices; }
            set { SetProperty(ref _Devices, value); }
        }

        private ICommand _ItemTappedCommand;
        public ICommand ItemTappedCommand => _ItemTappedCommand ?? (_ItemTappedCommand = new Command(OnItemTappedCommand));

        private async void OnItemTappedCommand(object obj)
        {
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("DeviceModel", (DeviceModel)obj);
            await _navigationService.NavigateAsync("DeviceDetailsView", navigationParameters);
        }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            var res = await _appSettingsManager.GetDevicesAsync();
            Devices = new ObservableCollection<DeviceModel>(res.OrderBy((arg) => arg.Mac));
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            //_userDialogs.ShowLoading("Loading...");
            //try
            //{
            //    var res = await _appSettingsManager.GetDevicesAsync();
            //    Devices = new ObservableCollection<DeviceModel>(res.OrderBy((arg) => arg.Mac));
            //}
            //catch(Exception ex)
            //{

            //}
            //finally
            //{
            //    _userDialogs.HideLoading();
            //}

        }
    }
}
