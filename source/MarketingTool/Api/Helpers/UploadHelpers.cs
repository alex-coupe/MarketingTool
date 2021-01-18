using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public static class UploadHelpers
    {
        private static async Task<string> ReadAsStringAsync(this IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }

        public static async Task<IEnumerable<Recipient>> ImportUpload(IFormFile file, RecipientSchema schema, IConfiguration configuration)
        {
            string[] permittedExtensions = { ".txt", ".csv", ".json" };
            var _fileSizeLimit = configuration.GetValue<long>("FileSizeLimit");
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext) || file.Length > _fileSizeLimit)
                return null;
            
            switch (ext)
            {
                case ".csv":
                case ".txt":
                  return await CsvUploadParser(file, schema);
                case ".json":
                    return await JsonUploadParser(file, schema);
                default:
                    return null;
            }
        }

        public static async Task<IEnumerable<Recipient>> JsonUploadParser(IFormFile file, RecipientSchema schema)
        {
            List<Recipient> recipients = new List<Recipient>();
            var text = await ReadAsStringAsync(file);
            JArray jsonInput = (JArray)JsonConvert.DeserializeObject(text);

            foreach(var item in jsonInput)
            {
                var json = item.ToObject<JObject>();
                var email = json.Properties().First().Value.ToString();
                json.First.Remove();
                
                recipients.Add(new Recipient
                {
                    ClientId = schema.ClientId,
                    EmailAddress = email,
                    SchemaValues = json
                   

                });
            }
            return recipients;
        }

            public static async Task<IEnumerable<Recipient>> CsvUploadParser(IFormFile file, RecipientSchema schema)
        {
            /* This code takes in a csv. The schema is required to validate against the input
             * Once the text is read, it is sanitised to remove line breaks.
             * The stride is calculated by working out the size of the schema (amount of properties) +1 for the email address
             * as it has a separate column in the db. After that, loop through the array of tokens increasing by stride, skipping the first element
             * as it will be the email address. Always take count + 1 so you have the email address to insert into the Recipient object
             *
             * TODO - allow commas in "" blocks i.e. "hello, world" would not split hello and world into different tokens
             */
            var text = await ReadAsStringAsync(file);
            JObject _schema = (JObject)JsonConvert.DeserializeObject(schema.Schema.ToString());
            List<Recipient> recipients = new List<Recipient>();
            if (!string.IsNullOrEmpty(text))
            {
                string cleaned = text.Replace("\n", "").Replace("\r", "");
                string[] subset = cleaned.Split(',');
                int stride = subset.Length / (_schema.Count+1);
                for (int i = 0; i != subset.Length; i+=stride)
                {
                    var item = subset.Skip(i).Take((_schema.Count + 1));
                    

                    var properties = item.Skip(1);

                    var json = new JObject();

                    for(int j = 0; j != properties.Count(); ++j)
                    {
                        json.Add(_schema.Properties().ElementAt(j).Name, properties.ElementAt(j));
                    }

                    recipients.Add(new Recipient
                    {
                        ClientId = schema.ClientId,
                        EmailAddress = item.First(),
                        SchemaValues = json
                    });

                }

            }
            return recipients;

        }
    }
}
