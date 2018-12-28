namespace MovieDatabase.Web.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Web.Areas.Admin.Controllers.Base;

    public class PostsController : BaseController
    {
        private ICrudService<Post> postService;

        public PostsController(ICrudService<Post> postService)
        {
            this.postService = postService;
        }

        [Route("admin/posts")]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/screenwriters/create")]
        public async Task<IActionResult> Create(object vm)
        {
            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");
                return this.View();
            }

            var post = Mapper.Map<Post>(vm);

            await this.postService.Create(post);

            this.AddAlert(true, "Successfully added post");

            return this.RedirectToAction("Index");
        }
    }
}
