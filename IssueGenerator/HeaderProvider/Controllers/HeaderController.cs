using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeaderProvider.Services;
using HeaderProvider.Models;

namespace HeaderProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly IHeaderService service;
        private readonly ILogger<HeaderController> logger;

        public HeaderController(IHeaderService service, ILogger<HeaderController> logger)
        {
            this.service = service;
            this.logger = logger;
        }
        [HttpGet]
        public ActionResult<HeaderModel> Get()
        {
            return service.GetHeader();
        }
    }
}
