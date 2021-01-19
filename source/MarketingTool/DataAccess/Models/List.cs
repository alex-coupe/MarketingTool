using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [JsonIgnore]
        public virtual Client Client { get; set; }

        [Required]
        [ForeignKey("CreatingUser")]
        public int CreatorId { get; set; }

        [ForeignKey("ModifyingUser")]
        public int? ModifierId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public ICollection<Recipient> Recipients { get; set; }

        [JsonIgnore]
        public virtual User CreatingUser { get; set; }

        [JsonIgnore]
        public virtual User ModifyingUser { get; set; }
    }
}
