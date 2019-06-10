using live.travel.solution.Data;
using live.travel.solution.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace live.travel.solution.Manager {
    public class SiteManager {
        private readonly ApplicationDbContext _context;

        public SiteManager(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Models.Core.Site> GetCurrent(string email) {
            return await _context.Sites.Include(i => i.Person).Include(i => i.Person.IdentityUser)
                .Where(x => x.Person.IdentityUser.Email == email).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Models.Core.Site> GetPublic(string name) {
            return await _context.Sites.Include(i => i.Person)
                .Where(x => Utils.RemoveSpaces(x.Person.Name)
                .ToLower() == name.ToLower()).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task Register(string banner, string insta, string face, string whats, string job, string presentation, string personId) {
            await _context.Sites.AddAsync(new Models.Core.Site {
                BannerUri = banner,
                InstaUri = insta,
                FaceUri = face,
                WhatsUri = whats,
                Job = job,
                Presentation = presentation,
                PersonId = personId,
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateJob(string presentation, string identityId) {
            var site = GetByUserId(identityId);

            if (site is null) {
                var person = _context.People.SingleOrDefault(x => x.IdentityUserId == identityId);
                await Register(null, null, null, null, null, presentation, person?.Id);
                return;
            } else {
                site.Presentation = presentation;
                _context.Update(site);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrRegister(string banner, string insta, string face, string whats, string job, string presentation, string identityId) {
            var site = GetByUserId(identityId);

            if (site is null) {
                var person = _context.People.SingleOrDefault(x => x.IdentityUserId == identityId);
                await Register(banner, insta, face, whats, job, presentation, person?.Id);
                return;
            } else {

                site.BannerUri = banner;
                site.InstaUri = insta;
                site.FaceUri = face;
                site.WhatsUri = whats;
                site.Job = job;

                _context.Update(site);
                await _context.SaveChangesAsync();
            }
        }

        public Models.Core.Site GetByUserId(string identityId) {
            return _context.Sites.Include(i => i.Person).Where(x => x.Person.IdentityUserId == identityId).FirstOrDefault();
        }
    }
}
