using TourApp.Base;
using TourApp.DB;
using TourApp.ViewModel;
using TourApp.Scraper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;


namespace TourApp.Model
{
    public class AutoSearchModel
    {

        static List<BaseScraper> _scrapers = new List<BaseScraper>() { new AnexScraper() };
        public static void StartSearchApartment()
        {
            Console.WriteLine("StartSearchApartment");
            Timer searchTimer = new Timer();

            searchTimer.Interval = 10 * 60 * 1000; //10 min

            searchTimer.Elapsed += Tick;
            searchTimer.Enabled = true;
            searchTimer.AutoReset = true;

            searchTimer.Start();

            Tick(null, null);
        }
        private static async void Tick(object o, EventArgs e)
        {
            Console.WriteLine("Start search");
            var searchArgument = DataBase.GetCollectionList<SearchTourItem>(DBTable.AutoSearchTour);

            if (searchArgument.Count == 0) return;

            var tasks = _scrapers.ToList().Select(async s => await s.GetAutoSearchTours(searchArgument));

            var tasks_result = await Task.WhenAll(tasks);

            var aparts = new List<FoundTour>();
            tasks_result.ToList().ForEach((t) => aparts.AddRange(t));

            var col = DataBase.GetCollectionList<FoundTour>(DBTable.FoundTour);

            aparts.ForEach(async res =>
            {
                if (DataBase.Query<FoundTour>(DBTable.FoundTour).Where(tour =>
                  tour.Tour.CityTo == res.Tour.CityTo &&
                  tour.Tour.HotelId == res.Tour.HotelId &&
                  tour.Tour.Cost == res.Tour.Cost &&
                  tour.Tour.MealDescription == res.Tour.MealDescription &&
                  tour.Tour.TownName == res.Tour.TownName).Limit(1).ToList().Count == 0)
                {


                    DataBase.Insert(res, DBTable.FoundTour);
                    if (AutoSearchViewModel.Instance.Items != null)
                    {
                        var asItem = AutoSearchViewModel.Instance.Items.FirstOrDefault(a => a.Id == res.SearchId);
                        if (asItem != null)
                        {
                            asItem.FoundTour++;
                            asItem.Num = asItem.Num;
                            DataBase.Update(asItem, DBTable.AutoSearchTour);
                        }
                    }
                }
            });
        }

        public void AddNewAutoSearchItem(SearchTourItem item)
        {
            item.Num = (AutoSearchViewModel.Instance.Items.Count + 1).ToString();
            DataBase.Insert(item, DBTable.AutoSearchTour);
            AutoSearchViewModel.Instance.Items.Add(item);
        }
        public async void GetAutoSearchItem()
        {
            var items = await Task.Run(() => { return DataBase.GetCollectionList<SearchTourItem>(DBTable.AutoSearchTour); });

            var foundAparts = DataBase.GetCollectionList<FoundTour>(DBTable.FoundTour);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Num = (i + 1).ToString();
                items[i].FoundTour = foundAparts.Where(a => a.SearchId == items[i].Id).ToList().Count;
            }
            AutoSearchViewModel.Instance.Items = new System.Collections.ObjectModel.ObservableCollection<SearchTourItem>(items);

            if (items.Count == 0)
            {
                AutoSearchViewModel.Instance.ShowStatus = Visibility.Visible;
            }

            
        }
        public void DeleteAutoSearchItem(SearchTourItem item)
        {
            DataBase.Delete<FoundTour>(tour => item.Id == tour.SearchId, DBTable.FoundTour);

            DataBase.Delete(item.Id, DBTable.AutoSearchTour);
            AutoSearchViewModel.Instance.Items.Remove(item);

            for (int i = 0; i < AutoSearchViewModel.Instance.Items.Count; i++)
            {
                AutoSearchViewModel.Instance.Items[i].Num = (i + 1).ToString();
            }
        }
        public void UpdateAutoSearchItem(SearchTourItem item)
        {
            item.Num = item.Num;
            AutoSearchViewModel.Instance.Items[int.Parse(item.Num) - 1] = item;
            DataBase.Update(item, DBTable.AutoSearchTour);
            DataBase.Delete<FoundTour>(apart => item.Id == apart.SearchId, DBTable.FoundTour);
        }
        public void GetFoundTours(SearchTourItem item)
        {
            ResultViewModel.Instance.Items = DataBase.Query<FoundTour>(DBTable.FoundTour)
                .Where(a => a.SearchId == item.Id)
                .Select(a => a.Tour).ToList();

            ResultViewModel.Instance.ShowAnimation = Visibility.Hidden;
            ResultViewModel.Instance.ShowResult = Visibility.Visible;

            if (ResultViewModel.Instance.Items.Count == 0)
            {
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;
                ResultViewModel.Instance.ShowStatus = Visibility.Visible;
            }
            ResultViewModel.Instance.Items.ForEach(async t =>
             {
                 if (t.ImageUrl == "pack://siteoforigin:,,,/Resources/noImage.png")
                 {
                     t.ImageUrlInfo = await AnexScraper.ImgUrl(t.HotelId);
                     DataBase.Update(t, DBTable.FavoriteTour);
                     await Task.Delay(250);
                 }
             });
        }
    }
}
