namespace MovieDatabase.Web.Areas.Identity.Pages
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using MovieDatabase.Common.Exceptions;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Identity;
    using MovieDatabase.Web.Areas.Identity.Pages.Account.OutputModels;

    [AllowAnonymous]
    public class ThankYouForRegisteringModel : PageModel
    {
        private readonly ApplicationUserManager<ApplicationUser> userManager;

        public ThankYouForRegisteringModel(ApplicationUserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public ThankYouForRegisteringOutputModel Output { get; set; }

        public async Task OnGet(string userId)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new EntityNotFoundException("userId");
            }

            this.Output = new ThankYouForRegisteringOutputModel
            {
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname
            };
        }
    }
}