using live.travel.solution.Models.Core.Base;

namespace live.travel.solution.Models.Core {
    public class Form : EntityBase {
        public PlanType Plan { get; set; }
        public FormStatus Status { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Tell { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Provincy { get; set; }
        public string PersonId { get; set; }

        public Form() {
        }
    }
}
