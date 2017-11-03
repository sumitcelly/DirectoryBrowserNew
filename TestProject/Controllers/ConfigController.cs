using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FileSystemAccess;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TestProject.Utils;

namespace TestProject.Controllers
{
	public class ConfigController : ApiController
	{

		public ConfigController()
		{

		}

		public string Get()
		{
			try
			{
				return System.Configuration.ConfigurationManager.AppSettings["HomeFolder"];
			}
			catch (Exception exc)
			{

			}
			return null;

		}

		public static string ConvertFromJson(string id)
		{

			System.Text.StringBuilder path = new System.Text.StringBuilder();
			List<string> fromJson = new List<string>();
			try
			{
				fromJson = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<string>>(id).ToList();
				fromJson.ForEach(str =>
				{
					path.Append(str).Append('\\');
				});
			}
			catch (Exception)
			{
				path.Append(Newtonsoft.Json.JsonConvert.DeserializeObject(id));
			}


			return path.ToString().Trim(new char['\\']);
		}

		//[Route("api/file/search/{id}")]

	}
}