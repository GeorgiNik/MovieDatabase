namespace MovieDatabase.Web.Areas.Admin.Models.Screenwriters
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Base;

    public class ScreenwriterListVM : PaginatedWithMappingVM<Director>
    {
        public List<ScreenwriterVM> Screenwriters { get; set; }
    }
}