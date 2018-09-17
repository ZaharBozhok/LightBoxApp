using System;
using Xamarin.Forms;

namespace LightBoxApp.Controls
{
    public class SwitchableBoxView : BoxView
    {
        public static readonly BindableProperty OnColorProperty = BindableProperty.Create(
            nameof(OnColor), typeof(Color), typeof(SwitchableBoxView), Color.White);

        public Color OnColor
        {
            get { return (Color)GetValue(OnColorProperty); }
            set { SetValue(OnColorProperty, value); }
        }

        public static readonly BindableProperty OffColorProperty = BindableProperty.Create(
            nameof(OffColor), typeof(Color), typeof(SwitchableBoxView), Color.Black);

        public Color OffColor
        {
            get { return (Color)GetValue(OffColorProperty); }
            set { SetValue(OffColorProperty, value); }
        }

        public static readonly BindableProperty StateProperty = BindableProperty.Create(
            nameof(State),typeof(bool), typeof(SwitchableBoxView),default(bool),BindingMode.TwoWay,propertyChanged: OnStateChanged);

        private static void OnStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null || bindable == null)
                return;
            var _this = (SwitchableBoxView)bindable;
            bool _newValue = (bool)newValue;
            if (_newValue == true)
                _this.BackgroundColor = _this.OnColor;
            else
                _this.BackgroundColor = _this.OffColor;
        }

        public bool State
        {
            get { return (bool)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
    }
}
