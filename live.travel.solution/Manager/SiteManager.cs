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

        public async Task<Models.Core.Site> GetCurrent(string id) {
            return await _context.Sites.Include(i => i.Person).Include(i => i.Person.IdentityUser)
                .Where(x => x.Person.IdentityUserId == id).AsNoTracking().FirstOrDefaultAsync();
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

        public async Task UpdateJob(string presentation, string identityId, string personId) {
            var site = GetSiteByUser(identityId);

            if (site is null) {
                await Register(null, null, null, null, null, presentation, personId);
                return;
            } else {
                site.Presentation = presentation;
                _context.Update(site);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrRegister(string banner, string insta, string face, string whats, string job, string presentation, string identityId, string personId) {
            var site = GetSiteByUser(identityId);

            if (site is null) {
                await Register(banner, insta, face, whats, job, presentation, personId);
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

        public Models.Core.Site GetSiteByUser(string identityId) {
            return _context.Sites.Include(i => i.Person).Where(x => x.Person.IdentityUserId == identityId).FirstOrDefault();
        }
    }
}
