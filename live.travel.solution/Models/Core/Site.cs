using live.travel.solution.Models.Core.Base;

namespace live.travel.solution.Models.Core {
    public class Site : EntityBase {
        public string BannerUri { get; set; }
        public string InstaUri { get; set; }
        public string FaceUri { get; set; }
        public string WhatsUri { get; set; }
        public string Job { get; set; }
        public string Presentation { get; set; }

        public string PersonId { get; set; }
        public Person Person { get; set; }

        public Site() {
        }
    }
}
