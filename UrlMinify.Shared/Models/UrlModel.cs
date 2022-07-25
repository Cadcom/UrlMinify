using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlMinify.Shared.Entities.Base;

namespace UrlMinify.Shared.Models
{
    public class UrlModel
    {
        public string Minified { get; set; }

        public string Url { get; set; }
    }
}
