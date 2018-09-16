using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LightBoxApp.Controls
{
    public partial class ClickableGrid : ContentView
    {
        public ClickableGrid()
        {
            InitializeComponent();
            DrawGrid();
        }

        public static readonly BindableProperty XAmountProperty = BindableProperty.Create(
            nameof(XAmount), typeof(int), typeof(ClickableGrid), 1, propertyChanged: OnAmountChanged);
        public int XAmount
        {
            get { return (int)GetValue(XAmountProperty); }
            set { SetValue(XAmountProperty, value); }
        }

        public static readonly BindableProperty YAmountProperty = BindableProperty.Create(
            nameof(YAmount), typeof(int), typeof(ClickableGrid), 1, propertyChanged: OnAmountChanged);

        public int YAmount
        {
            get { return (int)GetValue(YAmountProperty); }
            set { SetValue(YAmountProperty, value); }
        }

        private static void OnAmountChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var _this = (ClickableGrid)bindable;
            _this.DrawGrid();
        }

        void DrawGrid()
        {

            this.grid.Children.Clear();
            for (int i = 0; i < XAmount; i++)
            {
                for (int j = 0; j < YAmount; j++)
                {
                    BoxView boxView = new BoxView();
                    boxView.BackgroundColor = Color.Fuchsia;
                    boxView.HeightRequest = 30;
                    boxView.WidthRequest = boxView.HeightRequest;
                    this.grid.Children.Add(boxView);
                    Grid.SetRow(boxView, i);
                    Grid.SetColumn(boxView, j);
                }
            }

        }
    }
}
