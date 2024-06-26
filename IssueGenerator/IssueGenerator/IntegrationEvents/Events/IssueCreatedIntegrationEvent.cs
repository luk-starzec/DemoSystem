﻿using EventBus;
using IssueGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator.IntegrationEvents.Events
{
    public class IssueCreatedIntegrationEvent : IntegrationEvent
    {
        public IssueModel Issue { get; set; }

        public IssueCreatedIntegrationEvent()
        { }

        public IssueCreatedIntegrationEvent(IssueModel issue)
        {
            Issue = issue;
        }
    }
}
