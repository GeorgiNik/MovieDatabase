namespace MovieDatabase.Data.Models
{
    using System;
    using MovieDatabase.Data.Common.Models;

    public class CastMember: BaseDeletableModel<string>
    {
        public CastMember()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}