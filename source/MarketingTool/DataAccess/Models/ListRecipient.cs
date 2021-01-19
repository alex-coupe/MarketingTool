using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ListRecipient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ListId { get; set; }

        [Required]
        public int RecipientId { get; set; }
    }
}
