using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using LightBoxApp.Controls;
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
        public ControlViewModel(IOrientationService orientationService, INavigationService navigationService, IAppSettingsManager appSettingsManager) : base(navigationService)
        {
            _orientationService = orientationService;
            _appSettingsManager = appSettingsManager;
            _orientationService.SetOrientation(Enums.EDeviceOrientations.Landscape);
            _XAmount = Constants.XAmount;
            _YAmount = Constants.YAmount;
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

        private void OnSendCommand(object obj)
        {
            //HttpClient httpClient = new HttpClient();
            //string path = Constants.AddressOnAP + Constants.ConfigsPath;
            //var uri = new Uri(path);
            //var values = new Dictionary<string, string>
            //    {
            //        { "accessPoint", this.APName },
            //        { "password", this.APPassword },
            //        { "reboot", "false"}
            //    };

            //var content = new FormUrlEncodedContent(values);
            //var response = await _client.PostAsync(path, content);
            //var responseString = await response.Content.ReadAsStringAsync();
            //Debug.WriteLine(responseString);
            Debug.WriteLine("SendDataToLamposhniiServerAsync");
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
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
