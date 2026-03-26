using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace clal.Models
{
    public class WorkFlowStepDto
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string OperationName { get; set; }

        [Required]
        public int? NextIdIfOutputIsLessThan { get; set; }

        [Required]
        public int? NextIdIfOutputIsGreaterThan { get; set; }

        [JsonPropertyName("nextIdIfOutsGreaterThan")]
        public int? NextIdIfOutsGreaterThanTypo
        {
            get => NextIdIfOutputIsGreaterThan;
            set => NextIdIfOutputIsGreaterThan = value;
        }
    }
}
