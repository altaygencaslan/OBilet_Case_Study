using Microsoft.Extensions.Caching.Memory;
using OBilet.Core.Cache.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.Cache.Concrete
{
    public class MemoryCacheManager : IMemoryCacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Clear(string key)
        {
            _memoryCache.Remove(key);
        }

        public void TryGetValue<T>(string key, out T item)
        {
            _memoryCache.TryGetValue<T>(key, out item);
        }

        public void SetWithExpire<T>(string key, T value, DateTime? expireDate = null)
        {
            DateTime date;
            if (expireDate != null)
            {
                date = expireDate.Value;
            }
            else
            {
                date = DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays(1).AddTicks(-1);
            }

            _memoryCache.Set(key, value, date);
        }
    }
}
