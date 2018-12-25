namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;

    public class Movie : BaseDeletableModel<string>
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Slug { get; set; }

        public MovieCategory MovieCategory { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Studio { get; set; }

        public string Description { get; set; }

        public string PosterImageLink { get; set; }

        public string TrailerLink { get; set; }

        public string Duration { get; set; }

        public string Country { get; set; }

        public double ImdbRating { get; set; }

        public string Language { get; set; }

        public string CountryOfProduction { get; set; }

        public MovieDirector Director { get; set; }

        public MovieScreenwriter Screenwriter { get; set; }

        public MovieComposer Composer { get; set; }

        public ICollection<Award> Awards { get; set; } = new HashSet<Award>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<Keyword> Keywords { get; set; } = new HashSet<Keyword>();

        public ICollection<MovieActor> Actors { get; set; }

        public ICollection<MovieRating> Ratings { get; set; }

        public virtual ICollection<UserWishlist> UserWishlists { get; set; } = new HashSet<UserWishlist>();

        public virtual ICollection<UserWatchedMovie> UserWatchedMovies { get; set; } = new HashSet<UserWatchedMovie>();

        public virtual ICollection<UserOwnedMovie> UserOwnedMovies { get; set; } = new HashSet<UserOwnedMovie>();
    }
}