using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlMinify.Data.Abstract;
using UrlMinify.Shared.Entities;

namespace UrlMinify.Data.Concrate
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private dbContext db;

        public DatabaseHelper(dbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Delete URL record from database
        /// </summary>
        /// <param name="key"></param>
        public void deleteUrl(string key)
        {
            var deletedUrl = getUrl(key);

            if (deletedUrl != null)
                db.Remove(deletedUrl);

            db.SaveChanges();
        }

        /// <summary>
        /// get dencrypted orginal url by encrypted key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UrlItem? getUrl(string key)
        {
            var url = db.Urls.AsQueryable().FirstOrDefault(x => x.Minified.Equals(key));
            return url;
        }

        /// <summary>
        /// saves the original url and returns a encrypted key
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<UrlItem?> insertUrlAsync(UrlItem data)
        {
            
            await db.Set<UrlItem>().AddAsync(data);
            db.SaveChanges();

            return getUrl(data.Minified);
        }
    }
}
