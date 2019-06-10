using live.travel.solution.Controllers.Base;
using live.travel.solution.Data;
using live.travel.solution.Models;
using live.travel.solution.Models.Core;
using live.travel.solution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace live.travel.solution.Controllers {
    public class ProfileController : CoreController {

        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            try {

                //if (!string.IsNullOrEmpty(nome)) {
                //    var site = await _context.Profiles.Include(p => p.Person).Where(w => w.Person.Name == nome).FirstOrDefaultAsync();
                //    return View((DashboardViewModel)profile);
                //}

                var email = User.Identity.Name;

                if (!string.IsNullOrEmpty(email)) {
                    var site = await _context.People.Include(p => p.Profile).Where(w => w.IdentityUser.Email == email).Select(s => s.Profile).AsNoTracking().FirstOrDefaultAsync();
                    return View((DashboardViewModel)site);
                }

            } catch (Exception e) {
                SetMessage(e.Message, MsgType.Error);
            }

            return View(new DashboardViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}