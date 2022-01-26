using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiLibrary.Models;
using UiLibrary.Resources;

namespace UiLibrary.Serializers
{
    public class FiguresJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(AbstractFigure));
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    throw new NotImplementedException();
        //}

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            string diplayNameProperty = "DisplayName";
            JObject jo = JObject.Load(reader);

            if (jo[diplayNameProperty].Value<string>() == GlobalStrings.CircleDisplayName)
            {
                return jo.ToObject<CircleModel>(serializer);
            }

            if (jo[diplayNameProperty].Value<string>() == GlobalStrings.RectangleDisplayName)
            {
                return jo.ToObject<RectangleModel>(serializer);
            }

            if (jo[diplayNameProperty].Value<string>() == GlobalStrings.TriangleDisplayName)
            {
                return jo.ToObject<TriangleModel>(serializer);
            }

            return null;
        }
    }
}
