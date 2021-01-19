using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class CampaignJob
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public int CampaignId { get; set; }

        public virtual Campaign Campaign { get; set; }

        [Required]
        public string RecipientEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string SenderEmail { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime ProcessingDateTime { get; set; }
    }
}
