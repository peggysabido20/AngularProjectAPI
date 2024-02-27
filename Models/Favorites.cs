namespace AngularProjectAPI.Models
{
    public class Favorites
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserName { get; set; }
        public bool Favorite { get; set; }
    }
}
