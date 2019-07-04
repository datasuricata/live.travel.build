using live.travel.solution.Controllers.Base;
using live.travel.solution.Manager;
using live.travel.solution.Models;
using live.travel.solution.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace live.travel.solution.Controllers {
    [Authorize]
    public class DashboardController : CoreController {

        private readonly SiteManager _siteManager;
        private readonly PersonManager _personManager;
        private readonly FormManager _formManager;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(FormManager formManager, PersonManager personManager, SiteManager siteManager, UserManager<IdentityUser> userManager) {
            _personManager = personManager;
            _siteManager = siteManager;
            _userManager = userManager;
            _formManager = formManager;
        }

        public async Task<IActionResult> Index() {
            try {

                var user = await _userManager.GetUserAsync(HttpContext.User);
                var general = await _formManager.ListCounters(user?.Id);
                var last = await _formManager.LastEntries(user?.Id);

                var all = general.FirstOrDefault(x => x.Key == "all");
                var daily = general.FirstOrDefault(x => x.Key == "daily");
                var complete = general.FirstOrDefault(x => x.Key == "complete");

                return View(new DashboardViewGeneral {
                    Forms = last.ConvertAll(e => (FormViewModel)e),
                    All = all.Value,
                    Completes = complete.Value,
                    Daily = daily.Value,
                });

            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction("Index", "Home");
            }
            
        }

        #region [ formulario ]

        public IActionResult Remove(string id) {
            try {
                return View(new FormViewModel { Id = id });
            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Aprove(string id) {
            try {
                return View(new FormViewModel { Id = id });
            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Forms() {
            try {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var forms = await _formManager.ListByUser(user?.Id);
                return View(forms.ConvertAll(e => (FormViewModel)e));
            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Detail(string id) {
            try {

                var form = await _formManager.GetDetail(id);
                return View((FormViewModel)form);

            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(FormViewModel vm) {
            try {

                await _formManager.Remove(vm.Id, null);
                return View(nameof(Forms));

            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aprove(FormViewModel vm) {
            try {

                await _formManager.ChangeStatus(vm.Id, Models.Core.FormStatus.Prospected);
                return View(nameof(Forms));

            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion

        #region [ site ]

        public async Task<IActionResult> Site() {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var site = await _siteManager.GetCurrent(user?.Id);
            return View((DashboardViewModel)site);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Site(DashboardViewModel vm) {

            try {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var person = await _personManager.GetCurrent(user?.Id);
                await _siteManager.UpdateOrRegister(vm.BannerUri, vm.InstaUri, vm.FaceUri, vm.WhatsUri, vm.Job, vm.Presentation, user?.Id, person?.Id);

                if(vm.Name != person?.Name) 
                    await _personManager.UpdateName(vm.Name, user?.Id);

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
                var person = await _personManager.GetCurrent(user?.Id);
                await _siteManager.UpdateJob(vm.Presentation, user?.Id, person?.Id);

                return RedirectToAction(nameof(Site));
            } catch (Exception e) {
                SetMessage(e.Message, Models.Core.MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
