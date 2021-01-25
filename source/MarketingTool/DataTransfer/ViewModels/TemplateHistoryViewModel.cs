using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    public class TemplateHistoryViewModel
    {
        public string TemplateName { get; set; }

        public ICollection<TemplateHistoryItemViewModel> TemplateHistory { get; set; }
      
    }
}
