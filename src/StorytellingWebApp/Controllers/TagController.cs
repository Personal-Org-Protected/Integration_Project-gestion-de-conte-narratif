using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorytellingWebApp.Factory.ConsumeApi;
using StorytellingWebApp.Models;

namespace StorytellingWebApp.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly IConsume<TagVM> _consume;
        private readonly IConsume<Tag> _consumeTag;
        public TagController(IConsume<TagVM> consume, IConsume<Tag> consumeTag)
        {
            _consumeTag = consumeTag;
            _consume = consume;
        }

        public async  Task<ActionResult> Index()
        {
            return View(await _consume.GetAll());
        }
        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Tag tag)
        {
                await _consumeTag.Insert(tag);
                return RedirectToAction(nameof(Index));
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Delete(int id)
        {
                await _consumeTag.Delete($"/{id}");
                return RedirectToAction(nameof(Index));
        }
    }
}
