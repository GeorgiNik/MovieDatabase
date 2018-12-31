namespace MovieDatabase.Web.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Web.Controllers.Base;
    using MovieDatabase.Web.ViewModels.Posts;

    public class PostsController : BaseController
    {
        private ICrudService<Post> postService;

        public PostsController(ICrudService<Post> postService)
        {
            this.postService = postService;
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
    }
}
