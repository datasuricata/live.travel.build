using live.travel.solution.Models.Core.Base;
using Microsoft.AspNetCore.Identity;

namespace live.travel.solution.Models.Core {
    public class Person : EntityBase {

        // full person name
        public string Name { get; set; }
        public string PhotoUri { get; set; }

        // documents
        public string CPF { get; set; }
        public string RG { get; set; }

        // address
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string CEP { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }

        // profile
        public Site Profile { get; set; }

        // user
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public Person() {
        }
    }
}
