using live.travel.solution.Controllers.Base;
using live.travel.solution.Manager;
using live.travel.solution.Models;
using live.travel.solution.Models.Core;
using live.travel.solution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;

namespace live.travel.solution.Controllers {
    public class ProfileController : CoreController {

        private readonly SiteManager _siteManager;
        private readonly FormManager _formManager;

        public ProfileController(SiteManager siteManager, FormManager formManager) {
            _siteManager = siteManager;
            _formManager = formManager;
        }

        //[Route("{id}")]
        public async Task<IActionResult> Index(string id = "") {
            try {
                if (!string.IsNullOrEmpty(id)) {
                    var site = await _siteManager.GetPublic(id);
                    return View((DashboardViewModel)site);
                }

                var email = User.Identity.Name;

                if (!string.IsNullOrEmpty(email)) {
                    var site = await _siteManager.GetCurrent(email);

                    if(site is null) {
                        SetMessage("Configure seu site antes de continuar", MsgType.Info);
                        return RedirectToAction("Site", "Dashboard");
                    }

                    return View((DashboardViewModel)site);
                }

            } catch (Exception e) {
                SetMessage(e.Message, MsgType.Error);
            }

            return View(new DashboardViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Formulario(FormViewModel vm, string Radios = "") {
            try {

                var email = User.Identity.Name;
                await _formManager.Register(vm.Name, vm.Email, vm.BirthDate, vm.Tell, vm.City, vm.State, vm.Provincy, PlanType.Gold, email);
                return View(nameof(Index));

            } catch (Exception e) {
                SetMessage(e.Message, MsgType.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}