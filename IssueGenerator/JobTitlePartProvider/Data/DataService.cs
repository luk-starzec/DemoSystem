using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Text.Json;

namespace JobTitlePartProvider.Data
{
    public class DataService : IDataService
    {
        private string[] data;

        private Random random = new();

        public DataService(IWebHostEnvironment webHostEnvironment, int fileIndex)
        {
            InitData(webHostEnvironment.ContentRootPath, fileIndex);
        }

        public string[] GetAllData()
        {
            return data;
        }

        public string GetData()
        {
            int index = random.Next(data.Length);
            return data[index];
        }

        private void InitData(string contentRootPath, int fileIndex)
        {
            var path = Path.Combine(contentRootPath, $"Data/data{fileIndex}.json");
            var json = File.ReadAllText(path);

            data = JsonSerializer.Deserialize<string[]>(json);
        }
    }
}
