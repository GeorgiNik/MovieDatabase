namespace MovieDatabase.Web.Areas.Admin.Models.MovieCategories
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class CreateMovieCategoryVM : IMapFrom<MovieCategory>
    {
        [Required]
        public string Name { get; set; }
    }
}