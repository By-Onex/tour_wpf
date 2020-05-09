using System.Collections.Generic;
using TourApp.Base;
using TourApp.DB;

namespace TourApp.ViewModel
{
    public class ResultViewModel : BaseViewModel
    {
        public static ResultViewModel Instance { get; } = new ResultViewModel();

        private List<TourItem> _items;
        public List<TourItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyPropertyChanged();
            }
        }

        private ResultViewModel()
        {
            Items = new List<TourItem>();
        }
    }
}
