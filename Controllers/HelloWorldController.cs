using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers {
    public class HelloWorldController : Controller {
        public string Index() {
            return "This is my default action...";
        }

        public IActionResult Welcome(string name, int numTimes = 1) {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}