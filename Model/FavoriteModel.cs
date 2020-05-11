using TourApp.DB;
using TourApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourApp.Scraper;

namespace TourApp.Model
{
    public class FavoriteModel
    {
        public async void GetTours()
        {
            var tours = await Task.Run(() => { return DataBase.GetCollectionList<TourItem>(DBTable.FavoriteTour); });
            tours.ForEach(a => a.IsFavorite = true);
            ResultViewModel.Instance.Items = tours;

            ResultViewModel.Instance.ShowAnimation = Visibility.Hidden;
            ResultViewModel.Instance.ShowResult = Visibility.Visible;

            if (tours.Count == 0)
            {
                ResultViewModel.Instance.ShowStatus = Visibility.Visible;
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;
            }
            foreach(var t in tours)
            {
                if(t.ImageUrl == "pack://siteoforigin:,,,/Resources/noImage.png")
                {
                    t.ImageUrlInfo = await AnexScraper.ImgUrl(t.HotelId);
                    DataBase.Update(t, DBTable.FavoriteTour);
                    await Task.Delay(250);
                }
            }
        }

        public void AddFavorite(TourItem tour)
        {
            if (tour.IsFavorite)
            {
                DataBase.Delete(tour.Id, DBTable.FavoriteTour);
                tour.IsFavorite = false;
            }
            else
            {
                var items = DataBase.Query<FoundTour>(DBTable.FoundTour).Where(res =>
                 tour.CityTo == res.Tour.CityTo &&
                 tour.HotelId == res.Tour.HotelId &&
                 tour.Cost == res.Tour.Cost &&
                 tour.MealDescription == res.Tour.MealDescription &&
                 tour.TownName == res.Tour.TownName).Limit(1).ToList();

                if (items.Count == 0)
                {
                    DataBase.Insert(tour, DBTable.FavoriteTour);
                    tour.IsFavorite = true;
                }
                else
                {
                    tour.Id = items[0].Id;
                    tour.IsFavorite = true;
                }
            }
        }
    }
}
