namespace MovieDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using MovieDatabase.Data.Common.Models;

    public class Post : BaseDeletableModel<string>
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public double Rating { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}