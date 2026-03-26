namespace clal.Models
{
    public class WorkFlowResponseDto
    {
        public double FinalAmount { get; set; }
        public List<int> ExecutedStepIds { get; set; } = new();
        public string StopReason { get; set; } = string.Empty;
    }
}
