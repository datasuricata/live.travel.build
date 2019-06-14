using live.travel.solution.Data;
using live.travel.solution.Models.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace live.travel.solution.Manager {
    public class PersonManager {
        private readonly ApplicationDbContext _context;

        public PersonManager(ApplicationDbContext context) {
            _context = context;
        }

        public async Task Register(string cpf, string rg, string name, string identityId) {
            await _context.People.AddAsync(new Models.Core.Person { CPF = cpf, RG = rg, Name = name, IdentityUserId = identityId });
            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetCurrent(string id) {
            return await _context.People.Where(x => x.IdentityUserId == id)
                .AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdatePhoto(string uri, string identityId) {
            var people = await _context.People.Where(x => x.IdentityUserId == identityId).FirstOrDefaultAsync();
            people.PhotoUri = uri;
            _context.Update(people);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateName(string name, string identityId) {
            var people = await _context.People.Where(x => x.IdentityUserId == identityId).FirstOrDefaultAsync();
            people.Name = name;
            _context.Update(people);
            await _context.SaveChangesAsync();
        }
    }
}
