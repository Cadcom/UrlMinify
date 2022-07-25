using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlMinify.Shared.Entities.Base;

namespace UrlMinify.Shared.Entities
{
    public class UrlItem: BaseClass
    {
        [Key]
        [Required]
        [MaxLength(20)]
        public string Minified { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
