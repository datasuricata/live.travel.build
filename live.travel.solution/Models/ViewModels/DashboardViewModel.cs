using live.travel.solution.Models.Core;

namespace live.travel.solution.Models.ViewModels {
    public class DashboardViewModel {
        public string Id { get; set; }
        public string BannerUri { get; set; }
        public string PhotoUri { get; set; }
        public string InstaUri { get; set; }
        public string FaceUri { get; set; }
        public string WhatsUri { get; set; }
        public string Job { get; set; }
        public string Presentation { get; set; }
        public string PersonId { get; set; }

        public string Name { get; set; }

        public FormViewModel Form { get; set; } = new FormViewModel();

        public static explicit operator DashboardViewModel(Site v) {
            return v == null ? new DashboardViewModel() : new DashboardViewModel {
                Id = v.Id,
                Job = v.Job,
                PersonId = v.PersonId,
                Name = v.Person?.Name,
                PhotoUri = v.Person?.PhotoUri,
                FaceUri = v.FaceUri,
                WhatsUri = v.WhatsUri,
                InstaUri = v.InstaUri,
                BannerUri = v.BannerUri,
                Presentation = v.Presentation,
            };
        }
    }
}