using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    public class RecipientViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public IDictionary<string,string> SchemaValues { get; set; }

        public string Notes { get; set; }
    }
}
