using IssueGenerator.IntegrationEvents;
using IssueGenerator.IntegrationEvents.Events;
using IssueGenerator.Models;
using IssueGenerator.Services;
using IssueGenerator.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly ILogger<IssueController> logger;
        private readonly IIssueService issueService;
        private readonly IIntegrationEventService integrationEventService;

        public IssueController(IIssueService issueService, IIntegrationEventService integrationEventService, ILogger<IssueController> logger)
        {
            this.issueService = issueService;
            this.integrationEventService = integrationEventService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(GenerationOptions options)
        {
            var issues = await issueService.GenerateIssues(options);
            foreach (var item in issues)
            {
                logger.LogInformation($"Title: {item.Title} id: {item.Id}");

                var issueCreatedEvent = new IssueCreatedIntegrationEvent(item);
                integrationEventService.PublishThroughEventBus(issueCreatedEvent);
            }
            return NoContent();
        }
    }
}
