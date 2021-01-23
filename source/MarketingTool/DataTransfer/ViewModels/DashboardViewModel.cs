using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    public class DashboardViewModel
    {
        [Required]
        public int RecipientCount { get; set; }

        [Required]
        public int ListCount { get; set; }

        [Required]
        public int TotalCampaigns { get; set; }
    }
}
