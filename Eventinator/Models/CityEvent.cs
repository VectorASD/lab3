using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventinator.Models {
    public class CityEvent {
        public CityEvent(string title, string desc, string image, string date, string[] cats, string price) {
            Header = title;
            Description = desc;
            Date = date;
            this.cats = cats;
            Price = price;

            byte[] bytes = Convert.FromBase64String(image);
            Stream stream = new MemoryStream(bytes);
            Image = new Avalonia.Media.Imaging.Bitmap(stream);
        }
        public string Header { get; }
        public string Description { get; }
        public Avalonia.Media.Imaging.Bitmap Image { get; }
        public string Date { get; }
        private string[] cats;
        public string Price { get; }

    }
}
