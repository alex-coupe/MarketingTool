using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Recipient
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Notes { get; set; }

        public ICollection<List> Lists { get; set; }

        public virtual Client Client { get; set; }
    }
}
