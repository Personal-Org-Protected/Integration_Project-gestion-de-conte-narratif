using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorytellingWebApp.Factory.ConsumeApi;
using StorytellingWebApp.Models;

namespace StorytellingWebApp.Controllers
{
    [Authorize]
    public class StoryTellingController : Controller
    {
        private readonly IConsume<StoryTell> _consumer;
        public StoryTellingController(IConsume<StoryTell> consume)
        {
            _consumer = consume;
        }
        // GET: StoryTellingController
        public async Task<ActionResult> Index(int pgNumber)
        {
            var result = await _consumer.GetAll($"?pageNumber={pgNumber}");
            return View(result);
        }


        // GET: StoryTellingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoryTellingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StoryTell StoryTelling)
        {
                await _consumer.Insert(StoryTelling);
                return RedirectToAction(nameof(Index));
        }


        // POST: StoryTellingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
                await _consumer.Delete($"/{id}");
                return RedirectToAction(nameof(Index));
        }
    }
}
