namespace MovieDatabase.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
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
    using MovieDatabase.Services.Utils;
    using MovieDatabase.Web.Controllers.Base;
    using MovieDatabase.Web.ViewModels;
    using MovieDatabase.Web.ViewModels.Posts;

    public class PostsController : EntityListController
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
        public IActionResult Index(PaginationVM pagination, PostFilterVM postFilter)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var postsQuery = this.FilterPosts(postFilter, this.postService.GetAll().ProjectTo<PostVM>());

            var paginatedPosts = this.PaginateList<PostVM>(pagination, postsQuery).ToList();

            foreach (var post in paginatedPosts)
            {
                post.Movie.PosterImageRelativeLink = FileManager.GetRelativeFilePath(post.Movie.PosterImageLink);
                post.Movie.OverallRating = post.Movie.Ratings.Any() ? post.Movie.Ratings.Average(s => s.Rating.Score) : 0;
            }

            int totalPages = this.GetTotalPages(pagination.PageSize, postsQuery.Count());

            PostListVM postListViewModel = new PostListVM
            {
                Posts = paginatedPosts,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            };

            this.LoadListMoviesDropdowns(postFilter);

            return this.View(postListViewModel);
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

        private void LoadListMoviesDropdowns(PostFilterVM postFilter)
        {
            this.ViewBag.MovieCategories = this.movieCategoryService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Name.ToLower(), Selected = postFilter.MovieCategory == e.Name.ToLower() })
                .ToList();

            this.ViewBag.Directors = this.directorService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Name.ToLower() })
                .ToList();

            this.ViewBag.Screenwriters = this.screenwriterService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Name.ToLower() })
                .ToList();

            this.ViewBag.Composers = this.composerService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Name.ToLower() })
                .ToList();
        }

        private IQueryable<PostVM> FilterPosts(PostFilterVM filter, IQueryable<PostVM> query)
        {
            if (string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                query = query.OrderBy(q => q.Movie.Name);
            }
            else
            {
                switch (filter.OrderBy.Trim().ToLower())
                {
                    case "alphabetical":
                        {
                            query = query.OrderBy(q => q.Movie.Name);
                        }

                        break;
                    case "rating":
                        {
                            query = query.OrderBy(q => q.Movie.Ratings.Average(s => s.Rating.Score));
                        }

                        break;
                    case "category":
                        {
                            query = query.OrderBy(q => q.Movie.Categories.First().Category.Name);
                        }

                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.MovieName))
            {
                query = query.Where(q => q.Movie.Name.ToLower().Contains(filter.MovieName.ToLower().Trim()));
            }

            if (!string.IsNullOrWhiteSpace(filter.MovieCategory))
            {
                query = query.Where(q => q.Movie.Categories.First().Category.Name.ToLower() == filter.MovieCategory);
            }

            return query;
        }
    }
}
