using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StorytellingWebApp.Controllers
{
    public class AccountController : Controller
    {
       public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0",new AuthenticationProperties() { RedirectUri = returnUrl });
        }
        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                RedirectUri = Url.Action("GetClientImage", "ImageClient", new {pgNumber=1})
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }
        public  IActionResult AccessDenied()
        {
             HttpContext.Abort();
            return RedirectToAction("GetClientImage", "ImageClient",new {pgNumber=1});
        }
    }
}
