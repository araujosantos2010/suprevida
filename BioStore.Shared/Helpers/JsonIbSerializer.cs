using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BioStore.Shared.Helpers
{
    public class JsonIbSerializer : ISerializer, IDeserializer
    {
        public JsonIbSerializer()
        {
            this.ContentType = "application/json";
        }

        public string ContentType { get; set; }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, SerializerSettings.JsonSerializerSettings);
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content, SerializerSettings.JsonSerializerSettings);
        }

    }
}
