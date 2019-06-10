using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace live.travel.solution.Models.Core.Base {
    public class EntityBase {
        #region [ attributes ]

        [Key]
        public string Id { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region [ ctor ]

        protected EntityBase() {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTimeOffset.UtcNow;
        }

        #endregion

        public static string LetterHash(int length) {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
