using live.travel.build.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace live.travel.build.Controllers {
    public class ProfileController : Controller {

        public IActionResult Index() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
