namespace MovieDatabase.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Services.Identity;
    using MovieDatabase.Services.Utils;
    using MovieDatabase.Web.Controllers.Base;
    using MovieDatabase.Web.ViewModels.Posts;

    public class PostsController : BaseController
    {
        private IHostingEnvironment hostingEnvironment;
        private ApplicationUserManager<ApplicationUser> userManager;

        private ICrudService<Post> postService;
        private ICrudService<Category> movieCategoryService;
        private ICrudService<Director> directorService;
        private ICrudService<Screenwriter> screenwriterService;
        private ICrudService<Composer> composerService;
        private ICrudService<Movie> movieService;
        private ImdbService imdbService;

        public PostsController(
            ICrudService<Post> postService,
            ICrudService<Category> movieCategoryService,
            ICrudService<Director> directorService,
            ICrudService<Screenwriter> screenwriterService,
            ICrudService<Composer> composerService,
            ICrudService<Movie> movieService,
            ImdbService imdbService,
            IHostingEnvironment hostingEnvironment,
            ApplicationUserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.movieCategoryService = movieCategoryService;
            this.directorService = directorService;
            this.screenwriterService = screenwriterService;
            this.composerService = composerService;
            this.movieService = movieService;

            this.imdbService = imdbService;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        [Route("posts")]
        public async Task<IActionResult> Index()
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var posts = this.postService.GetAll().Include(s => s.Movie).ToList();

            return this.View();
        }

        [HttpGet]
        [Route("posts/create")]
        public IActionResult Create()
        {
            this.LoadCreateMovieDropdowns();

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("posts/create")]
        public async Task<IActionResult> Create(CreatePostVM postModel)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");

                this.LoadCreateMovieDropdowns();
                return this.View(postModel);
            }

            var post = Mapper.Map<Post>(postModel);

            post.UserId = this.userManager.GetUserId(this.User);

            post.Movie.Slug = SlugGenerator.GenerateSlug(post.Movie.Name);
            post.Movie.PosterImageLink = await FileManager.SaveFile(this.hostingEnvironment, postModel.Movie.PosterImage);

            var rating = Mapper.Map<Rating>(postModel.Movie.Rating);
            rating.RatedById = this.userManager.GetUserId(this.User);
            rating.RatedOn = DateTime.Now;

            post.Movie.Ratings.Add(new MovieRating { Rating = rating });

            foreach (var keyword in postModel.Movie.KeywordsString.Trim().Split(","))
            {
                post.Movie.Keywords.Add(new Keyword { Name = keyword.Trim() });
            }

            try
            {
                await this.postService.Create(post);
            }
            catch (DbUpdateException e)
            when (e.InnerException is SqlException sqlEx &&
                (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                this.AddAlert(false, "Cannot insert duplicate movie");

                this.LoadCreateMovieDropdowns();
                return this.View(postModel);
            }

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

        private void LoadCreateMovieDropdowns()
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
        }
    }
}
