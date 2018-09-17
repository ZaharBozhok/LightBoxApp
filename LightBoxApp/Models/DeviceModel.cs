using System;
using Prism.Mvvm;

namespace LightBoxApp.Models
{
    public class DeviceModel : BindableBase
    {
        private string _Name;
        public string Name 
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
        public string Mac { get; set; }
        public string Site { get; set; }
        private string _Panel;
        public string Panel
        {
            get { return _Panel; }
            set 
            { 
                SetProperty(ref _Panel, value);

            }
        }
    }
}
