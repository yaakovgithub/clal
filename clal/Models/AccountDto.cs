using System.ComponentModel.DataAnnotations;

namespace clal.Models
{
    public class AccountDto
    {
        [Required]
        public long? AccountNumber { get; set; }
        
        [Required]
        public double? InitialAmount { get; set; }
    }
}
