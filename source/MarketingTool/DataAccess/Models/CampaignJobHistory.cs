﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class CampaignJobHistory
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public int CampaignId { get; set; }
           
        [Required]
        public string RecipientEmail { get; set; }

        [Required]
        public DateTime ProcessedTimestamp { get; set; }

        [Required]
        public int EmailStatusCode { get; set; }

      
    }
}
