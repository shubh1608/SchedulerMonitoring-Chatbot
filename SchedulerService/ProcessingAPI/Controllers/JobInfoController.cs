using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProcessingAPI.Service;

namespace ProcessingAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class JobInfoController : ControllerBase
    {
        private JobInfoService _jobStatusService { get; set; }
        public JobInfoController(IConfiguration configuration)
        {
            _jobStatusService = new JobInfoService(configuration);
        }

        [HttpGet]
        [Route("api/jobinfo")]
        public ActionResult GetStatus()
        {

            var res = _jobStatusService.GetJobStatus();
            return new JsonResult(res);
        }

        [HttpGet]
        [Route("api/jobinfo/{jobname}")]
        public ActionResult GetJobStatus(string jobname)
        {
            var jobDetails = _jobStatusService.GetJobExecutionDetails(jobname);
            return new JsonResult(jobDetails);
        }

        [HttpGet]
        [Route("api/jobinfo/validate/{jobname}")]
        public ActionResult ValidateJobName(string jobName)
        {
            bool res = jobName.ToLower() == "job-a" ? true : jobName.ToLower() == "job-a" ? true : false;
            return new JsonResult(res);
        }
    }
}
