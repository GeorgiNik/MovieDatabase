namespace MovieDatabase.Data.Models
{
    public class MovieComposer    
    {
        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public Composer Composer { get; set; }
        
        public string ComposerId { get; set; }
    }
}