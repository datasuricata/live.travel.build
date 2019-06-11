using live.travel.solution.Data;
using live.travel.solution.Models.Core;
using live.travel.solution.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace live.travel.solution.Manager {
    public class FormManager {
        private readonly ApplicationDbContext _context;

        public FormManager(ApplicationDbContext context) {
            _context = context;
        }

        public async Task Register(string name, string email, string birth, string tell, string city, string state, string provincy, PlanType plan, string id) {

            var form = new Form {
                BirthDate = Utils.FormatarData(birth),
                City = city,
                Email = email,
                Name = name,
                Provincy = provincy,
                State = state.ToUpper(),
                Tell = Utils.CleanFormat(tell),
                PersonId = id,
                Plan = plan,
                Status = FormStatus.New,
            };

            await _context.AddAsync(form);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetPerson(string id) {
            return await _context.People.AsNoTracking().SingleOrDefaultAsync(x => x.IdentityUserId == id);
        }

        public async Task<List<Form>> ListByUser(string id) {
            var person = await GetPerson(id);
            return await _context.Forms.Where(x => !x.IsDeleted && x.PersonId == person.Id)
                .AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync();
        }
    }
}