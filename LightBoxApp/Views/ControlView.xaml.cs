using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LightBoxApp.Views
{
    public partial class ControlView : BaseContentPage
    {
        public ControlView()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
