using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Windows.Input;
using Acr.UserDialogs;
using LightBoxApp.Models;
using LightBoxApp.Services.AppSettingsManager;
using Newtonsoft.Json;
using Prism.Navigation;
using Xamarin.Forms;

namespace LightBoxApp.ViewModels
{
    public class ConfigureAsAPViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IAppSettingsManager _appSettingsManager;
        private HttpClient _client;
        private string path = Constants.AddressOnAP + Constants.ConfigsPath;

        public ConfigureAsAPViewModel(INavigationService navigationService, IUserDialogs userDialogs, IAppSettingsManager appSettingsManager) : base(navigationService)
        {
            _userDialogs = userDialogs;
            _appSettingsManager = appSettingsManager;
            _client = new HttpClient();
        }

        private ICommand _WriteCommand;
        public ICommand WriteCommand => _WriteCommand ?? (_WriteCommand = new Command(OnWriteCommand));

        private async void OnWriteCommand(object obj)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ProgressDialogConfig progressDialogConfig = new ProgressDialogConfig()
            {
                Title = "Writing...",
                CancelText = "Cancel",
                OnCancel = cancellationTokenSource.Cancel
            };
            var diag = _userDialogs.Progress(progressDialogConfig);
            try
            {
                var uri = new Uri(path);
                var values = new Dictionary<string, string>
                {
                    { "accessPoint", this.APName },
                    { "password", this.APPassword },
                    { "reboot", "false"}
                };

                var content = new FormUrlEncodedContent(values);
                var response = await _client.PostAsync(path, content, cancellationTokenSource.Token);
                var responseString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine("OperationCanceledException");
            }
            catch (Exception ex)
            {
                diag.Hide();
                await _userDialogs.AlertAsync("Something went wrong...");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                diag.Hide();
            }
            Debug.WriteLine("OnWriteCommand");
        }

        private ICommand _ReadCommand;
        public ICommand ReadCommand => _ReadCommand ?? (_ReadCommand = new Command(OnReadCommand));

        private async void OnReadCommand(object obj)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ProgressDialogConfig progressDialogConfig = new ProgressDialogConfig()
            {
                Title = "Reading...",
                CancelText = "Cancel",
                OnCancel = cancellationTokenSource.Cancel
            };
            var diag = _userDialogs.Progress(progressDialogConfig);
            try
            {
                var uri = new Uri(path);
                HttpResponseMessage response = await _client.GetAsync(uri,cancellationTokenSource.Token);
                if (response.IsSuccessStatusCode)
                {
                    var a = await response.Content.ReadAsStringAsync();
                    LightBoxConfigModel lightBoxConfigModel = (LightBoxConfigModel)JsonConvert.DeserializeObject(a, typeof(LightBoxConfigModel));
                    this.APName = lightBoxConfigModel.accessPoint;
                    this.APPassword = lightBoxConfigModel.password;
                    this.MAC = lightBoxConfigModel.mac;
                    this.Site = lightBoxConfigModel.url;

                    //Saving to memory
                    await _appSettingsManager.AddDeviceAsync(new DeviceModel()
                    {
                        Mac = lightBoxConfigModel.mac,
                        Site = lightBoxConfigModel.url,
                        Panel = "1"
                    });
                }
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine("OperationCanceledException");
            }
            catch (Exception ex)
            {
                diag.Hide();
                await _userDialogs.AlertAsync("Something went wrong...");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                diag.Hide();
            }
            Debug.WriteLine("OnReadCommand");
        }

        private ICommand _ResetCommand;
        public ICommand ResetCommand => _ResetCommand ?? (_ResetCommand = new Command(OnResetCommand));

        private async void OnResetCommand(object obj)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ProgressDialogConfig progressDialogConfig = new ProgressDialogConfig()
            {
                Title = "Reseting...",
                CancelText = "Cancel",
                OnCancel = cancellationTokenSource.Cancel
            };
            var diag = _userDialogs.Progress(progressDialogConfig);
            try
            {
                var uri = new Uri(path);
                var values = new Dictionary<string, string>
                {
                    { "accessPoint", this.APName },
                    { "password", this.APPassword },
                    { "reboot", "true"}
                };

                var content = new FormUrlEncodedContent(values);
                var response = await _client.PostAsync(path, content, cancellationTokenSource.Token);
                var responseString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine("OperationCanceledException");
            }
            catch (Exception ex)
            {
                diag.Hide();
                await _userDialogs.AlertAsync("Something went wrong...");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                diag.Hide();
            }
            Debug.WriteLine("OnWriteCommand");
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if(parameters.TryGetValue("Path", out String newPath))
            {
                this.path = newPath;
            }
            OnReadCommand(null);
        }

        private string _APName;
        public string APName
        {
            get { return _APName; }
            set { SetProperty(ref _APName, value); }
        }

        private string _APPassword;
        public string APPassword
        {
            get { return _APPassword; }
            set { SetProperty(ref _APPassword, value); }
        }

        private string _MAC;
        public string MAC
        {
            get { return _MAC; }
            set { SetProperty(ref _MAC, value); }
        }

        private string _Site;
        public string Site
        {
            get { return _Site; }
            set { SetProperty(ref _Site, value); }
        }
    }
}
