using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportService.Models;
using ReportService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService issueService;
        private readonly ILogger<IssuesController> logger;

        public IssuesController(IIssueService issueService, ILogger<IssuesController> logger)
        {
            this.issueService = issueService;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IssueModel[]> GetIssues()
        {
            return issueService.GetIssues();
        }
    }
}
