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
using System.Text;
namespace TestProject.Controllers
{
	public class SearchController : ApiController
	{

		public SearchController()
		{

		}
	

		public IEnumerable<FileSystemMetaData> Get(string id)
		{
			List<string> fromJson = new List<string>();
			string parentFolder = string.Empty;
			string searchParams = string.Empty;
			try
			{
				fromJson = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<string>>(id).ToList();
			}
			catch (Exception)
			{
				parentFolder = Newtonsoft.Json.JsonConvert.DeserializeObject(id).ToString();
			}
			if (fromJson!=null )
			{
				StringBuilder sb = new StringBuilder();
				for (int i = 1; i < fromJson.Count; i++)
					 sb.Append(fromJson[i]+"\\");
				
				parentFolder=sb.ToString().Substring(0, sb.Length - 1);
				searchParams = "*"+fromJson[0]+"*";
			}

			WindowsFolderAccess folderAccess = new WindowsFolderAccess(TestProject.Utils.ServerUtils.ServerFileFolder);

			List<FileSystemMetaData> folderList = null;
			string error;
			folderList = folderAccess.GetFolderContents(parentFolder, searchParams, true,out error).ToList();
			if (folderList != null && folderList.Count >= 0)
				return folderList;
			else
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

		}

		
		
	}
}