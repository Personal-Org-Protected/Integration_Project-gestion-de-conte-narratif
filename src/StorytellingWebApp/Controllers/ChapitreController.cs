using StorytellingWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorytellingWebApp.Factory.ConsumeApi;
using Microsoft.AspNetCore.Authorization;

namespace StorytellingWebApp.Controllers
{
    [Authorize]
    public class ChapitreController : Controller
    {
        private readonly IConsume<Chapitre> _consumer;
        private readonly IConsume<Story> _consumerStory;
        private readonly IConsume<Image> _consumerImage;
        public ChapitreController(IConsume<Chapitre> consume, IConsume<Story> consumerStory, IConsume<Image> consumeImage)
        {
            _consumerImage=consumeImage;
            _consumerStory = consumerStory;
            _consumer = consume;
        }
        // GET: ChapitreController
        public async Task<ActionResult> Index(int idStoryTell, int pgNumber)
        {
            var result = await _consumer.GetAll($"?idStoryTell={idStoryTell}&pgNumber={pgNumber}");
            var data = new ChapitreCreationVM()
            {
                PaginatedChaps = result,
                idStoryTelling = idStoryTell,
                stories= await ImplementListDataStory(result),
                images=await ImplementListDataImage(result)

        };
            
            return View(data);
        }

        // GET: ChapitreController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result =await _consumer.GetById($"/{id}");
            var results = await ImplementData( result.IdImage, result.IdStory);
            return View(results);
        }

        // GET: ChapitreController/Create
        public ActionResult Create(ChapitreCreationVM chapitreCreation)
        {
            return View(chapitreCreation);
        }

        // POST: ChapitreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(ChapitreCreationVM chapitre)
        {
            var chapitreInsert=new Chapitre()
            {
                IdStory=chapitre.idStory,
                IdImage=chapitre.idImage,
                IdStoryTelling=chapitre.idStoryTelling,
            };

               await _consumer.Insert(chapitreInsert);
                return RedirectToAction(nameof(Index),new { idStoryTell =chapitre.idStoryTelling, pgNumber=1});
        }
 

        // POST: ChapitreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id,ChapitreCreationVM chapitre)
        {
              await _consumer.Delete($"/{id}");
              await _consumerStory.Delete($"/{chapitre.idStory}");
              return RedirectToAction(nameof(Index), new { idStoryTell = chapitre.idStoryTelling, pgNumber = 1 });
        }



        #region

        private async Task<List<Story>> ImplementListDataStory(PaginatedItems<Chapitre> paginatedItems)
        {
            var stories=new List<Story>();
            foreach(var item in paginatedItems.Items)
            {
                stories.Add(await _consumerStory.GetById($"/{item.IdStory}"));
            }
            return stories;
        }

        private async Task<List<Image>> ImplementListDataImage(PaginatedItems<Chapitre> paginatedItems)
        {
            var images = new List<Image>();
            foreach (var item in paginatedItems.Items)
            {
                images.Add(await _consumerImage.GetById($"/{item.IdImage}"));
            }
            return images;
        }

        private async Task<ChapitreCreationVM> ImplementData(int idImage,int idStory)
        {
            var resultImage = await _consumerImage.GetById($"/{idImage}");
            var resultStory = await _consumerStory.GetById($"/{idStory}");
            return new ChapitreCreationVM()
            {
                image = resultImage,
                story = resultStory,
            };
        }
        #endregion
    }
}
