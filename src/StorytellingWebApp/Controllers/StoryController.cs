using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorytellingWebApp.Factory.ConsumeApi;
using StorytellingWebApp.Models;

namespace StorytellingWebApp.Controllers
{
    [Authorize]
    public class StoryController : Controller
    {
        private readonly IConsume<Story> _consume;
        private readonly IConsume<Image> _consumeImage;

        public StoryController(IConsume<Story> consume, IConsume<Image> consumeImg)
        {
            _consumeImage = consumeImg;
            _consume = consume;
        }


        // GET: StoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _consume.GetById($"/{id}");
            return View(result);
        }

        // GET: StoryController/Create
        public async Task<ActionResult> Create(ChapitreCreationVM chapitreCreation)
        {
            chapitreCreation.image = await _consumeImage.GetById($"/{chapitreCreation.idImage}");
            return View(chapitreCreation);
        }

        // POST: StoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(ChapitreCreationVM chapitreCreation)
        {
              var result= await _consume.InsertWithResult(chapitreCreation.story);
                return RedirectToAction(nameof(Create),nameof(Chapitre),
                    new { idStoryTelling =chapitreCreation.idStoryTelling,
                        idImage=chapitreCreation.idImage,
                        idStory=result.idEntity
                    });
        }

        //// GET: StoryController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: StoryController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(int id, Story story)
        //{
        //    try
        //    {
        //       await _consume.Update($"/{id}", story);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // POST: StoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
                _consume.Delete($"/{id}");
                return RedirectToAction(nameof(Index));
        }
    }
}
