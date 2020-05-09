using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourApp.Base;
using TourApp.DB;
using TourApp.View;

namespace TourApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public static MainViewModel Instance { get; } = new MainViewModel();
        private object _pageContent;
        public object PageContent
        {
            set
            {
                _pageContent = value;
                NotifyPropertyChanged();
            }
            get => _pageContent;
        }
        private string _pageStateText;

        private int _heightGrid = 420;
        public int HeightGrid
        {
            set
            {
                _heightGrid = value;
                NotifyPropertyChanged("HeightGrid");
            }
            get => _heightGrid;
        }

        private int _widthGrid = 800;
        public int WidthGrid
        {
            set
            {
                _widthGrid = value;
                NotifyPropertyChanged("WidthGrid");
            }
            get => _widthGrid;
        }

        public string PageStateText
        {
            get => _pageStateText;
            set
            {
                _pageStateText = value;
                NotifyPropertyChanged();
            }
        }
        
        public SearchTourItem SearchTourParams = new SearchTourItem();
        private MainViewModel()
        {
            DataBase.Connect();
            PageContent = new StartMenuView().Content;
            PageStateText = "Добро пожаловать! С чем работать?";

            //AutoSearchModel.StartSearchApartment();
        }

        public void ChangePage(object contetPage)
        {
            PageContent = contetPage;
        }

        public void ChangePage(object contetPage, string newTitle)
        {
            PageContent = contetPage;
            PageStateText = newTitle;
        }
    }
}
