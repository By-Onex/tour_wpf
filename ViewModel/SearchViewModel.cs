using System.Collections.ObjectModel;
using TourApp.Base;
using TourApp.Model;

namespace TourApp.ViewModel
{
    public class ResortItem : BaseViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
    public class SearchViewModel : BaseViewModel
    {
        public SearchModel searchModel;


        private ResortItem _resortSelected;
        public ResortItem ResortSelected
        {
            get => _resortSelected;
            set
            {
                if (value != null && value != _resortSelected)
                {
                    _resortSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<ResortItem> _resortItemsTemp;
        private ObservableCollection<ResortItem> _resortItems;
        public ObservableCollection<ResortItem> ResortItems
        {
            get => _resortItems;
            set
            {
                _resortItems = value;
                NotifyPropertyChanged();
            }
        }
        public SearchViewModel()
        {
            searchModel = new SearchModel();
            GetResorts();
        }

        private async void GetResorts()
        {
            var res = await searchModel.GetResorts(MainViewModel.Instance.SearchTourParams.FromCityId, MainViewModel.Instance.SearchTourParams.ToCityId);
            ResortItems = res;
            _resortItemsTemp = new ObservableCollection<ResortItem>(res);
            if (res.Count > 0) ResortSelected = res[0];
        }
    }
}

