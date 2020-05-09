using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TourApp.Base;
using TourApp.DB;
using TourApp.View;

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
        private Visibility _showResult;
        public Visibility ShowResult
        {
            get => _showResult;
            set
            {
                _showResult = value;
                NotifyPropertyChanged();
            }
        }
        private Visibility _showAnimation;
        public Visibility ShowAnimation
        {
            get => _showAnimation;
            set
            {
                _showAnimation = value;
                NotifyPropertyChanged();
            }
        }
        private Visibility _showStatus;
        public Visibility ShowStatus
        {
            get => _showStatus;
            set
            {
                _showStatus = value;
                NotifyPropertyChanged();
            }
        }
        private string _sort;
        public string Sort
        {
            get => _sort;
            set
            {
                if (_sort != value)
                {
                    _sort = value;
                    NotifyPropertyChanged("Sort");
                    SortList();
                }
            }
        }
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value.Trim();
                NotifyPropertyChanged("SearchText");
                Search();
            }
        }


        public ICommand ReturnToTop { get; set; }
        public ICommand OpenWebPage { get; set; }
        public ICommand AddFavorite { get; set; }

        private ResultViewModel()
        {
            Items = new List<TourItem>();

            ShowAnimation = Visibility.Hidden;
            ShowResult = Visibility.Hidden;
            ShowStatus = Visibility.Hidden;

            ReturnToTop = new BaseCommand(o =>
            {
                MainView.GoTop();
                ShowResult = Visibility.Hidden;
                ShowAnimation = Visibility.Hidden;
                ShowStatus = Visibility.Hidden;
            });

            OpenWebPage = new BaseCommand(url =>
            {
                System.Diagnostics.Process.Start(url.ToString());
            });

            AddFavorite = new BaseCommand(apart =>
            {
                /*if (apart is ApartmentItem)
                {
                    FavoriteModel.AddFavorite(apart as ApartmentItem);
                }*/
            });
        }

        private void Search()
        {
            //_tempItems = new List<ApartmentItem>()
            if (!string.IsNullOrWhiteSpace(_searchText))
                _items.ForEach(tour =>
                {
                    /*if (apart.Address.DistrictInfo.Contains(_searchText) || apart.Address.Info.Contains(_searchText) || apart.Info.Contains(_searchText))
                        apart.Visibility = Visibility.Visible;
                    else apart.Visibility = Visibility.Collapsed;*/
                });
            else _items.ForEach(tour => tour.Visibility = Visibility.Visible);
        }

        private void SortList()
        {
            /*if (_items != null && _sort != null)
            {
                _items.Sort((a1, a2) =>
                {
                    switch (_sort)
                    {
                        case "Цена ↑":
                            return a1.Price == a2.Price ? 0 : a1.Price > a2.Price ? 1 : -1;
                        case "Цена ↓":
                            return a1.Price == a2.Price ? 0 : a1.Price > a2.Price ? -1 : 1;
                        case "Комнаты ↑":
                            return a1.RoomCount == a2.RoomCount ? 0 : a1.RoomCount > a2.RoomCount ? 1 : -1;
                        case "Комнаты ↓":
                            return a1.RoomCount == a2.RoomCount ? 0 : a1.RoomCount > a2.RoomCount ? -1 : 1;
                        default: return 0;
                    }
                });
                Items = new List<ApartmentItem>(_items);
            }*/
        }
    }
}
