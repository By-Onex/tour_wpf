using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourApp.Base;
using TourApp.Model;

namespace TourApp.ViewModel
{
    public class CityItem : BaseViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public ObservableCollection<CityItem> ToCity { get; set; }
    }
    public class FromToCityViewModel : BaseViewModel
    {
        private FromToCityModel fromToCityModel;
        private string _fromCitySearch = "Откуда";
        public string FromCitySearch
        {
            get => _fromCitySearch;
            set
            {
                _fromCitySearch = value;

                if (_selectedFromCity != null && value == _selectedFromCity.Value)
                {
                    _fromCitySearch = _selectedFromCity.Value;
                    FromCity.Clear();
                    FromCity.Add(_selectedFromCity);
                }
                else if (string.IsNullOrEmpty(_fromCitySearch))
                {
                    FromCity = new ObservableCollection<CityItem>(_fromCityTemp);
                }
                else
                {
                    FromCity.Clear();

                    foreach (var el in _fromCityTemp)
                    {
                        if (el.Value.ToLower() == _fromCitySearch.ToLower())
                        {
                            _fromCitySearch = el.Value;
                            SelectedFromCity = el;
                            FromCity.Clear();
                            FromCity.Add(el);

                            break;
                        }
                        else if (el.Value.ToLower().Contains(_fromCitySearch.ToLower())) FromCity.Add(el);
                        else FromCity.Remove(el);
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private string _toCitySearch = "Куда";
        public string ToCitySearch
        {
            get => _toCitySearch;
            set
            {
                _toCitySearch = value;

                if (_selectedToCity != null && value == _selectedToCity.Value)
                {
                    _toCitySearch = _selectedToCity.Value;
                    ToCity.Clear();
                    ToCity.Add(_selectedToCity);
                }
                else if (string.IsNullOrEmpty(_toCitySearch))
                {
                    ToCity = new ObservableCollection<CityItem>(_toCityTemp);
                }
                else
                {
                    ToCity.Clear();

                    foreach (var el in _toCityTemp)
                    {
                        if (el.Value.ToLower() == _toCitySearch.ToLower())
                        {
                            _toCitySearch = el.Value;
                            SelectedToCity = el;
                            ToCity.Clear();
                            ToCity.Add(el);
                            break;
                        }
                        else if (el.Value.ToLower().Contains(_toCitySearch.ToLower())) ToCity.Add(el);
                        else ToCity.Remove(el);
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<CityItem> _fromCity;
        public ObservableCollection<CityItem> FromCity
        {
            get => _fromCity;
            set
            {
                _fromCity = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<CityItem> _toCity;
        public ObservableCollection<CityItem> ToCity
        {
            get => _toCity;
            set
            {
                _toCity = value;
                NotifyPropertyChanged();
            }
        }

        private CityItem _selectedFromCity;
        public CityItem SelectedFromCity
        {
            get => _selectedFromCity;
            set
            {
                if (value != null)
                {
                    _selectedFromCity = value;
                    FromCitySearch = value.Value;
                    NotifyPropertyChanged();
                    GetToCity(value);

                    MainViewModel.Instance.PageStateText = "Выберите страну для отдыха";
                }
                else
                {
                    MainViewModel.Instance.PageStateText = "Выберите город для вылета";
                }
            }
        }

        private CityItem _selectedToCity;
        public CityItem SelectedToCity
        {
            get => _selectedToCity;
            set
            {
                if (value != null)
                {
                    _selectedToCity = value;
                    ToCitySearch = value.Value;
                    NotifyPropertyChanged();
                    GoNext = true;
                }
            }
        }
        private bool _goNext = false;
        public bool GoNext
        {
            get => _goNext;
            set
            {
                _goNext = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<CityItem> _toCityTemp = new ObservableCollection<CityItem>();
        private ObservableCollection<CityItem> _fromCityTemp = new ObservableCollection<CityItem>();

        public ICommand OpenPage { get; set; }
        public FromToCityViewModel()
        {
            OpenPage = new BaseCommand((nextPage) => {
                MainViewModel.Instance.SearchTourParams.FromCityId = _selectedFromCity.Id;
                MainViewModel.Instance.SearchTourParams.ToCityId = _selectedToCity.Id;
                MainViewModel.Instance.ChangePage(((UserControl)Activator.CreateInstance((Type)nextPage)).Content, "Заполните необходимые критерии");
            });

            fromToCityModel = new FromToCityModel();
            FromCity = new ObservableCollection<CityItem>();
            ToCity = new ObservableCollection<CityItem>();
            GetFromCity();
        }

        private async void GetFromCity()
        {
            var res = await fromToCityModel.GetCityFrom();
            FromCity = res;
            _fromCityTemp = new ObservableCollection<CityItem>(res);   
        }

        private void GetToCity(CityItem city)
        {
            ToCity = city.ToCity;
            _toCityTemp = new ObservableCollection<CityItem>(city.ToCity);
        }

    }
}
