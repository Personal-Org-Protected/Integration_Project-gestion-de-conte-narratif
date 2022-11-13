using Microsoft.AspNetCore.Mvc;
using StorytellingWebApp.Factory.ConsumeApi;
using StorytellingWebApp.Models;

namespace StorytellingWebApp.Controllers
{
    public class ImageClientController : Controller
    {
        private readonly IConsume<ImageClientVM> _consume;
        public ImageClientController(IConsume<ImageClientVM> consume)
        {
            _consume = consume; 
        }
        public async Task<ActionResult> GetClientImage(string search, int pgNumber)
        {
            ImageClientVM paginatedItems;
            if (search is null) paginatedItems = await _consume.GetAllClient($"/ImageClient?querySearch=Space&pageNumber={pgNumber}");
            else paginatedItems = await _consume.GetAllClient($"/ImageClient?querySearch={search}&pageNumber={pgNumber}");

            return View(paginatedItems);
        }
    }
}
