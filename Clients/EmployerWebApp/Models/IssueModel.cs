﻿using Microsoft.AspNetCore.Components;
using System;
using System.Linq;

namespace EmployerWebApp.Models
{
    public class IssueModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string App { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sender { get; set; }
        public string Priority { get; set; }
        public string Employee { get; set; }
        public EventLogModel[] Logs { get; set; }

        public bool DetailsVisible { get; set; }

        public EventLogModel[] SortedLogs => Logs.OrderByDescending(r => r.Date).ToArray();
    }
}
