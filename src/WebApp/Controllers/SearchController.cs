using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
           return RedirectToAction("Index", "Users", new { username = model.Username });
        }
    }
}