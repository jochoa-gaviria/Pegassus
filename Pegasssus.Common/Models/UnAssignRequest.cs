using System.ComponentModel.DataAnnotations;

namespace Pegasssus.Common.Models
{
    public class UnAssignRequest
    {
        [Required]
        public int AgendaId { get; set; }
    }
}
