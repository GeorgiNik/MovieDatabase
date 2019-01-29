namespace MovieDatabase.Web.Controllers
{
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Services.Identity;
    using MovieDatabase.Web.Controllers.Base;
    using MovieDatabase.Web.ViewModels.Events;

    public class EventsController : EntityListController
    {
        private ICrudService<Event> eventService;
        private IHostingEnvironment hostingEnvironment;

        private ICrudService<Movie> movieService;
        private ApplicationUserManager<ApplicationUser> userManager;

        public EventsController(
            ICrudService<Movie> movieService,
            IHostingEnvironment hostingEnvironment,
            ApplicationUserManager<ApplicationUser> userManager,
            ICrudService<Event> eventService)
        {
            this.movieService = movieService;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.eventService = eventService;
        }

        [Route("events")]
        public IActionResult Index()
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            string username = this.userManager.GetUserId(this.User);

            var userEvents = this.eventService.GetAll()
                .Include(x => x.Owner)
                .Include(x => x.Participants)
                .Include(x => x.Movie)
                .Where(x => x.OwnerId == username)
                .ProjectTo<EventVM>()
                .ToList();

            var eventsParticipating = this.eventService.GetAll()
                .Include(x => x.Owner)
                .Include(x => x.Participants)
                .Include(x => x.Movie)
                .Where(x => x.Participants.Any(y => y.UserId == username))
                .ProjectTo<EventVM>()
                .ToList();

            var indexVM = new EventIndexVM
            {
                UserEvents = userEvents,
                EventsParticipating = eventsParticipating
            };

            return this.View(indexVM);
        }

        [HttpGet]
        [Route("events/create")]
        public IActionResult Create()
        {
            this.LoadCreateEventDropdowns();

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/create")]
        public async Task<IActionResult> Create(CreateEventVM eventModel)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");

                this.LoadCreateEventDropdowns();
                return this.View(eventModel);
            }

            var @event = Mapper.Map<Event>(eventModel);
            @event.OwnerId = this.userManager.GetUserId(this.User);

            try
            {
                await this.eventService.Create(@event);
            }
            catch (DbUpdateException e)
                when (e.InnerException is SqlException sqlEx &&
                    (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                this.AddAlert(false, "Cannot insert duplicate movie");

                this.LoadCreateEventDropdowns();
                return this.View(eventModel);
            }

            this.AddAlert(true, "Successfully added event");

            return this.RedirectToAction("Index");
        }

        [Route("events/event/{eventId}")]
        public IActionResult Event(string eventId)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var eventModel = this.eventService.GetAll()
                .Include(x => x.Owner)
                .Include(x => x.Participants)
                .Include(x => x.Movie)
                .Where(p => p.Id == eventId)
                .ProjectTo<EventVM>()
                .FirstOrDefault();

            if (eventModel == null)
            {
                return this.NotFound("Event not found");
            }

            return this.View(eventModel);
        }

        private void LoadCreateEventDropdowns()
        {
            this.ViewBag.MovieCategories = this.movieService.GetAll()
                .OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id })
                .ToList();
        }
    }
}