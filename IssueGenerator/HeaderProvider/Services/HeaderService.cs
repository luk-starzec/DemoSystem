using HeaderProvider.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeaderProvider.Services
{
    public class HeaderService : IHeaderService
    {
        private readonly string[] apps;
        private readonly string[] errors;

        private Random random = new();

        public HeaderService(IWebHostEnvironment webHostEnvironment)
        {
            apps = GetData(webHostEnvironment.ContentRootPath, "apps");
            errors = GetData(webHostEnvironment.ContentRootPath, "errors");
        }

        public HeaderModel GetHeader()
        {
            int a = random.Next(apps.Length);
            int e = random.Next(errors.Length);
            return new HeaderModel(apps[a], errors[e]);
        }


        private string[] GetData(string contentRootPath, string fileName)
        {
            var path = Path.Combine(contentRootPath, "Data", $"{fileName}.json");
            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<string[]>(json);
        }
    }
}
