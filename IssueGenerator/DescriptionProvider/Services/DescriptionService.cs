using DescriptionProvider.Data;
using Microsoft.Extensions.Logging;
using System;

namespace DescriptionProvider.Services
{
    public partial class DescriptionService : Description.DescriptionBase
    {
        private readonly DescriptionDbContext dbContext;
        private readonly ILogger<DescriptionService> logger;
        private readonly Random random = new();

        public DescriptionService(DescriptionDbContext dbContext, ILogger<DescriptionService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
    }
}
