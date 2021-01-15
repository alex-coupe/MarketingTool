using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class EmailStatus
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }
    }
}
