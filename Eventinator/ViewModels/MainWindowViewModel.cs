using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.X11;
using Eventinator.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace Eventinator.ViewModels {
    public class MainWindowViewModel: ViewModelBase {

        private string error = "";
        public string Error { get => error; set => this.RaiseAndSetIfChanged(ref error, value); }

        public MainWindowViewModel() {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            if (assets == null) return;
            try {
                var stream = assets.Open(new Uri("resm:Eventinator.Assets.Storager.xml"));
            } catch (Exception ex) {
                Error = "LOL " + ex;
            }
        }

        TabItem[] tabItems = new TabItem[] {
            new TabItem("\xf118 Для детей", "kids"),
            new TabItem("\xf119 Спорт", "wellness"),
            new TabItem("\xf11a Культура", "culture"),
            new TabItem("\xf11b Экскурсии", "excursion"),
            new TabItem("\xf11c Образ жизни", "lifestyle"),
            new TabItem("\xf11d Вечеринки", "parties"),
            new TabItem("\xf11e Образование", "study"),
            new TabItem("\xf11f Онлайн", "online"),
            new TabItem("\xf120 Шоу", "shows"),
        };
        public TabItem[] TabItems { get => tabItems; }
    }
}
