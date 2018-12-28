namespace MovieDatabase.Web.Areas.Admin.Models.MovieCategories
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Base;

    public class MovieCategoryListVM : PaginatedWithMappingVM<MovieCategory>
    {
        public List<MovieCategoryVM> MovieCategories { get; set; }
    }
}