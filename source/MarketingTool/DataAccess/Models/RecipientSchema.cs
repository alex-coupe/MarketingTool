﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RecipientSchema
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        
        public JObject Schema { get; set; }
    }
}
