using System.ComponentModel.DataAnnotations;

namespace clal.Models
{
    public class WorkFlowRequestDto
    {
        [Required]
        public AccountDto Account { get; set; }
        [Required]
        [MinLength(1)]
        public List<WorkFlowStepDto> Steps { get; set; }
    }
}
