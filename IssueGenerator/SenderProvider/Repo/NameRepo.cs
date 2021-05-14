using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SenderProvider.Repo
{
    public class NameRepo : INameRepo
    {
        private string[] firstNames;
        private string[] lastNames;

        private Random random = new();

        public NameRepo(IWebHostEnvironment webHostEnvironment)
        {
            firstNames = GetData(webHostEnvironment.ContentRootPath, "firstNames");
            lastNames = GetData(webHostEnvironment.ContentRootPath, "lastNames");
        }

        public string GetFirstName()
        {
            int index = random.Next(firstNames.Length);
            return firstNames[index];
        }

        public string GetLastName()
        {
            int index = random.Next(lastNames.Length);
            return lastNames[index];
        }


        private string[] GetData(string contentRootPath, string fileName)
        {
            var path = Path.Combine(contentRootPath, "Data", $"{fileName}.json");
            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<string[]>(json);
        }

    }
}
