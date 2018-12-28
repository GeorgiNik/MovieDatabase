namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;

    public class MovieCategory : BaseDeletableModel<string>
    {
        public string Slug { get; set; }
    }
}