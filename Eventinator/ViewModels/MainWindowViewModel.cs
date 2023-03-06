using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.X11;
using Eventinator.Models;

namespace Eventinator.ViewModels {
    public class MainWindowViewModel: ViewModelBase {
        public static string Error { get => Storager.Me.error; }

        readonly TabItem[] tabItems = new TabItem[] {
            new TabItem("Для детей",    "kids",         "img_a"),
            new TabItem("Спорт",        "wellness",     "img_b"),
            new TabItem("Культура",     "culture",      "img_c"),
            new TabItem("Экскурсии",    "excursion",    "img_d"),
            new TabItem("Образ жизни",  "lifestyle",    "img_e"),
            new TabItem("Вечеринки",    "parties",      "img_f"),
            new TabItem("Образование",  "study",        "img_g"),
            new TabItem("Онлайн",       "online",       "img_h"),
            new TabItem("Шоу",          "shows",        "img_i"),
        };
        public TabItem[] TabItems { get => tabItems; }
    }
}
