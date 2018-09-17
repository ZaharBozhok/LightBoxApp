using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LightBoxApp.Controls
{
    public class StateModel
    {
        public bool State { get; set; } = false;
    }
    public partial class ClickableGrid : ContentView
    {
        public ClickableGrid()
        {
            InitializeComponent();
            DrawGrid();
        }

        public static readonly BindableProperty OnAnyStateChangedProperty = BindableProperty.Create(
            nameof(OnAnyStateChanged),typeof(ICommand),typeof(ClickableGrid),default(ICommand));

        public ICommand OnAnyStateChanged
        {
            get { return (ICommand)GetValue(OnAnyStateChangedProperty); }
            set { SetValue(OnAnyStateChangedProperty, value); }
        }

        public static readonly BindableProperty StatesProperty = BindableProperty.Create(
            nameof(States), typeof(List<StateModel>), typeof(ClickableGrid), new List<StateModel>(), BindingMode.TwoWay);

        public List<StateModel> States
        {
            get { return (List<StateModel>)GetValue(StatesProperty); }
            set { SetValue(StatesProperty, value); }
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

        private ICommand _BoxViewTappedCommand;
        public ICommand BoxViewTappedCommand => _BoxViewTappedCommand ?? (_BoxViewTappedCommand = new Command(OnBoxViewTappedCommand));

        private void OnBoxViewTappedCommand(object obj)
        {
            var _obj = (SwitchableBoxView)obj;
            _obj.State = !_obj.State;
            OnPropertyChanged(nameof(States));
            if (OnAnyStateChanged != null)
                OnAnyStateChanged.Execute(null);
        }

        void DrawGrid()
        {
            this.States = new List<StateModel>();
            this.grid.Children.Clear();

            double size = Math.Max(App.ScreenWidth, App.ScreenHeight) * 0.06;
            for (int i = 0; i < XAmount; i++)
            {
                for (int j = 0; j < YAmount; j++)
                {
                    States.Add(new StateModel());
                    SwitchableBoxView boxView = new SwitchableBoxView();
                    boxView.BackgroundColor = Constants.OffColor;
                    boxView.OnColor = Constants.OnColor;
                    boxView.OffColor = Constants.OffColor;
                    boxView.SetBinding(SwitchableBoxView.StateProperty, new Binding("State",BindingMode.TwoWay, source: States[i]));
                    boxView.HeightRequest = size;
                    boxView.WidthRequest = boxView.HeightRequest;

                    ClickableContentView contentView = new ClickableContentView();
                    contentView.Content = boxView;
                    contentView.Command = BoxViewTappedCommand;
                    contentView.CommandParameter = boxView;

                    this.grid.Children.Add(contentView);
                    Grid.SetRow(contentView, i);
                    Grid.SetColumn(contentView, j);
                }
            }

        }
    }
}
