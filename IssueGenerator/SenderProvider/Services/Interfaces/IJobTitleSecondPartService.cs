﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenderProvider.Services.Interfaces
{
    public interface IJobTitleSecondPartService
    {
        Task<string> GetJobTitleSecondPartAsync();
    }
}