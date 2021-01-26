using System;
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
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Version { get; set; }

        [Required]
        public bool Protected { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("ModifyingUser")]
        public int? ModifierId { get; set; }

        public virtual User ModifyingUser { get; set; }

        public virtual Template Template { get; set; }

    }
}
