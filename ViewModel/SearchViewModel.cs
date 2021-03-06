﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourApp.Base;
using TourApp.Model;
using TourApp.View;

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
        public string MinCost
        {
            get => MainViewModel.Instance.SearchTourParams.MinCost.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    MainViewModel.Instance.SearchTourParams.MinCost = int.Parse(value);
                    NotifyPropertyChanged();
                }
            }
        }
        public string MaxCost
        {
            get => MainViewModel.Instance.SearchTourParams.MaxCost.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    MainViewModel.Instance.SearchTourParams.MaxCost = int.Parse(value);
                    NotifyPropertyChanged();
                }
            }
        }
        public string NightStart
        {
            get => MainViewModel.Instance.SearchTourParams.NightStart.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    MainViewModel.Instance.SearchTourParams.NightStart = int.Parse(value);
                    NotifyPropertyChanged();
                }
            }
        }
        public string NightEnd
        {
            get => MainViewModel.Instance.SearchTourParams.NightEnd.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    MainViewModel.Instance.SearchTourParams.NightEnd = int.Parse(value);
                    NotifyPropertyChanged();
                }
            }
        }
        public string AdultCount
        {
            get => MainViewModel.Instance.SearchTourParams.AdultCount.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    MainViewModel.Instance.SearchTourParams.AdultCount = int.Parse(value);
                    NotifyPropertyChanged();
                }
            }
        }
        public string ChildCount
        {
            get => MainViewModel.Instance.SearchTourParams.ChildCount.ToString();
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    MainViewModel.Instance.SearchTourParams.ChildCount = int.Parse(value);
                    NotifyPropertyChanged();
                }
            }
        }
        public string DateTo
        {
            get => MainViewModel.Instance.SearchTourParams.DateTo;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (DateTime.TryParse(value, out DateTime res))
                    {
                        _selectedDateTo = res;
                        NotifyPropertyChanged("SelectedDateTo");
                    }
                    MainViewModel.Instance.SearchTourParams.DateTo = value;
                    NotifyPropertyChanged();
                    EnableSearchButton = CheckDate();
                }
            }
        }
        public string DateFrom
        {
            get => MainViewModel.Instance.SearchTourParams.DateFrom;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (DateTime.TryParse(value, out DateTime res))
                    {
                        _selectedDateFrom = res;
                        NotifyPropertyChanged("SelectedDateFrom");
                    }
                    MainViewModel.Instance.SearchTourParams.DateFrom = value;
                    NotifyPropertyChanged();
                    EnableSearchButton = CheckDate();
                }
            }
        }
        public string Description
        {
            get => MainViewModel.Instance.SearchTourParams.Description;
            set
            {
                MainViewModel.Instance.SearchTourParams.Description = value;
                NotifyPropertyChanged("Description");
            }
        }
        public int FromCityId
        {
            get => MainViewModel.Instance.SearchTourParams.FromCityId;
            set
            {
                MainViewModel.Instance.SearchTourParams.FromCityId = value;
                NotifyPropertyChanged();
            }
        }
        public int ToCityId
        {
            get => MainViewModel.Instance.SearchTourParams.ToCityId;
            set
            {
                MainViewModel.Instance.SearchTourParams.ToCityId = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _selectedDateFrom;
        public DateTime SelectedDateFrom
        {
            get => _selectedDateFrom;
            set
            {
                _selectedDateFrom = value;
                DateFrom = value.ToString("dd.MM.yyyy");
                NotifyPropertyChanged();
            }
        }
        private DateTime _selectedDateTo;
        public DateTime SelectedDateTo
        {
            get => _selectedDateTo;
            set
            {
                _selectedDateTo = value;
                DateTo = value.ToString("dd.MM.yyyy");
                NotifyPropertyChanged();
            }
        }

        /*private ResortItem _resortSelected;
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
        }*/

        private string _buttonText = "Добавить";
        public string ButtonText { get => _buttonText; set { _buttonText = value; NotifyPropertyChanged("ButtonText"); } }

        private FromToCityViewModel _fromToCityViewModel;
        public FromToCityViewModel FromToCityViewModel
        {
            get => _fromToCityViewModel;
            set
            {
                _fromToCityViewModel = value;
                NotifyPropertyChanged();
            }
        }

        private bool _leftCalendar;
        public bool LeftCalendar
        {
            get => _leftCalendar;
            set
            {
                _leftCalendar = value;
                NotifyPropertyChanged();
            }
        }

        private bool _rightCalendar;
        public bool RightCalendar
        {
            get => _rightCalendar;
            set
            {
                _rightCalendar = value;
                NotifyPropertyChanged();
            }
        }

        private bool _enableSearchButton = true;

        public bool EnableSearchButton
        {
            get => _enableSearchButton;
            set
            {
                _enableSearchButton = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand ToggleRightCal { get; set; }
        public ICommand ToggleLeftCal { get; set; }
        public ICommand SearchTours { get; set; }
        public ICommand OpenPage { get; set; }


        public SearchViewModel()
        {
            searchModel = new SearchModel();

            SelectedDateTo = DateTime.Now + new TimeSpan(30, 0, 0, 0);
            SelectedDateFrom = DateTime.Now;

            ToggleLeftCal = new BaseCommand((o) =>
            {
                LeftCalendar = !_leftCalendar;
            });

            ToggleRightCal = new BaseCommand((o) =>
            {
                RightCalendar = !_rightCalendar;
            });

            SearchTours = new BaseCommand((o) =>
            {
                MainView.GoBottom();
                ResultViewModel.Instance.ShowAnimation = Visibility.Visible;
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;
                ResultViewModel.Instance.ShowStatus = Visibility.Hidden;

                SearchModel.isLoading = true;
                searchModel.GetTours(MainViewModel.Instance.SearchTourParams);
            });

            OpenPage = new BaseCommand((nextPage) =>
            {
                MainViewModel.Instance.ChangePage(((UserControl)Activator.CreateInstance((Type)nextPage)).Content, "С чем работать?");
            });
        }

        public void Update()
        {
            NotifyPropertyChanged("MinCost");
            NotifyPropertyChanged("MaxCost");

            NotifyPropertyChanged("NightStart");
            NotifyPropertyChanged("NightEnd");

            NotifyPropertyChanged("AdultCount");
            NotifyPropertyChanged("ChildCount");

            NotifyPropertyChanged("DateTo");
            NotifyPropertyChanged("DateFrom");

            NotifyPropertyChanged("Description");

            NotifyPropertyChanged("FromCityId");
            NotifyPropertyChanged("ToCityId");

            
        }
        private bool CheckDate()
        {
            return DateTime.TryParse(DateTo, out DateTime _) && DateTime.TryParse(DateFrom, out DateTime _);
        }
    }
}

