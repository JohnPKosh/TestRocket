using System.Net;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using BFG.Coding.Exercise.Models;
using System;

namespace BFG.Coding.Exercise.Controllers
{
	public class MediaController : ApiController
	{
		public static string getDataFile() => HttpContext.Current.Server.MapPath("/data.json");
		public static void writeDataFile(string json) => File.WriteAllText(getDataFile(), json);
		public static Func<Media, bool> AlreadyExists(Media n) => (Media m) =>
			string.IsNullOrEmpty(n.author) ?
				(m.director == n.director && m.title == n.title) :
				(m.author == n.author && m.title == n.title);

		public MediaStore LoadJson(string f)
		{
			using (StreamReader r = new StreamReader(f))
			{
				string json = r.ReadToEnd();
				return (MediaStore)JsonConvert.DeserializeObject<MediaStore>(json);
			}
		}

		[HttpPost]
		public HttpResponseMessage Index([FromBody] MediaRequest req)
		{
			HttpResponseMessage res;

			var data = LoadJson(getDataFile());
			var newMedia = new Models.Media();
				newMedia.title = req.media.title;
			
			if (req.collection == "books")
			{
				newMedia.author = req.media.author;
			}
			else
			{
				newMedia.director = req.media.director;
			}

			if (data[req.collection].Where(AlreadyExists(newMedia)).Count() > 0)
			{

				res = new HttpResponseMessage(HttpStatusCode.Conflict);
			}
			else
			{
				data[req.collection].Add(newMedia);				  
				writeDataFile(JsonConvert.SerializeObject(data));	  
				res = new HttpResponseMessage(HttpStatusCode.Created);
			}

			return res;
		}

		[HttpGet]
		public HttpResponseMessage Index()
		{
			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = new StreamContent(new FileStream(getDataFile(), FileMode.Open, FileAccess.Read));
			response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
			response.Content.Headers.ContentDisposition.FileName = "data.json";
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return response;
		}

	}
}
