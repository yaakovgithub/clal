using clal.Models;
using clal.Operations;

namespace clal.Services
{
    public class WorkFlowEngine : IWorkFlowEngine
    {
        private readonly Dictionary<string, IWorkFlowOperation> _operations;
        public WorkFlowEngine(IEnumerable<IWorkFlowOperation> operations)
        {
            _operations = operations.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
        }
        public WorkFlowResponseDto Run(WorkFlowRequestDto request, DateOnly? currentDate = null)
        {
            if (request.Account is null)
                throw new ArgumentException("Account is required.");

            if (request.Steps is null || request.Steps.Count == 0)
                throw new ArgumentException("At least one workflow step is required.");

            var stepMap = request.Steps.ToDictionary(x => x.Id);
            var currentStepId = request.Steps[0].Id;
            double currentValue = request.Account.InitialAmount.Value;
            var executedSteps = new List<int>();
            var visitedStepIds = new HashSet<int>();
            var today = currentDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
            while (true)
            {
                if (!visitedStepIds.Add(currentStepId!.Value))
                {
                    return new WorkFlowResponseDto
                    {
                        FinalAmount = Math.Round(currentValue, 4),
                        ExecutedStepIds = executedSteps,
                        StopReason = $"Cycle detected at step {currentStepId}"
                    };
                }
                if (!stepMap.TryGetValue(currentStepId, out var step))
                {
                    return new WorkFlowResponseDto { StopReason = $"Step {currentStepId} Not Found", ExecutedStepIds = executedSteps, FinalAmount = Math.Round(currentValue, 5) };
                }
                if (!_operations.TryGetValue(step.OperationName, out var operation))
                {
                    throw new ArgumentException($"Operation {step.OperationName} Not Supported");
                }
                executedSteps.Add(step.Id!.Value);
                currentValue = operation.Execute(currentValue, request.Account.AccountNumber.Value, today);
                if (currentValue >= 2000 || currentValue <= 0)
                {
                    return new WorkFlowResponseDto
                    {
                        FinalAmount = Math.Round(currentValue, 5),
                        ExecutedStepIds = executedSteps,
                        StopReason = "Stopped because output was >= 2000 or <= 0"
                    };
                }
                int nextId = currentValue < 1000 ? step.NextIdIfOutputIsLessThan!.Value : step.NextIdIfOutputIsGreaterThan!.Value;
                if (nextId == 0)
                {
                    return new WorkFlowResponseDto { FinalAmount=Math.Round(currentValue,5),ExecutedStepIds=executedSteps,StopReason="Stopped Because ID is 0"};
                }
                currentStepId = nextId;
            }

        }
    }
}
