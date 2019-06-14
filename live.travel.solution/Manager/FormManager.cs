using live.travel.solution.Data;
using live.travel.solution.Models.Core;
using live.travel.solution.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
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
            return await _context.People.AsNoTracking().Where(x => x.IdentityUserId == id).FirstOrDefaultAsync();
        }

        public async Task Remove(string id, string detail) {
            var form = await _context.Forms.SingleOrDefaultAsync(x => x.Id == id);
            form.IsDeleted = true;
            //form.Detail = detail;
            _context.Update(form);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeStatus(string id, FormStatus status) {
            var form = await _context.Forms.Where(x => x.Id == id).FirstOrDefaultAsync();
            form.Status = status;
            _context.Update(form);
            await _context.SaveChangesAsync();
        }

        public async Task<Form> GetDetail(string id) {
            return await _context.Forms.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Form>> ListByUser(string id) {
            var person = await GetPerson(id);
            return await _context.Forms.Where(x => !x.IsDeleted && x.PersonId == person.Id)
                .AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<List<KeyValuePair<string,int>>> ListCounters(string id){
            var now = DateTime.UtcNow;

            var person = await GetPerson(id);
            var query = _context.Forms.Where(x => !x.IsDeleted && x.PersonId == person.Id).AsNoTracking().AsQueryable();

            int all = await query.CountAsync();
            int daily = await query.Where(x => x.CreatedAt == now).AsNoTracking().CountAsync();
            int complete = await query.Where(x => x.Status == FormStatus.Prospected).AsNoTracking().CountAsync();

            return new List<KeyValuePair<string, int>>{
                new KeyValuePair<string, int>("daily", daily),
                new KeyValuePair<string, int>("all", all),
                new KeyValuePair<string, int>("complete", complete),
            };
        }

        public async Task<List<Form>> LastEntries(string id) {
            var person = await GetPerson(id);
            return await _context.Forms.Where(x => !x.IsDeleted && x.PersonId == person.Id)
                .AsNoTracking().OrderByDescending(x => x.CreatedAt).Take(10).ToListAsync();
        }
    }
}