namespace TourApp.DB
{
    public class FoundTour
    {
        public int Id { get; set; }
        public int SearchId { get; set; }
        public TourItem Tour { get; set; }
    }
}
