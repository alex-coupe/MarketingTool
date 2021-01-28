using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("List")]
        public int ListId { get; set; }

        [Required]
        [ForeignKey("Recipient")]
        public int RecipientId { get; set; }

        public virtual Recipient Recipient { get; set; }
    }
}
