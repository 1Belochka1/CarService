using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CarService.Api.Helper.Json;

public static class JsonSerializerHelp
{
	public static string Serialize(object value)
		=> JsonConvert.SerializeObject(value, new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		});
}