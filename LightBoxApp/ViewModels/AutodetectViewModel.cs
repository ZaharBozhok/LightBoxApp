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

        private ICommand _SaveCommand;
        public ICommand SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(OnSaveCommand));

        private async void OnSaveCommand(object obj)
        {
            _userDialogs.ShowLoading("Saving...");
            try
            {
                foreach (var dev in Devices)
                {
                    await _appSettingsManager.UpdateDeviceAsync(dev);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
        }

        private ICommand _ItemTappedCommand;
        public ICommand ItemTappedCommand => _ItemTappedCommand ?? (_ItemTappedCommand = new Command(OnItemTappedCommand));

        private async void OnItemTappedCommand(object obj)
        {
            bool ans = await _userDialogs.ConfirmAsync("Remove the selected item?");
            if (!ans)
                return;
            var item = (DeviceModel)obj;
            _userDialogs.ShowLoading("Removing...");
            try
            {
                Devices.Remove(item);
                await _appSettingsManager.RemoveDeviceAsync(item);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _userDialogs.ShowLoading("Loading...");
            try
            {
                var res = await _appSettingsManager.GetDevicesAsync();
                Devices = new ObservableCollection<DeviceModel>(res);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                _userDialogs.HideLoading();
            }

        }
    }
}
