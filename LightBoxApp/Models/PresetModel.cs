using System;
using System.Collections.Generic;
using System.Linq;
using LightBoxApp.Controls;
using Prism.Mvvm;

namespace LightBoxApp.Models
{
    public class PresetModel : BindableBase
    {
        public PresetModel(int cols, int rows)
        {
            _FirstPanel = new List<StateModel>(Enumerable.Repeat<StateModel>(new StateModel(), cols*rows));
            _SecondPanel = new List<StateModel>(Enumerable.Repeat<StateModel>(new StateModel(), cols * rows));
            _ThirdPanel = new List<StateModel>(Enumerable.Repeat<StateModel>(new StateModel(), cols * rows));
        }

        private List<StateModel> _FirstPanel;
        public List<StateModel> FirstPanel
        {
            get { return _FirstPanel; }
            set { SetProperty(ref _FirstPanel, value); }
        }
        private List<StateModel> _SecondPanel;
        public List<StateModel> SecondPanel
        {
            get { return _SecondPanel; }
            set { SetProperty(ref _SecondPanel, value); }
        }

        private List<StateModel> _ThirdPanel;
        public List<StateModel> ThirdPanel
        {
            get { return _ThirdPanel; }
            set { SetProperty(ref _ThirdPanel, value); }
        }
        public Guid id { get; set; } = Guid.NewGuid();

        private string _Name = "Preset";
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
    }
}
