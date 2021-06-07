using JobTitlePartProvider.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace JobTitlePartProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> logger;
        private readonly IDataService dataService;

        public DataController(ILogger<DataController> logger, IDataService dataService)
        {
            this.logger = logger;
            this.dataService = dataService;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            logger.LogInformation("Called {action} in {controller} at {time}", nameof(Get), nameof(DataController), DateTime.UtcNow);
            return dataService.GetData();
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<string[]> GetAll()
        {
            logger.LogInformation("Called {action} in {controller} at {time}", nameof(GetAll), nameof(DataController), DateTime.UtcNow);
            return dataService.GetAllData();
        }
    }
}
