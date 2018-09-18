using System;
using System.Windows.Input;
using LightBoxApp.Models;
using LightBoxApp.Services.AppSettingsManager;
using Prism.Navigation;
using Xamarin.Forms;

namespace LightBoxApp.ViewModels
{
    public class PresetDetailsViewModel : BaseViewModel
    {
        private readonly IAppSettingsManager _appSettingsManager;

        public PresetDetailsViewModel(INavigationService navigationService, IAppSettingsManager appSettingsManager) : base(navigationService)
        {
            _appSettingsManager = appSettingsManager;
            _PresetModel = new PresetModel(0, 0);
        }

        private PresetModel _PresetModel;
        public PresetModel PresetModel
        {
            get { return _PresetModel; }
            set { SetProperty(ref _PresetModel, value); }
        }

        private ICommand _RemoveCommand;
        public ICommand RemoveCommand => _RemoveCommand ?? (_RemoveCommand = new Command(OnRemoveCommand));

        private async void OnRemoveCommand(object obj)
        {
            await _appSettingsManager.RemovePresetAsync(PresetModel.id);
            await _navigationService.GoBackAsync();
        }

        public async override void OnBackCommand(object obj)
        {
            await _appSettingsManager.UpdatePresetAsync(PresetModel);
            base.OnBackCommand(obj);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if(parameters.TryGetValue("Preset",out PresetModel presetModel))
            {
                PresetModel = presetModel;
            }
        }

        private ICommand _LoadPresetCommand;
        public ICommand LoadPresetCommand => _LoadPresetCommand ?? (_LoadPresetCommand = new Command(OnLoadPresetCommand));

        private async void OnLoadPresetCommand(object obj)
        {
            NavigationParameters navigationParameters = new NavigationParameters
            {
                { "Preset", PresetModel }
            };
            await _navigationService.NavigateAsync("ControlView", navigationParameters);
        }
    }
}
