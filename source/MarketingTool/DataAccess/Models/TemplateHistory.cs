﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TemplateHistory
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public int TemplateId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Version { get; set; }

        public DateTime? EditedDate { get; set; }

        [ForeignKey("ModifyingUser")]
        public int? ModifierId { get; set; }

        public string ModifyingUser { get; set; }

    }
}
