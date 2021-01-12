using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TemplateHistory
    {
        public int Id { get; set; }

        public int TemplateId { get; set; }

        public string Content { get; set; }

        public int Version { get; set; }
    }
}
