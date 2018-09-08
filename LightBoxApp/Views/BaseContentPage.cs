using System;
using Prism.Mvvm;
using Xamarin.Forms;

namespace LightBoxApp.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            ViewModelLocator.SetAutowireViewModel(this, true);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            BackgroundColor = Color.White;
        }
    }
}
