using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace LoginProject
{
    internal interface ISerializable
    {
        string Filename { get; }
    }

    internal static class SerializeManager
    {
        

        private static string CreateAndGetPath(string filename)
        {
            if (!Directory.Exists(StaticResources.ClientDirPath))
                Directory.CreateDirectory(StaticResources.ClientDirPath);

            return Path.Combine(StaticResources.ClientDirPath, filename);
        }

        public static void Serialize<TObject>(TObject obj) where TObject : ISerializable
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var filename = CreateAndGetPath(obj.Filename);

                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, obj);
                }
            }
            catch (Exception e)
            {
                // TODO Add logging
                throw;

            }
        }

        public static TObject Deserialize<TObject>(string filename) where TObject : ISerializable
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    return (TObject) formatter.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                // TODO add logging
                throw;
            }
        }
    }
}
