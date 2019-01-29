namespace MovieDatabase.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;

    public class CreateEventVM
    {
        public string Id { get; set; }
        
        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string MovieId { get; set; }
    }
}