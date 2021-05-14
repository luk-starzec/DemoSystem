using SenderProvider.Models;
using SenderProvider.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenderProvider.Services
{
    public class SenderService : ISenderService
    {
        private readonly INameService nameService;
        private readonly IJobTitleFirstPartService jobTitleFirstPartService;
        private readonly IJobTitleSecondPartService jobTitleSecondPartService;
        private readonly IJobTitleThirdPartService jobTitleThirdPartService;

        public SenderService(INameService nameService, IJobTitleFirstPartService jobTitleFirstPartService, IJobTitleSecondPartService jobTitleSecondPartService, IJobTitleThirdPartService jobTitleThirdPartService)
        {
            this.nameService = nameService;
            this.jobTitleFirstPartService = jobTitleFirstPartService;
            this.jobTitleSecondPartService = jobTitleSecondPartService;
            this.jobTitleThirdPartService = jobTitleThirdPartService;
        }

        public async Task<SenderModel> GetSender()
        {
            var name = await nameService.GetNameAsync();

            var firstPart = await jobTitleFirstPartService.GetJobTitleFirstPartAsync();
            var secondPart = await jobTitleSecondPartService.GetJobTitleSecondPartAsync();
            var thirdPart = await jobTitleThirdPartService.GetJobTitleThirdPartAsync();
            var jobTitle = $"{firstPart} {secondPart} {thirdPart}".Replace("  ", " ");

            return new SenderModel(name, jobTitle);
        }
    }
}
