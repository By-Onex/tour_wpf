using TourApp.Base;

namespace TourApp.DB
{
    public class SearchTourItem : BaseViewModel
    {
        public int Id { get; set; }
        public int FromCityId { get; set; } = -1;
        public int ToCityId { get; set; } = -1;
        public int MinCost { get; set; } = 0;
        public int MaxCost { get; set; } = 100000;
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int NightStart { get; set; } = 5;
        public int NightEnd { get; set; } = 12;
        public int AdultCount { get; set; } = 2;
        public int ChildCount { get; set; } = 0;
        public string Description { get; set; } = "";
        public int FoundTour { get; set; } = 0;

        [LiteDB.BsonIgnore]
        public string DescriptionInfo { get => string.IsNullOrWhiteSpace(Description) ? "Примечание отсутствует" : "Примечание: " + Description; }
        [LiteDB.BsonIgnore]
        public string FoundTourInfo { get => "Найдено: " + FoundTour; }

        [LiteDB.BsonIgnore]
        private string _num = "";
        [LiteDB.BsonIgnore]
        public string Num { get => _num; set { _num = value; NotifyPropertyChanged("Num"); NotifyPropertyChanged("DescriptionInfo"); NotifyPropertyChanged("FoundTourInfo"); } }

    }
}
