namespace TourApp.DB
{
    public class SearchTourItem
    {
        public int Id { get; set; }
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public int ResortId { get; set; }
        public int MinCost { get; set; } = 0;
        public int MaxCost { get; set; } = 20000;
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int NightStart { get; set; } = 6;
        public int NightEnd { get; set; } = 12;
        public int AdultCount { get; set; } = 2;
        public int ChildCount { get; set; } = 0;
    }
}
