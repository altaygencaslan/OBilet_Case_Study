using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.Cache.Abstract
{
    public interface IMemoryCacheManager
    {
        public void SetWithExpire<T>(string key, T value, DateTime? expireDate = null);
        public void TryGetValue<T>(string key, out T item);
        public void Clear(string key);
    }
}
