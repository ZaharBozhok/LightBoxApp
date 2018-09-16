using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace LightBoxApp.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible
    {
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
    }
}
