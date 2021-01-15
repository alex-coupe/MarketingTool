using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Template
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public int CreatorId { get; set; }

        public int ModifierId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        public int Version { get; set; }

        public bool Protected { get; set; } = false;
    }
}
