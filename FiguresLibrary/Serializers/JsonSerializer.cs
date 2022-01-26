using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiLibrary.Serializers
{
    public class JsonSerializer
    {
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = Newtonsoft.Json.JsonConvert.SerializeObject(objectToWrite, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    //Formatting = Formatting.Indented,
                });

                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }                
            }
        }

        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();

                JsonConverter[] converters = { new FiguresJsonConverter() };

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(fileContents, new Newtonsoft.Json.JsonSerializerSettings
                {
                    //TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                    //NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                    Converters = converters
                }) ;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }  
            }
        }
    }
}
