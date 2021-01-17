using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Client
    {
        [Key]
        public int Id { get; init; }

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        public int SubscriptionLevelId { get; set; }

        public virtual SubscriptionLevel SubscriptionLevel { get; set; }

    }
}
