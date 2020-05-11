using System.Windows;
using TourApp.Base;

namespace TourApp.DB
{
    public class TourItem : BaseViewModel
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public string MealDescription { get; set; }
        public string Date { get; set; }
        public string Resort { get; set; }
        public string TownName { get; set; }
        public string CityTo { get; set; }
        public string PageUrl { get; set; }
        public string ImageUrl { get; set; } = "pack://siteoforigin:,,,/Resources/noImage.png";
        public int HotelId { get; set; }

        [LiteDB.BsonIgnore]
        private bool _isFavorite;
        [LiteDB.BsonIgnore]
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                _isFavorite = value;
                ButtonText = _isFavorite ? "Удалить" : "В избранное";
                NotifyPropertyChanged("IsFavorite");
                NotifyPropertyChanged("ButtonText");
            }
        }
        [LiteDB.BsonIgnore]
        public string ButtonText { get; set; } = "В избранное";
        [LiteDB.BsonIgnore]
        private Visibility _visibility = Visibility.Visible;
        [LiteDB.BsonIgnore]
        public Visibility Visibility { get => _visibility; set { if (value != _visibility) { _visibility = value; NotifyPropertyChanged("Visibility"); } } }
        [LiteDB.BsonIgnore]
        public string PriceInfo { get => string.Format("{0} руб.", Cost); }

        [LiteDB.BsonIgnore]
        public string TourInfo { get => string.Format("{0}, {1}", CityTo, TownName); }

        [LiteDB.BsonIgnore]
        public string ImageUrlInfo
        {
            get => ImageUrl;
            set
            {
                ImageUrl = value;
                NotifyPropertyChanged();
            }
        }
    }
}
