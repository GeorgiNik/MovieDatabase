namespace MovieDatabase.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class EventVM : IMapFrom<Event>, IHaveCustomMappings
    {
        public string Id { get; set; }
        
        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string OwnerId { get; set; }
        
        public string OwnerName { get; set; }

        public string MovieId { get; set; }
        
        public string MovieName { get; set; }

        public IEnumerable<string> PartisipantsList { get; set; }
    }
}