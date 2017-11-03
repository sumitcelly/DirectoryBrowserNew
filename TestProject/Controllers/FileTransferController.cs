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
using System.Net.Http.Headers;
using TestProject.Utils;
using System.Web;
using System.Web.Http;

namespace TestProject.Controllers
{
	public class FileTransferController : ApiController
	{

		public FileTransferController()
		{ }
	

		public HttpResponseMessage Get(string id)
		{
			try
			{
				var stream = new MemoryStream();
				// processing the stream.

				var result = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new ByteArrayContent(File.ReadAllBytes(ServerUtils.GetFilePathOnServer(id)))
				};
				result.Content.Headers.ContentDisposition =
					new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
					{
						FileName = ServerUtils.GetFileNameFromPassedArray(id)
					};
				result.Content.Headers.ContentType =
					new MediaTypeHeaderValue("application/octet-stream");

				return result;
			}
			catch (Exception exc)
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
				response.Content = new StringContent(exc.Message);
				return response;
			}

		}

		[HttpPost]
		public async Task<HttpResponseMessage>  Post()
		{			

			// Check if the request contains multipart/form-data.
			if (!Request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

			string root = HttpContext.Current.Server.MapPath("~/App_Data");
			var provider = new MultipartFormDataStreamProvider(root);

			try
			{
				// Read the form data.
				await Request.Content.ReadAsMultipartAsync(provider);

				// This illustrates how to get the file names.
				foreach (MultipartFileData file in provider.FileData)
				{
					
					Console.Write(file.Headers.ContentDisposition.FileName);
					File.Move(file.LocalFileName,root + "\\" + file.Headers.ContentDisposition.FileName.Replace("\"",string.Empty));					
					Console.Write("Server file path: " + file.LocalFileName);
				}
				return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (System.Exception e)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}	
		}
		
		
	}
}