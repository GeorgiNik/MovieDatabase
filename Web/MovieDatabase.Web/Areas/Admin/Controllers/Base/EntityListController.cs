namespace MovieDatabase.Web.Areas.Admin.Controllers.Base
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Web.Areas.Admin.Models;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Admin")]
    public class EntityListController : BaseController
    {
        protected IQueryable<T> PaginateList<T>(PaginationVM pagination, IQueryable<T> query)
        {
            var skip = (pagination.Page - 1) * pagination.PageSize;
            var take = pagination.PageSize;

            return query.Skip(skip).Take(take);
        }

        protected int GetTotalPages(int pageSize, int entityCount)
        {
            var totalPages = (int)Math.Ceiling(decimal.Divide(entityCount, pageSize));

            return totalPages;
        }
    }
}