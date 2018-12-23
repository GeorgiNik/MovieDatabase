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
    using MovieDatabase.Services.Identity;
    using MovieDatabase.Web.Areas.Admin.Controllers.Base;
    using MovieDatabase.Web.Areas.Admin.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Users;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Admin")]
    public class UsersController : EntityListController
    {
        private ApplicationUserManager<ApplicationUser> userManager;

        public UsersController(ApplicationUserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(PaginationVM pagination)
        {
            string currentId = this.userManager.GetUserId(this.User);
            var usersQuery = this.userManager.Users.Where(u => u.Id != currentId).ProjectTo<UserVM>();

            var paginatedUsers = this.PaginateList<UserVM>(pagination, usersQuery).ToList();

            int totalPages = this.GetTotalPages(pagination.PageSize, usersQuery.Count());

            return this.View(new UserListVM
            {
                Users = paginatedUsers,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            });
        }
    }
}