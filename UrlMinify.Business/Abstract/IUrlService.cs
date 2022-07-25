using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlMinify.Shared.Entities;

namespace UrlMinify.Business.Abstract
{
    public interface IUrlService
    {
        public Task<UrlItem> insertUrlAsync(string url);
        public void deleteUrl(string key);
        public UrlItem? getUrl(string key);
    }
}
