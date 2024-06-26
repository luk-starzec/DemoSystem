﻿using EmployerWebApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IReportService
    {
        Task<List<IssueViewModel>> GetLastIssuesAsync();
    }
}