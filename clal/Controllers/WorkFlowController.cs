using clal.Models;
using clal.Services;
using Microsoft.AspNetCore.Mvc;

namespace clal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkFlowController : ControllerBase
    {
        private readonly IWorkFlowEngine _workflowEngine;

        public WorkFlowController(IWorkFlowEngine workflowEngine)
        {
            _workflowEngine = workflowEngine;
        }

        [HttpPost("run")]
        public ActionResult<WorkFlowResponseDto> RunWorkFlow([FromBody] WorkFlowRequestDto request)
        {
            var result = _workflowEngine.Run(request);
            return Ok(result);
        }
    }
}
