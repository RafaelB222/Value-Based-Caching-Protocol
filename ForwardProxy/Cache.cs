using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace ForwardProxy
{
    internal class Cache
    {
        private Dictionary<string, ImageBlock> _cache;

        public Cache() 
        {
            _cache = new Dictionary<string, ImageBlock>();

        }

        public void AddItemToCache(string key, ImageBlock value)
        {
            _cache.Add(key, value);
        }

        public ImageBlock GetItemFromCache(string key)
        {
            return _cache[key]; 
        }

        public void Clear()
        {
            _cache.Clear(); 
        }

        public bool Contains(string key)
        {
            return _cache.ContainsKey(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key); 
        }

        public int Count
        {
            get { return _cache.Count; } 
        }

        public Dictionary<string, ImageBlock>.KeyCollection Keys
        {
            get { return _cache.Keys; }
        }

        public Dictionary<string, ImageBlock>.ValueCollection Values
        {
            get { return _cache.Values; }
        }

        public Dictionary<string, ImageBlock> GetCache()
        {
            return _cache;
        }

    }
}
