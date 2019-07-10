using live.travel.solution.Controllers.Base;
using live.travel.solution.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace live.travel.solution.Controllers {
    public class ServiceController : CoreController {
        public IActionResult Presentations() {
            return View();
        }

        public IActionResult Trainings() {
            return View();
        }

        public IActionResult Mentoring() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
