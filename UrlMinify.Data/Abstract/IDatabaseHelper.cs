using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlMinify.Shared.Entities;
using UrlMinify.Shared.Models;

namespace UrlMinify.Data.Abstract
{
    public interface IDatabaseHelper
    {
        public Task<UrlItem> insertUrlAsync(UrlItem data);
        public void deleteUrl(string key);
        public UrlItem? getUrl(string key);
    }
}
