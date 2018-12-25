namespace MovieDatabase.Data.Models
{
    public class MovieDirector    
    {
        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public Director Director { get; set; }
        
        public string DirectorId { get; set; }
    }
}