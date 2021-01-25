using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    public class TemplateHistoryViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int Version { get; set; }

        public DateTime? EditedDate { get; set; }

        [ForeignKey("ModifyingUser")]
        public int? ModifierId { get; set; }

        public virtual UserViewModel ModifyingUser { get; set; }
    }
}
