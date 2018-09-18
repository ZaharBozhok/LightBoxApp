using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using LightBoxApp.Controls;
using LightBoxApp.Models;
using LightBoxApp.Services;
using LightBoxApp.Services.AppSettingsManager;
using Prism.Navigation;
using Xamarin.Forms;

namespace LightBoxApp.ViewModels
{
    public class ControlViewModel : BaseViewModel
    {
        private readonly IOrientationService _orientationService;
        private readonly IAppSettingsManager _appSettingsManager;
        private readonly IUserDialogs _userDialogs;
        public ControlViewModel(IOrientationService orientationService,
                                INavigationService navigationService,
                                IAppSettingsManager appSettingsManager, IUserDialogs userDialogs) : base(navigationService)
        {
            _orientationService = orientationService;
            _appSettingsManager = appSettingsManager;
            _userDialogs = userDialogs;
            _orientationService.SetOrientation(Enums.EDeviceOrientations.Landscape);
            _XAmount = Constants.XAmount;
            _YAmount = Constants.YAmount;
            LoadSites();
        }

        private void LoadSites()
        {
            Task.Run(async () =>
            {
                deviceModels = await _appSettingsManager.GetDevicesAsync();
                if (deviceModels == null)
                    deviceModels = new HashSet<DeviceModel>();
            });
        }

        private HashSet<DeviceModel> deviceModels;

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

        private List<StateModel> _FirstStates;
        public List<StateModel> FirstStates
        {
            get { return _FirstStates; }
            set { SetProperty(ref _FirstStates, value); }
        }
        private List<StateModel> _SecondStates;
        public List<StateModel> SecondStates
        {
            get { return _SecondStates; }
            set { SetProperty(ref _SecondStates, value); }
        }

        private List<StateModel> _ThirdStates;
        public List<StateModel> ThirdStates
        {
            get { return _ThirdStates; }
            set { SetProperty(ref _ThirdStates, value); }
        }

        private ICommand _SendCommand;
        public ICommand SendCommand => _SendCommand ?? (_SendCommand = new Command(OnSendCommand));

        private string StatesToString(List<StateModel> models)
        {
            string ret = "";
            foreach (var model in models)
            {
                ret += model.State == true ? '1' : '0';
            }
            ret += "0000000";
            return ret;
        }

        private async void OnSendCommand(object obj)
        {
            var data1 = StatesToString(FirstStates);
            var data2 = StatesToString(SecondStates);
            var data3 = StatesToString(ThirdStates);
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ProgressDialogConfig progressDialogConfig = new ProgressDialogConfig()
            {
                Title = "Sending...",
                CancelText = "Cancel",
                OnCancel = cancellationTokenSource.Cancel
            };
            var diag = _userDialogs.Progress(progressDialogConfig);
            await Task.Run(async() =>
            {
                var res = deviceModels.Where(x => x.IsEnabled == true);
                if (res == null)
                    return;
                foreach (var device in res)
                {
                    try
                    {
                        var data = default(string);
                        switch (device.Panel)
                        {
                            case "1":
                                data = data1;
                                break;
                            case "2":
                                data = data2;
                                break;
                            case "3":
                                data = data3;
                                break;
                        }

                        HttpClient httpClient = new HttpClient();
                        //httpClient.Timeout = Constants.HttpRequestTimeout;
                        string path = device.Site + Constants.ControlPath;
                        var uri = new Uri(path);
                        var values = new Dictionary<string, string>{
                            { "ledsData", data }
                        };
                        var content = new FormUrlEncodedContent(values);
                        var response = await httpClient.PostAsync(path, content, cancellationTokenSource.Token);
                        Debug.WriteLine(response);
                    }
                    catch(OperationCanceledException ex)
                    {
                        Debug.WriteLine("OperationCanceledException");
                        return;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            });
            diag.Hide();
            Debug.WriteLine("SendDataToLamposhniiServerAsync");
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            LoadSites();
            Debug.WriteLine("Navigated to");
        }

        private ICommand _SettingsCommand;
        public ICommand SettingsCommand => _SettingsCommand ?? (_SettingsCommand = new Command(OnSettingsCommand));

        private async void OnSettingsCommand(object obj)
        {
            await _navigationService.NavigateAsync("SettingsView");
        }
    }
}
