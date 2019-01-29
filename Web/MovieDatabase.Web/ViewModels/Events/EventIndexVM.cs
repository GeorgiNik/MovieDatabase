namespace MovieDatabase.Web.ViewModels.Events
{
    using System.Collections.Generic;

    public class EventIndexVM
    {
        public ICollection<EventVM> UserEvents { get; set; }

        public ICollection<EventVM> EventsParticipating { get; set; }
    }
}