using System;
using System.Windows.Controls;
using System.Windows.Input;
using TourApp.Base;

namespace TourApp.ViewModel
{
    public class TypeSearchViewModel : BaseViewModel
    {
        public ICommand OpenPageSearch { get; set; }

        public TypeSearchViewModel()
        {
            OpenPageSearch = new BaseCommand((st) =>
            {
                //MainViewModel.Instance.SearchArgumentModel.SearchType = (string)st == "Buy" ? NewModel.SearchType.Buy : NewModel.SearchType.Arenda;
                MainViewModel.Instance.ChangePage(((UserControl)Activator.CreateInstance(typeof(View.SearchView))).Content, "Заполните необходимые критерии");
            });
        }
    }
}
