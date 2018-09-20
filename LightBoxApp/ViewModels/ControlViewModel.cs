using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            LoadSites();
            _PresetModel = new PresetModel(Constants.XAmount,Constants.YAmount);
        }

        private PresetModel _PresetModel;
        public PresetModel PresetModel
        {
            get { return _PresetModel; }
            set { SetProperty(ref _PresetModel, value); }
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

        private ICommand _SendCommand;
        public ICommand SendCommand => _SendCommand ?? (_SendCommand = new Command(OnSendCommand));

        private string StatesToString(List<StateModel> models)
        {
            StringBuilder ret = new StringBuilder(new String(Constants.NoData, Constants.DataTemplate.Count()));
            //ti - templte index
            //da - data index
            for (int ti = 0, da = 0; ti < Constants.DataTemplate.Count() && da < models.Count; ti++)
            {
                if(Constants.DataTemplate[ti] == Constants.Data)
                {
                    ret[ti] = models[da++].State == true ? Constants.Data:Constants.NoData;
                }
            }
            return ret.ToString();
        }

        private ICommand _SaveCommand;
        public ICommand SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(OnSaveCommand));

        private async void OnSaveCommand(object obj)
        {
            PresetModel.id = Guid.NewGuid();
            await _appSettingsManager.UpdatePresetByNameAsync(PresetModel);
        }

        private async void OnSendCommand(object obj)
        {
            var data1 = StatesToString(PresetModel.FirstPanel);
            var data2 = StatesToString(PresetModel.SecondPanel);
            var data3 = StatesToString(PresetModel.ThirdPanel);
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
            if(parameters.TryGetValue("Preset", out PresetModel presetModel))
            {
                this.PresetModel.FirstPanel = presetModel.FirstPanel;
                this.PresetModel.SecondPanel = presetModel.SecondPanel;
                this.PresetModel.ThirdPanel = presetModel.ThirdPanel;
                this.PresetModel.Name = presetModel.Name;
                OnSendCommand(null);
            }
            Debug.WriteLine("OnNavigatingTo to");
        }

        private ICommand _SettingsCommand;
        public ICommand SettingsCommand => _SettingsCommand ?? (_SettingsCommand = new Command(OnSettingsCommand));

        private async void OnSettingsCommand(object obj)
        {
            await _navigationService.NavigateAsync("SettingsView");
        }

        private ICommand _PresetsCommand;
        public ICommand PresetsCommand => _PresetsCommand ?? (_PresetsCommand = new Command(OnPresetsCommand));

        private async void OnPresetsCommand(object obj)
        {
            await _navigationService.NavigateAsync("PresetsView");
        }
    }
}
