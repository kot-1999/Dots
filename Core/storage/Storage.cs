using System.Collections.Generic;

namespace dots.comments
{
    public interface Storage
    {
        void Add(string key,string value);
        List<KeyValuePair<string, string>> Get();
        string GetValue(string key);
        void SortList();
        void save();
        void load();
        void Refresh();
        void SetFile(string filename);
    }
}