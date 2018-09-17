using System;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace LightBoxApp.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible
    {
        protected readonly INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        #region -- IViewActionsHandler implementation --

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        #endregion

        #region -- INavigationAware implementation --

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        #endregion

        #region -- IDestructible implementation --

        public virtual void Destroy()
        {

        }

        #endregion

        protected void AssertParametrExisting(NavigationParameters parameters, string parameter)
        {
            if (parameters.ContainsKey(parameter))
            {
                return;
            }
            else
            {
                throw new ArgumentException(parameter + " parsmeter doesn't setted");
            }
        }

        private ICommand _BackCommand;
        public ICommand BackCommand => _BackCommand ?? (_BackCommand = new Command(OnBackCommand));

        public virtual async void OnBackCommand(object obj)
        {
            await _navigationService.GoBackAsync();
        }
    }
}
