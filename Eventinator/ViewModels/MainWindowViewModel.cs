using Eventinator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Eventinator.ViewModels {
    public class MainWindowViewModel: ViewModelBase {

        public string LOL { get => "\f118\n10"; }
        private readonly ObservableCollection<CityEvent> eventsList = new() {
            new CityEvent("lol"),
            new CityEvent("lol"),
            new CityEvent("lol")
        };
        public ObservableCollection<CityEvent> EventsList { get => eventsList; }
    }
}
