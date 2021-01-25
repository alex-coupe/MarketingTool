﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    public class TemplateHistoryItemViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int Version { get; set; }

        [Required]
        public bool Protected { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifyingUser { get; set; }
    }
}
