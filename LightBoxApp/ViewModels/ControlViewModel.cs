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
        }

        private PresetModel _PresetModel = new PresetModel(Constants.XAmount, Constants.YAmount);
        public PresetModel PresetModel
        {
            get {return _PresetModel; }
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
            //for(int reti = 0, maski=0, i=0; maski < Constants.XAmount;  i++)
            //{
            //    for(int j=0; j<Constants.YAmount; j++, reti++)
            //    {
            //        if(Constants.DataTemplate[maski] == Constants.Data)
            //        {
            //            int modi = i * Constants.XAmount;
            //            if(i%2 == 0)
            //                modi += Constants.YAmount - j-1;
            //            else
            //                modi += j;

            //            ret[reti] = models[modi].State == true ? Constants.Data : Constants.NoData;
            //        }
            //    }
            //}

            ret[0] = models[4].State== true ? Constants.Data : Constants.NoData;
            ret[1] = models[3].State== true ? Constants.Data : Constants.NoData;
            ret[2] = models[2].State== true ? Constants.Data : Constants.NoData;
            ret[3] = models[1].State== true ? Constants.Data : Constants.NoData;
            ret[4] = models[0].State== true ? Constants.Data : Constants.NoData;

            ret[5] = models[5].State== true ? Constants.Data : Constants.NoData;
            ret[6] = models[6].State== true ? Constants.Data : Constants.NoData;
            ret[7] = models[7].State== true ? Constants.Data : Constants.NoData;
            ret[8] = models[8].State== true ? Constants.Data : Constants.NoData;
            ret[9] = models[9].State== true ? Constants.Data : Constants.NoData;

            ret[10] = models[14].State== true ? Constants.Data : Constants.NoData;
            ret[11] = models[13].State== true ? Constants.Data : Constants.NoData;
            ret[12] = models[12].State== true ? Constants.Data : Constants.NoData;
            ret[13] = models[11].State== true ? Constants.Data : Constants.NoData;
            ret[14] = models[10].State== true ? Constants.Data : Constants.NoData;
            //15 skipped
            ret[16] = models[15].State== true ? Constants.Data : Constants.NoData;
            ret[17] = models[16].State== true ? Constants.Data : Constants.NoData;
            ret[18] = models[17].State== true ? Constants.Data : Constants.NoData;
            ret[19] = models[18].State== true ? Constants.Data : Constants.NoData;
            ret[20] = models[19].State== true ? Constants.Data : Constants.NoData;

            ret[21] = models[24].State== true ? Constants.Data : Constants.NoData;
            ret[22] = models[23].State== true ? Constants.Data : Constants.NoData;
            ret[23] = models[22].State== true ? Constants.Data : Constants.NoData;
            ret[24] = models[21].State== true ? Constants.Data : Constants.NoData;
            ret[25] = models[20].State== true ? Constants.Data : Constants.NoData;

            //ret[25] = (models[0].State== true ? Constants.Data : Constants.NoData);
            //ret[24] = models[1].State == true ? Constants.Data : Constants.NoData;
            //ret[23] = models[2].State == true ? Constants.Data : Constants.NoData;
            //ret[22] = models[3].State == true ? Constants.Data : Constants.NoData;
            //ret[21] = models[4].State == true ? Constants.Data : Constants.NoData;

            //ret[20] = models[9].State == true ? Constants.Data : Constants.NoData;
            //ret[19] = models[8].State == true ? Constants.Data : Constants.NoData;
            //ret[18] = models[7].State == true ? Constants.Data : Constants.NoData;
            //ret[17] = models[6].State == true ? Constants.Data : Constants.NoData;
            //ret[16] = models[5].State == true ? Constants.Data : Constants.NoData;
            ////attention 15
            //ret[14] = models[10].State == true ? Constants.Data : Constants.NoData;
            //ret[13] = models[11].State == true ? Constants.Data : Constants.NoData;
            //ret[12] = models[12].State == true ? Constants.Data : Constants.NoData;
            //ret[11] = models[13].State == true ? Constants.Data : Constants.NoData;
            //ret[10] = models[14].State == true ? Constants.Data : Constants.NoData;

            //ret[9] = models[19].State == true ? Constants.Data : Constants.NoData;
            //ret[8] = models[18].State == true ? Constants.Data : Constants.NoData;
            //ret[7] = models[17].State == true ? Constants.Data : Constants.NoData;
            //ret[6] = models[16].State == true ? Constants.Data : Constants.NoData;
            //ret[5] = models[15].State == true ? Constants.Data : Constants.NoData;

            //ret[4] = models[20].State == true ? Constants.Data : Constants.NoData;
            //ret[3] = models[21].State == true ? Constants.Data : Constants.NoData;
            //ret[2] = models[22].State == true ? Constants.Data : Constants.NoData;
            //ret[1] = models[23].State == true ? Constants.Data : Constants.NoData;
            //ret[0] = models[24].State == true ? Constants.Data : Constants.NoData;
            ////for (int i = 0, ti = 0, da = 0; i < Constants.XAmount; i++)
            //{
            //    for (int j = 0; j < Constants.YAmount; j++, ti++, da++)
            //    {
            //        if (Constants.DataTemplate[ti] == Constants.Data)
            //        {
            //            var index = 0;
            //            if (i % 2 == 0)
            //                index = Constants.XAmount * i + j;
            //            else
            //                index = Constants.XAmount * i + Constants.YAmount - j - 1;
            //            var val = models[index].State == true ? Constants.Data : Constants.NoData;
            //            ret[da] = val;

            //        }
            //    }
            //}
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
            await Task.Run(async () =>
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
                    catch (OperationCanceledException ex)
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
            if (parameters.TryGetValue("Preset", out PresetModel presetModel))
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
