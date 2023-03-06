using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Text;

namespace Eventinator.Models {
    public class TabItem {
        public string Header { get; }
        public string Category { get; }
        public ObservableCollection<CityEvent> EventsList {
            get {
                var res = new ObservableCollection<CityEvent>();
                foreach (var Event in Storager.Me.eventsList)
                    if (Event.CheckCat(Category)) res.Add(Event);
                return res;
            }
        }
        public Bitmap? CategoryImage { get; }
        public TabItem(string header, string cat, string image) {
            Header = header;
            Category = cat;

            var app = Application.Current;
            if (app == null) return; // Такого просто не бывает, но надо ;'-}
            var ress = app.Resources;
            var img = (Image?) ress[image];
            if (img == null) return;
            CategoryImage = (Bitmap?) img.Source;
            // Storager.Me.error = "Image ok: " + CategoryImage;

            /* Убедился в том, что внутри действительно есть картинки...
            
            StringBuilder sb = new();
            foreach (var pair in ress) {
                if (sb.Length > 0) sb.Append("\n");
                sb.Append(pair.Key + "|" + pair.Value);
            }
            Storager.Me.error = sb.ToString() + "|" + image + " " + img.Source;

             * Чекаем App.axaml файлик - походу единственный способ вдолбить авалонии существование ресурса, не описанного в XML на прямую...
             * Вариант, где мы подкачиваем картинку на прямую через файловую систему, вообще заслуживает отдельного котла с пираньями XD,
             * ибо если мы соберём самостоятельный exe для продакшена, будет очень больно, когда exe не заработает и я знаю почему ;'-}
             * Storager.xml тоже по хорошему НЕЛЬЗЯ КАТЕГОРИЧЕСКИ подгружать через файлы сборки на прямую, но мне уже надоело этот проект править...
             * Да и код лояльно выглядет - коротко и ясно... */
        }
    }
}
