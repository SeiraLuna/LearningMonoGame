using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LearningMonoGame
{
    public class SerializationManager<T>
    {
        public Type Type;

        public SerializationManager()
        {
            Type = typeof(T);
        }

        public T Load(string path)
        {
            T instance;
            using (TextReader textReader = new StreamReader(path))
            {                
                var xmlS = new XmlSerializer(Type);
                instance = (T)xmlS.Deserialize(textReader);
            }

            return instance;
        }

        public void Save(string path, object obj)
        {
            using (TextWriter textWriter = new StreamWriter(path))
            {
                var xmlS = new XmlSerializer(Type);
                xmlS.Serialize(textWriter, obj);
            }
        }
    }
}
