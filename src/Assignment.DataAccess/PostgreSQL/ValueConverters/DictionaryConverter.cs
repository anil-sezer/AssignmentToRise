using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Assignment.DataAccess.PostgreSQL.ValueConverters;

public class DictionaryConverter : ValueConverter<Dictionary<string, int>, string>
{
    public DictionaryConverter() : base(
        dictionary => JsonConvert.SerializeObject(dictionary),
        json => JsonConvert.DeserializeObject<Dictionary<string, int>>(json))
    {
    }
}
