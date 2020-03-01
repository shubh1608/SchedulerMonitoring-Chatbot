using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcessingAPI.Service;

namespace ProcessingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobInfoController : ControllerBase
    {
        private JobInfoService _jobStatusService { get; set; }
        public JobInfoController()
        {
            _jobStatusService = new JobInfoService();
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        [HttpGet]
        public ActionResult GetStatus()
        {
            return new JsonResult(new { Success = true, Result = "Empty yet!!!"});
        }

        public ActionResult GetJobStatus(string jobId)
        {
            return new JsonResult(new { });
        }
    }
}
