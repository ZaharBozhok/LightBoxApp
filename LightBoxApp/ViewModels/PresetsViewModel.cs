using System;
using System.Collections.Generic;
using System.Windows.Input;
using Acr.UserDialogs;
using LightBoxApp.Models;
using LightBoxApp.Services.AppSettingsManager;
using Prism.Navigation;
using Xamarin.Forms;

namespace LightBoxApp.ViewModels
{
    public class PresetsViewModel : BaseViewModel
    {
        private readonly IAppSettingsManager _appSettingsManager;
        public readonly IUserDialogs _userDialogs;
        public PresetsViewModel(INavigationService navigationService, IAppSettingsManager appSettingsManager, IUserDialogs userDialogs) : base(navigationService)
        {
            _appSettingsManager = appSettingsManager;
            _userDialogs = userDialogs;
        }


        private List<PresetModel> _Presets;
        public List<PresetModel> Presets
        {
            get { return _Presets; }
            set { SetProperty(ref _Presets, value); }
        }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            Presets = await _appSettingsManager.GetPresetsAsync();
        }

        private ICommand _ItemTappedCommand;
        public ICommand ItemTappedCommand => _ItemTappedCommand ?? (_ItemTappedCommand = new Command(OnItemTappedCommand));

        private async void OnItemTappedCommand(object obj)
        {
            NavigationParameters navigationParameters = new NavigationParameters
            {
                { "Preset", ((PresetModel)obj) }
            };
            await _navigationService.NavigateAsync("PresetDetailsView", navigationParameters);
        }
    }
}
