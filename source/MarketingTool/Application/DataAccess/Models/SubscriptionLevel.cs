using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class SubscriptionLevel
    {
        [Key]
        public int Id { get; init; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int MaxUsers { get; set; }

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Cost { get; set; }


    }
}
