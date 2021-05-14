using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SenderProvider.Models;
using SenderProvider.Repo;
using SenderProvider.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SenderProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISenderService service;
        private readonly ILogger<SenderController> logger;

        public SenderController(ISenderService service, ILogger<SenderController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<SenderModel> Get()
        {
            return await service.GetSender();
        }

    }
}
