using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourApp.Base;
using TourApp.View;

namespace TourApp.ViewModel
{
    public class StartMenuViewModel : BaseViewModel
    {
        public ICommand OpenPage { get; set; }
        public ICommand MovePage { get; set; }
        public StartMenuViewModel()
        {
            OpenPage = new BaseCommand((nextPage) => 
                MainViewModel.Instance.ChangePage(((UserControl)Activator.CreateInstance((Type)nextPage)).Content, "Выберите город для вылета")); 

            MovePage = new BaseCommand((p) =>
            {
                if (p as string == "Favorite")
                {
                    MainView.GoBottom();
                    ResultViewModel.Instance.ShowAnimation = Visibility.Visible;
                    ResultViewModel.Instance.ShowResult = Visibility.Hidden;
                    ResultViewModel.Instance.favoriteModel.GetTours();
                }
                else
                {
                    MainView.GoRight();
                }
            });
        }
    }
}
