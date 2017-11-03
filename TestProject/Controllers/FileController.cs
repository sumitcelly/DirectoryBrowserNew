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
	public class FileController : ApiController
	{

		public FileController()
		{

		}
		
		public IEnumerable<FileSystemMetaData> Get(string id)
		{
			try
			{
				WindowsFolderAccess folderAccess = new WindowsFolderAccess(ServerUtils.ServerFileFolder);
				string error;
				List<FileSystemMetaData> folderList = null;

				folderList = folderAccess.GetFolderContents(ConvertFromJson(id), "*",false, out error).ToList<FileSystemMetaData>();

				if (folderList != null && folderList.Count >= 0)
					return folderList;
				else
					throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
			}
			catch(Exception exc)
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
			catch(Exception)
			{
				path.Append(Newtonsoft.Json.JsonConvert.DeserializeObject(id));
			}
			
			
			return path.ToString().Trim(new char['\\']);
		}
		
		//[Route("api/file/search/{id}")]
	
	}
}