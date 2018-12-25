namespace MovieDatabase.Data.Models
{
    public class MovieScreenwriter
    {
        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public Screenwriter Screenwriter { get; set; }
        
        public string ScreenwriterId { get; set; }
    }
}