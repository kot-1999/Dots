using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Microsoft.AspNetCore.Http
{
    public static class SessionExtensionsTuke
    {
        public static object GetObject(this ISession session, string key)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(session.Get(key));
            return bf.Deserialize(stream);
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, value);
            long len = stream.Length;
            byte[] serializedObject = new byte[len];
            Array.Copy(stream.GetBuffer(), serializedObject, len);
            session.Set(key, serializedObject);
        }
    }
}
