using System.Collections.Generic;

namespace DbrauResultUI.Helpers
{
    public static class CacheService
    {
        //for caching object
        public static T GetCacheObject<T>(string cacheKey) where T : class
        {
            // return MemoryCache.Default.Get(cacheKey) as T;
            return null;
        }
        public static T SetCacheObject<T>(string cacheKey, T data, int seconds = 600) where T : class
        {
            //T item = MemoryCache.Default.Get(cacheKey) as T;
            //if (item == null)
            //{
            //    item = data;
            //    MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddSeconds(seconds));
            //}
            //return item;

            return null;
        }

        //for caching list
        public static IEnumerable<T> GetCacheList<T>(string cacheKey) where T : class
        {
            // return MemoryCache.Default.Get(cacheKey) as IEnumerable<T>;
            return null;
        }
        public static IEnumerable<T> SetCacheList<T>(string cacheKey, IEnumerable<T> data, int seconds = 600) where T : class
        {
            //IEnumerable<T> item = MemoryCache.Default.Get(cacheKey) as IEnumerable<T>;
            //if (item == null)
            //{
            //    item = data;
            //    MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddSeconds(seconds));
            //}
            //return item

            return null;
        }

        public static void ClearCache()
        {
            //List<string> cacheKeys = MemoryCache.Default.Select(ck => ck.Key).ToList();
            //foreach (string cacheKey in cacheKeys)
            //{
            //    MemoryCache.Default.Remove(cacheKey);
            //}
        }
    }
}
