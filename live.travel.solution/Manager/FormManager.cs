using live.travel.solution.Data;
using live.travel.solution.Models.Core;
using live.travel.solution.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace live.travel.solution.Manager {
    public class FormManager {
        private readonly ApplicationDbContext _context;

        public FormManager(ApplicationDbContext context) {
            _context = context;
        }

        public async Task Register(string name, string email, string birth, string tell, string city, string state, string provincy, PlanType plan, string identity) {

            var person = _context.People.AsNoTracking().SingleOrDefault(x => x.IdentityUser.Email == email);

            var form = new Form {
                BirthDate = Utils.FormatarData(birth),
                City = city,
                Email = email,
                Name = name,
                Provincy = provincy,
                State = state,
                Tell = Utils.CleanFormat(tell),
                PersonId = person.Id,
                Plan = plan,
                Status = FormStatus.New,
            };

            await _context.AddAsync(form);
            await _context.SaveChangesAsync();
        }
    }
}