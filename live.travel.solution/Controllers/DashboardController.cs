using live.travel.solution.Controllers.Base;
using live.travel.solution.Manager;
using live.travel.solution.Models;
using live.travel.solution.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace live.travel.solution.Controllers {
    [Authorize]
    public class DashboardController : CoreController {

        private readonly SiteManager _siteManager;
        private readonly PersonManager _personManager;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(PersonManager personManager, SiteManager siteManager, UserManager<IdentityUser> userManager) {
            _personManager = personManager;
            _siteManager = siteManager;
            _userManager = userManager;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Formularios() {
            return View();
        }

        public async Task<IActionResult> Site() {
            var site = await _siteManager.GetCurrent(User?.Identity?.Name);
            return View((DashboardViewModel)site);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Site(DashboardViewModel vm) {

            try {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                await _siteManager.UpdateOrRegister(vm.BannerUri, vm.InstaUri, vm.FaceUri, vm.WhatsUri, vm.Job, vm.Presentation, user?.Id);

                if (!string.IsNullOrEmpty(vm.PhotoUri))
                    await _personManager.UpdatePhoto(vm.PhotoUri, user?.Id);

                return RedirectToAction(nameof(Site));
            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SiteJob(DashboardViewModel vm) {

            try {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                await _siteManager.UpdateJob(vm.Presentation, user?.Id);
                return RedirectToAction(nameof(Site));
            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
