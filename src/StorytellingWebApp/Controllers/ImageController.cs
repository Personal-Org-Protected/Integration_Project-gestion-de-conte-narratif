using StorytellingWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorytellingWebApp.Factory.ConsumeApi;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace StorytellingWebApp.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly IConsume<Image> _consumer;
        private readonly IConsume<TagVM> _consumeTag;

        public ImageController(IConsume<Image> consume, IConsume<TagVM> consumTag)
        {
            _consumer = consume;
            _consumeTag= consumTag;
        }
        // GET: ImageController
        public async Task<ActionResult> Index(int IdTag,int pgNumber)
        {
            var result=new PaginatedItems<Image>();
            if (IdTag == 0) result = await _consumer.GetAll($"?pageNumber={pgNumber}");
            else result = await _consumer.GetAll($"/Tag?Tag={IdTag}&pageNumber={pgNumber}");
            var tags = await _consumeTag.GetAll();
            var imageVM = new ImageVM()
            {
                selectListItems=new SelectList(tags.Tags, "IdTag", "NameTag"),
                PaginatedItems= result

        };
            return View(imageVM);
        }


        // GET: ImageController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _consumer.GetById($"/{id}");
            return View(result);
        }

        public async Task<ActionResult> CreateFromClient(string Uri)
        {
            var tag = await _consumeTag.GetAll();
            var imageVM = new ImageVM()
            {
                Uri = Uri,
                selectListItems = new SelectList(tag.Tags, "IdTag", "NameTag")
            };
            return View(imageVM);
        }

        // GET: ImageController/Create
        public async Task<ActionResult> Create()
        {
            var tag = await _consumeTag.GetAll();
            var imageVM = new ImageVM() 
            { 
                selectListItems= new SelectList(tag.Tags, "IdTag", "NameTag")
            };
            return View(imageVM);
        }

        // POST: ImageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Image image)
        {
                await _consumer.Insert(image);
                return RedirectToAction(nameof(Index));
        }

        // GET: ImageController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var tag = await _consumeTag.GetAll();
            var imageVM = new ImageVM()
            {
                image= await _consumer.GetById($"/{id}"),
                selectListItems = new SelectList(tag.Tags, "IdTag", "NameTag")
            };
            return View(imageVM);
        }

        // POST: ImageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Image image)
        {
                await _consumer.Update($"/{id}", image);
                return RedirectToAction(nameof(Index));
        }


        // POST: ImageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
                await _consumer.Delete($"/{id}");
                return RedirectToAction(nameof(Index));
        }

        private async Task<ImageVM> DataRetrieve(int id)
        {
            var tag = await _consumeTag.GetAll();
           return new ImageVM()
            {
                image = await _consumer.GetById($"/{id}"),
                selectListItems = new SelectList(tag.Tags, "IdTag", "NameTag"),
                PaginatedItems = await _consumer.GetAll($"?pageNumber={1}")
           };
        }

        public async Task<ActionResult> SelectImage(ChapitreCreationVM chapitreCreation)
        {
            var result = await _consumer.GetAll($"?pageNumber={1}");
            chapitreCreation.PaginatedItems = result;
            return View (chapitreCreation);
        }


    }
}
