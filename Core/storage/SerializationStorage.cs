using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace dots.comments
{
    public class SerializationStorage : Storage
    {
        private List<KeyValuePair<String, String>> list = new List<KeyValuePair<string, string>>();
        private string filename;

        public void Add(string key, string value)
        {
            if(key!=null && value!=null)
            {
                list.Add(new KeyValuePair<string, string>(key,value));
            }
        }

        public List<KeyValuePair<string, string>> Get()
        {
            return list;
        }

      
        public string GetValue(string key)
        {
           
            foreach (var s in list)
            {
                if (s.Key.Equals(key))
                    return s.Value;
            }
            return null;
        }

        public void SortList()
        {
            for (int a = 0; a < list.Count-1; a++)
            {
                for (int b = 1; b < list.Count;b++)
                {
                    KeyValuePair<string, string> p1 = list[b];
                    KeyValuePair<string, string> p2 = list[b-1];
                    KeyValuePair<string, string> temp;
                    
                    try
                    {
                        if (int.Parse(p1.Value) > int.Parse(p2.Value))
                        {
                            temp = list[b];
                            list[b] = list[b - 1];
                            list[b-1] = temp;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("cant parse element to int");
                    }
                    
                        
                }
            }
        }



        public void save()
        {
            if(filename==null)
                return;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, list);
                stream.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void load()
        {
            if(filename==null)
                return;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (stream.Length != 0)
                    list = (List<KeyValuePair<String, String>>) formatter.Deserialize(stream);
                stream.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Refresh()
        {
            if(filename!=null) System.IO.File.WriteAllText(filename,string.Empty);
        }

        public void SetFile(string filename)
        {
            this.filename=filename;
        }
        
    }
}