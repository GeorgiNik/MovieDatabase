namespace MovieDatabase.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieRatingVM : IMapFrom<Rating>
    {
        [Required(ErrorMessage = "Field is required")]
        public double Score { get; set; }
    }
}
