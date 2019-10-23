using System.ComponentModel.DataAnnotations;

namespace Pegasssus.Common.Models
{
    public class AssingRequest
    {
        [Required]
        public int AgendaId { get; set; }

        [Required]
        public int OrganizerId { get; set; }

        [Required]
        public int EventId { get; set; }

        public string Remarks { get; set; }
    }
}
