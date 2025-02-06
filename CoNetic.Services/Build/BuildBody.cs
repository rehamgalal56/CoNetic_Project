using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoNetic.Services.Build
{
    public class BuildBody
    {


        public static string GenerateEmailBody(string template, Dictionary<string, string> templateModel, IWebHostEnvironment webHostEnvironment)
        {
            string directory = Path.Combine(webHostEnvironment.WebRootPath, "Templates");

            var templatepath = $"{directory}/{template}.html";

            var stremReader = new StreamReader(templatepath);
            var body = stremReader.ReadToEnd();
            stremReader.Close();
            foreach (var item in templateModel)
            {
                body = body.Replace(item.Key, item.Value);

            }
            return body;
        }

    }
}
