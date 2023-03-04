using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventinator.Models {
    public class TabItem {
        public string Header { get; }
        public string Category { get; }
        public ObservableCollection<CityEvent> EventsList {
            get {
                var res = new ObservableCollection<CityEvent>();
                foreach (var Event in Storager.eventsList)
                    if (Event.CheckCat(Category)) res.Add(Event);
                return res;
            }
        }
        public TabItem(string header, string cat) {
            Header = header;
            Category = cat;
        }
    }
}
