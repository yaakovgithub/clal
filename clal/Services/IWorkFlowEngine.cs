using clal.Models;

namespace clal.Services
{
    public interface IWorkFlowEngine
    {
        WorkFlowResponseDto Run(WorkFlowRequestDto request, DateOnly? currentDate = null);
    }
}
