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
        private readonly string[] titles;

        private Random random = new();

        public HeaderService(IWebHostEnvironment webHostEnvironment)
        {
            apps = GetData(webHostEnvironment.ContentRootPath, "apps");
            titles = GetData(webHostEnvironment.ContentRootPath, "titles");
        }

        public HeaderModel GetHeader()
        {
            int a = random.Next(apps.Length);
            int e = random.Next(titles.Length);
            return new HeaderModel(apps[a], titles[e]);
        }

        public string[] GetApps() => apps;

        public string[] GetTitles() => titles;

        private string[] GetData(string contentRootPath, string fileName)
        {
            var path = Path.Combine(contentRootPath, "Data", $"{fileName}.json");
            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<string[]>(json);
        }
    }
}
