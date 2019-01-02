namespace MovieDatabase.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Services.Implementation;
    using MovieDatabase.Services.Utils;
    using MovieDatabase.Web.Controllers.Base;
    using MovieDatabase.Web.ViewModels.Posts;

    public class PostsController : BaseController
    {
        private ICrudService<Post> postService;
        private ICrudService<MovieCategory> movieCategoryService;
        private ICrudService<Director> directorService;
        private ICrudService<Screenwriter> screenwriterService;
        private ICrudService<Composer> composerService;
        private ICrudService<Award> awardService;
        private ImdbService imdbService;

        public PostsController(
            ICrudService<Post> postService,
            ICrudService<MovieCategory> movieCategoryService,
            ICrudService<Director> directorService,
            ICrudService<Screenwriter> screenwriterService,
            ICrudService<Composer> composerService,
            ICrudService<Award> awardService,
            ImdbService imdbService)
        {
            this.postService = postService;
            this.movieCategoryService = movieCategoryService;
            this.directorService = directorService;
            this.screenwriterService = screenwriterService;
            this.composerService = composerService;
            this.awardService = awardService;

            this.imdbService = imdbService;
        }

        [Route("posts")]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [Route("posts/create")]
        public IActionResult Create()
        {
            this.ViewBag.MovieCategories = this.movieCategoryService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id })
                .ToList();

            this.ViewBag.Directors = this.directorService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id })
                .ToList();

            this.ViewBag.Screenwriters = this.screenwriterService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id })
                .ToList();

            this.ViewBag.Composers = this.composerService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id })
                .ToList();

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("posts/create")]
        public async Task<IActionResult> Create(CreatePostVM postModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");
                return this.View(postModel);
            }

            var post = Mapper.Map<Post>(postModel);

            await this.postService.Create(post);

            this.AddAlert(true, "Successfully added post");

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("posts/get-imdb-rating")]
        public IActionResult GetMovieImdbRating(string title)
        {
            var imdbMovie = this.imdbService.GetMovieByTitle(title);

            if (imdbMovie.Response)
            {
                return this.Json(new { imdbMovie.ImdbRating });
            }

            return this.BadRequest(imdbMovie.Error);
        }
    }
}
