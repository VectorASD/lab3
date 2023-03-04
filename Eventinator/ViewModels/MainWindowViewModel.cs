using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Eventinator.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Eventinator.ViewModels {
    public class MainWindowViewModel: ViewModelBase {
        public string LOL { get => "&#xf118;"; }
        private readonly ObservableCollection<CityEvent> eventsList = new() {
            //new CityEvent("lol"),
            //new CityEvent("lol"),
            //new CityEvent("lol")
        };
        public ObservableCollection<CityEvent> EventsList { get => eventsList; }

        public MainWindowViewModel() {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            if (assets == null) return;
            try {
                var stream = assets.Open(new Uri("resm:Eventinator.Assets.Storager.xml"));
            } catch (Exception ex) {
                Items.Add(new Item { Name = "LOL " + ex, Color = new SolidColorBrush(Colors.Red) });
            }
        }

        private ObservableCollection<Item> items = new() {
            new Item { Name = "RedName", Color = new SolidColorBrush(Colors.Red) },
            new Item { Name = "GreenName", Color = new SolidColorBrush(Colors.Green) },
            new Item { Name = "PurpleName", Color = new SolidColorBrush(Colors.Purple) },
            new Item { Name = "BlueName", Color = new SolidColorBrush(Colors.Blue) },
            new Item { Name = "OrangeName", Color = new SolidColorBrush(Colors.Orange) },
            new Item { Name = "RedName", Color = new SolidColorBrush(Colors.Red) },
            new Item { Name = "GreenName", Color = new SolidColorBrush(Colors.Green) },
            new Item { Name = "PurpleName", Color = new SolidColorBrush(Colors.Purple) },
            new Item { Name = "BlueName", Color = new SolidColorBrush(Colors.Blue) },
            new Item { Name = "OrangeName", Color = new SolidColorBrush(Colors.Orange) },
            new Item { Name = "RedName", Color = new SolidColorBrush(Colors.Red) },
            new Item { Name = "GreenName", Color = new SolidColorBrush(Colors.Green) },
            new Item { Name = "PurpleName", Color = new SolidColorBrush(Colors.Purple) },
            new Item { Name = "BlueName", Color = new SolidColorBrush(Colors.Blue) },
            new Item { Name = "OrangeName", Color = new SolidColorBrush(Colors.Orange) },
            new Item { Name = "RedName", Color = new SolidColorBrush(Colors.Red) },
            new Item { Name = "GreenName", Color = new SolidColorBrush(Colors.Green) },
            new Item { Name = "PurpleName", Color = new SolidColorBrush(Colors.Purple) },
            new Item { Name = "BlueName", Color = new SolidColorBrush(Colors.Blue) },
            new Item { Name = "OrangeName", Color = new SolidColorBrush(Colors.Orange) },
            new Item { Name = "RedName", Color = new SolidColorBrush(Colors.Red) },
            new Item { Name = "GreenName", Color = new SolidColorBrush(Colors.Green) },
            new Item { Name = "PurpleName", Color = new SolidColorBrush(Colors.Purple) },
            new Item { Name = "BlueName", Color = new SolidColorBrush(Colors.Blue) },
            new Item { Name = "OrangeName", Color = new SolidColorBrush(Colors.Orange) },
        };

        public ObservableCollection<Item> Items {
            get { return items; }
            set {
                this.RaiseAndSetIfChanged(ref items, value);
            }
        }
    }
}
