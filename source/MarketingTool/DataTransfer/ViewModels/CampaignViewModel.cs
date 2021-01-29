using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    public class CampaignViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string SenderEmail { get; set; }

        [Required]
        public int TemplateId { get; set; }

        [Required]

        public string CreatingUser { get; set; }


        public string ModifyingUser { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime SendDate { get; set; }

        public DateTime? ProcessedTimestamp { get; set; }

        public int ListId { get; set; }
    }
}
