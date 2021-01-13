using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class List
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int CreatorId { get; set; }

        public int ModifierId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<Recipient> Recipients { get; set; }
    }
}
