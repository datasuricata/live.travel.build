using live.travel.solution.Models.Core;
using System.ComponentModel.DataAnnotations;

namespace live.travel.solution.Models.ViewModels {
    public class FormViewModel {
        public string Id { get; set; }
        public string PersonId { get; set; }
        [Required(ErrorMessage = "obrigatório")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "e-mail inválido.")]
        public string Email { get; set; }
        public string BirthDate { get; set; }
        [Required(ErrorMessage = "telefone é obrigatório")]
        public string Tell { get; set; }
        public string City { get; set; }

        [Required]
        [MaxLength(2, ErrorMessage = "estado deve ser representado por 2 caracteres")]
        public string State { get; set; }
        public string Provincy { get; set; }
        public string CreatedAt { get; set; }
        public string Plan { get; set; }
        public string Status { get; set; }

        public static explicit operator FormViewModel(Form v) {
            return v == null ? new FormViewModel() : new FormViewModel {
                BirthDate = v.BirthDate,
                City = v.City,
                CreatedAt = v.CreatedAt?.ToString("dd/MM/yyyy HH:mm"),
                Email = v.Email,
                Id = v.Id,
                Name = v.Name,
                PersonId = v.PersonId,
                Plan = v.Plan.ToString(),
                Provincy = v.Provincy,
                State = v.State,
                Tell = v.Tell,
                Status = v.Status.ToString(),
            };
        }
    }
}