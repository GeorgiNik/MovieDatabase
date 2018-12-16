namespace MovieDatabase.Web.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class ActorVM : IMapFrom<Actor>
    {
        public string  Name { get; set; }
        
        [Range(0, 150, ErrorMessage = "Invalid Age")]
        public int Age { get; set; }

        public string Nationality { get; set; }

        public Gender Gender { get; set; }
    }
}