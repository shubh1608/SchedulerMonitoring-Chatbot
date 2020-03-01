using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
