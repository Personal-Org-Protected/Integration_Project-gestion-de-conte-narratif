using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorytellingWebApp.Factory.ConsumeApi;
using StorytellingWebApp.Models;

namespace StorytellingWebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConsume<User> _consume;
        private readonly IConsume<UserUpdate> _ConsumerUpdate;

        public UserController(IConsume<User> consume)
        {
            _consume=consume;
        }
        // GET: UserController
        public async Task<ActionResult> Index(int pgNumber)
        {
            var result=await _consume.GetAll($"?pgNumber={pgNumber}");
            return View(new UserVM()
            {
                PaginatedItems = result,
            });
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var result=await _consume.GetById($"/{id}");
            return View(result);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
               await _consume.Insert(user);
                return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _consume.GetById($"/{id}");
            return View(result);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Edit(int id, UserUpdate user)
        {
                await _ConsumerUpdate.Update($"/{id}", user);
                return RedirectToAction(nameof(Index));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Delete(string user_id)
        {
                await _consume.Delete($"/{user_id}");
                return RedirectToAction(nameof(Index));
        }
    }
}
