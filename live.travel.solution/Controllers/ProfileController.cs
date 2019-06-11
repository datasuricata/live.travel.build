using live.travel.solution.Controllers.Base;
using live.travel.solution.Manager;
using live.travel.solution.Models;
using live.travel.solution.Models.Core;
using live.travel.solution.Models.Helpers;
using live.travel.solution.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace live.travel.solution.Controllers {
    public class ProfileController : CoreController {

        private readonly SiteManager _siteManager;
        private readonly FormManager _formManager;
        private readonly UserManager<IdentityUser> _userManager;


        public ProfileController(SiteManager siteManager, FormManager formManager, UserManager<IdentityUser> userManager) {
            _siteManager = siteManager;
            _formManager = formManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string id = "") {
            try {
                if (!string.IsNullOrEmpty(id)) {
                    var site = await _siteManager.GetPublic(id);
                    return View((DashboardViewModel)site);
                } else {
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    var site = await _siteManager.GetCurrent(user.Id);

                    if (site is null) {
                        SetMessage("Configure seu site antes de continuar", MsgType.Info);
                        return RedirectToAction("Site", "Dashboard");
                    }

                    return View((DashboardViewModel)site);
                }
            } catch (Exception e) {
                SetMessage(e.Message, MsgType.Error);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Formulario(DashboardViewModel vm, PlanType plan) {
            try {

                if (string.IsNullOrEmpty(vm.Form?.PersonId)) {
                    SetMessage("Algo deu errado, tente novamente.", MsgType.Info);
                    return RedirectToAction(nameof(Index));
                }

                await _formManager.Register(vm.Form.Name, vm.Form.Email, vm.Form.BirthDate,
                    vm.Form.Tell, vm.Form.City, vm.Form.State, vm.Form.Provincy, plan, vm.Form.PersonId);

                SetMessage("Agradeçemos a sua preferencia, nossa equipe vai entrar em contato", MsgType.Success);
                return Redirect(Url.Content("~/" + Utils.RemoveSpaces(vm.Form.Id.ToLower())));

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