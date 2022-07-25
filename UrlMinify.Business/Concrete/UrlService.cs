using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlMinify.Business.Abstract;
using UrlMinify.Data.Abstract;
using UrlMinify.Shared.Entities;

namespace UrlMinify.Business.Concrete
{
 
    /// <summary>
    /// Business layer for processes
    /// </summary>
    public class UrlService : IUrlService
    {
        IDatabaseHelper databaseHelper;
        public UrlService(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public UrlService()
        {
        }

        /// <summary>
        /// Delete URL record from database
        /// </summary>
        /// <param name="key"></param>
        public void deleteUrl(string key)
        {
            databaseHelper.deleteUrl(key);
        }


        /// <summary>
        /// get dencrypted orginal url by encrypted key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UrlItem? getUrl(string key)
        {
            return databaseHelper.getUrl(key);
        }


        /// <summary>
        /// saves the original url and returns a encrypted key
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<UrlItem> insertUrlAsync(string url)
        {
            UrlItem urlItem = new UrlItem()
            {
                CreateDate = DateTime.UtcNow,
                Minified= minifyUrl(url),
                Url= url
            };
            return await databaseHelper.insertUrlAsync(urlItem);
        }

        /// <summary>
        /// Url unique Encryption process
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string minifyUrl(string url) {
            string urlsafe = string.Empty;

            Enumerable.Range(0, url.Length)
              .OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => urlsafe += Convert.ToChar(url[i]));
            int starter = new Random().Next(0, urlsafe.Length-7);
            urlsafe = urlsafe.Substring(starter, 7);
            return urlsafe;
        }
    }
}
