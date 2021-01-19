using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Campaign
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public int ClientId { get; set; }

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
        
        public int CreatorId { get; set; }

       
        public int? ModifierId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Required]
        public int TimestepId { get; set; }
      
        public bool IsActive { get; set; } = true;

        public DateTime SendDate { get; set; }

        public int ListId { get; set; }
    }
}
