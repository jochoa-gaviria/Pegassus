using System.ComponentModel.DataAnnotations;

namespace Pegasssus.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
