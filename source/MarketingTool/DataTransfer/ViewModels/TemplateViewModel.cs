using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    public class TemplateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Version { get; set; }

        [Required]
        public bool Protected { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatingUser { get; set; }
        public string ModifyingUser { get; set; }

    }
}
