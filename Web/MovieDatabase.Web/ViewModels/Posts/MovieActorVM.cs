﻿namespace MovieDatabase.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieActorVM : IMapFrom<MovieActor>
    {
        [Required(ErrorMessage = "Field is required")]
        public string ActorId { get; set; }

        public string ActorName { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string CharacterName { get; set; }
    }
}
