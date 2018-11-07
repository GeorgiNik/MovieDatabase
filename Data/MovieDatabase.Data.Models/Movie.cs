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

        public double Rating { get; set; }

        public MovieCategory MovieCategory { get; set; }

        public string Director { get; set; }

        public string Music { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Studio { get; set; }

        public string Description { get; set; }

        public byte[] PosterImage { get; set; }

        public string TrailerLink { get; set; }

        public string Duration { get; set; }

        public string Country { get; set; }

        public double ImdbRating { get; set; }

        public string Language { get; set; }

        public string CountryOfProduction { get; set; }

        public ICollection<string> Cast { get; set; } = new HashSet<string>();

        public ICollection<string> Awards { get; set; } = new HashSet<string>();

        public ICollection<string> Comments { get; set; } = new HashSet<string>();
        
        public ICollection<string> Keywords { get; set; } = new HashSet<string>();

        public virtual ICollection<Actor> Actors { get; set; } = new HashSet<Actor>();
    }
}