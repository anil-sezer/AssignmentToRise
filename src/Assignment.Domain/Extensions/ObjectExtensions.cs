using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Assignment.Domain.Extensions
{
    public static class ObjectExtensions
    {
        public static StringContent ToStringContent(
            this object obj,
            Encoding encoding,
            string mediaType)
        {
            var bodyString = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new DefaultNamingStrategy()
                }
            });

            return new StringContent(bodyString, encoding, mediaType);
        }

        public static TObject FromStringToObject<TObject>(this string str)
        {
            return JsonConvert.DeserializeObject<TObject>(str, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new DefaultNamingStrategy()
                }
            });
        }
    }
}
