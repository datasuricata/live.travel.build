using live.travel.solution.Controllers.Base;
using live.travel.solution.Data;
using live.travel.solution.Models;
using live.travel.solution.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace live.travel.solution.Controllers {
    [Authorize]
    public class DashboardController : CoreController {

        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> Site() {
            var logged = User?.Identity?.Name;
            var site = await _context.Sites.Include(i => i.Person).Include(i => i.Person.IdentityUser)
                .Where(x => x.Person.IdentityUser.Email == logged).FirstOrDefaultAsync();
            return View((DashboardViewModel)site);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Site(DashboardViewModel vm) {

            try {
                if (!string.IsNullOrEmpty(vm?.Id)) {
                    var site = await _context.Sites.SingleOrDefaultAsync(x => x.Id == vm.Id);
                    site = SetProperties(vm);
                }
                return RedirectToAction(nameof(Site));
            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        private static Models.Core.Site SetProperties(DashboardViewModel vm) {
            return new Models.Core.Site {
                Id = vm.Id,
                BannerUri = vm.BannerUri,
                FaceUri = vm.FaceUri,
                InstaUri = vm.InstaUri,
                Job = vm.Job,
                Presentation = vm.Presentation,
                WhatsUri = vm.WhatsUri,
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
