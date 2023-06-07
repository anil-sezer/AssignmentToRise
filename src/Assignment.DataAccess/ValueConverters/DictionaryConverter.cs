using Newtonsoft.Json;

namespace Assignment.DataAccess.ValueConverters;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class DictionaryConverter : ValueConverter<Dictionary<string, int>, string>
{
    public DictionaryConverter() : base(
        dictionary => JsonConvert.SerializeObject(dictionary),
        json => JsonConvert.DeserializeObject<Dictionary<string, int>>(json))
    {
    }
}
