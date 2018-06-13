using System;
using System.Collections.Generic;

namespace stickeralbum.Game {
    public class FilterSettings {
        protected Dictionary<String, Object> InternalMap;
        public String Query;

        public FilterSettings()
            => InternalMap = new Dictionary<String, Object>();

        public Boolean ContainsKey(String key)
            => InternalMap.ContainsKey(key);

        public FilterSettings Add(String key, Object value) {
            InternalMap.Add(key, value);
            return this;
        }

        public Object Get(String key)
            => InternalMap.TryGetValue(key, out Object value) ? value : null;

        public FilterSettings Clear() {
            InternalMap.Clear();
            return this;
        }

        public String[] GetKeys() {
            String[] output = new String[InternalMap.Keys.Count];
            InternalMap.Keys.CopyTo(output, 0);
            return output;
        }

        public FilterSettings SetKey(String key, Object value) {
            InternalMap.Remove(key);
            InternalMap.Add(key, value);
            return this;
        }

        public FilterSettings RemoveKey(String key) {
            InternalMap.Remove(key);
            return this;
        }
    }
}
