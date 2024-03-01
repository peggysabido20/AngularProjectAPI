namespace AngularProjectAPI.Models
{
    public class EventsGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string Location { get; set; }
        public int Duration { get; set; }
        public Boolean IsFavorite { get; set; }
    }
}
