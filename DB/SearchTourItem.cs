namespace TourApp.DB
{
    public class SearchTourItem
    {
        public int Id { get; set; }
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public int MinCost { get; set; } = 0;
        public int MaxCost { get; set; } = 200000;

        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
