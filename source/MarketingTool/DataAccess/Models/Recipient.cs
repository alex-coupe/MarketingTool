using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Recipient
    {
        private string _jsonData = string.Empty;
        [Key]
        public int Id { get; init; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public int ClientId { get; set; }

        [NotMapped]
        public JObject SchemaValues
        {
            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(_jsonData) ? "{}" : _jsonData);
            }
            set
            {
                _jsonData = value.ToString();
            }
        }

        public string Notes { get; set; }

        public ICollection<List> Lists { get; set; }

        public virtual Client Client { get; set; }
    }
}
