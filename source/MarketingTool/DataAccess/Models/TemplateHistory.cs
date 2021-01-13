﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TemplateHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TemplateId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int Version { get; set; }
    }
}