using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TourApp.Base;
using TourApp.DB;
using TourApp.Model;
using TourApp.View;

namespace TourApp.ViewModel
{
    public class ResultViewModel : BaseViewModel
    {
        public static ResultViewModel Instance { get; } = new ResultViewModel();
        public FavoriteModel favoriteModel = new FavoriteModel();
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
                Console.WriteLine(value);
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

                SearchModel.isLoading = false;
            });

            OpenWebPage = new BaseCommand(url =>
            {
                System.Diagnostics.Process.Start(url.ToString());
            });

            AddFavorite = new BaseCommand(tour =>
            {
                if (tour is TourItem)
                {
                    favoriteModel.AddFavorite(tour as TourItem);
                }
            });
        }
        private void Search()
        {
            if (!string.IsNullOrWhiteSpace(_searchText))
                _items.ForEach(tour =>
                {
                    if (tour.TourInfo.Contains(_searchText) || tour.MealDescription.Contains(_searchText) || tour.Date.Contains(_searchText))
                        tour.Visibility = Visibility.Visible;
                    else tour.Visibility = Visibility.Collapsed;
                });
            else _items.ForEach(tour => tour.Visibility = Visibility.Visible);
        }
        private void SortList()
        {
            if (_items != null && _sort != null)
            {
                _items.Sort((a1, a2) =>
                {
                    switch (_sort)
                    {
                        case "Цена ↑":
                            return a1.Cost == a2.Cost ? 0 : a1.Cost > a2.Cost ? 1 : -1;
                        case "Цена ↓":
                            return a1.Cost == a2.Cost ? 0 : a1.Cost > a2.Cost ? -1 : 1;
                        case "Город":
                            return a1.TownName == a2.TownName ? 0 : a1.TownName[0] > a2.TownName[0] ? 1 : -1;
                        default: return 0;
                    }
                });
                Items = new List<TourItem>(_items);
            }
        }
    }
}
