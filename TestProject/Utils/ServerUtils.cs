using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProject.Utils
{
	public static class ServerUtils	
	{

		public static string ServerFileFolder
		{
			get { return System.Configuration.ConfigurationManager.AppSettings["HomeFolder"]; }
		}

		public static string GetFileNameFromPassedArray(string jsArray)
		{
			List<string> fromJson = new List<string>();
			try
			{
				fromJson = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<string>>(jsArray).ToList();
			}
			catch (Exception)
			{
				fromJson.Add(jsArray);
			}
			return fromJson[fromJson.Count - 1];

		}
		public static string GetFilePathOnServer(string jsArray)
		{
			return "C:\\" + ConvertFromJson(jsArray);
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


			return path.ToString().Substring(0, path.Length - 1);
		}
	}
}