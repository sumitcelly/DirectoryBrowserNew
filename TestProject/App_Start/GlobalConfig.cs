using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace TestProject
{
	public static class GlobalConfig
	{
		public static void CustomizeConfig(HttpConfiguration config)
		{
			
			config.Formatters.Remove(config.Formatters.XmlFormatter);
			
		}
	}
}