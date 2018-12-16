namespace MovieDatabase.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Web.Areas.Admin.Models;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Admin")]
    public class ActorsController : Controller
    {
        private ICrudService<Actor> actorService;
        
        public ActorsController(ICrudService<Actor> actorService)
        {
            this.actorService = actorService;
        }
        
        public IActionResult Index()
        {
            var actors = this.actorService.GetAll().ProjectTo<ActorVM>().ToList();
            
            return this.View(actors);
        }
        
        public IActionResult Create()
        {
            return this.View();
        }
        
        public async Task<IActionResult> Create(ActorVM vm)
        {
            var actor = Mapper.Map<Actor>(vm);
            await this.actorService.Create(actor);
            
            return this.RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edit(string id)
        {
            var actor = await this.actorService.Get(id);
            var actorVm = Mapper.Map<ActorVM>(actor);
            
            return this.View(actorVm);
        }
        
        public async Task<IActionResult> Edit(ActorVM vm)
        {
            await this.actorService.Update(Mapper.Map<Actor>(vm));
            
            return this.RedirectToAction("Index");
        }
    }
}