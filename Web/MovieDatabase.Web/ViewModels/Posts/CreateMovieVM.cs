namespace MovieDatabase.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class CreateMovieVM : IMapFrom<Movie>
    {
        [Required(ErrorMessage = "Field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string MovieCategoryId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Studio { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [MaxLength(250, ErrorMessage = "Description must be shorter than 250 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Allowed extensions are: jpg, jpeg and png")]
        public IFormFile PosterImage { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string TrailerLink { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public List<MovieActorVM> MovieActors { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string DirectorId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string ScreenwriterId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string ComposerId { get; set; }

        public string[] Awards { get; set; }

        public double ImdbRating { get; set; }

        public double Rating { get; set; }
    }
}
