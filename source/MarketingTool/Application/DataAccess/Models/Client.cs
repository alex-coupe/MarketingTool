using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Client
    {
        [Key]
        public int Id { get; init; }

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [MaxLength(20)]
        [Required]
        public string AddressOne { get; set; }

        [MaxLength(20)]
        public string AddressTwo { get; set; }

        [MaxLength(25)]
        [Required]
        public string City { get; set; }

        [MaxLength(25)]
        [Required]
        public string State { get; set; }

        [MaxLength(25)]
        [Required]
        public string PostalCode { get; set; }

        [MaxLength(25)]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Phone]
        [Required]
        public string TelephoneOne { get; set; }

        [Phone]
        public string TelephoneTwo { get; set; }

        public int SubscriptionLevelId { get; set; }

        [ForeignKey("SubscriptionLevelId")]
        public SubscriptionLevel SubscriptionLevel { get; set; }
    }
}
