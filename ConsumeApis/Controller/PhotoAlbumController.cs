using ConsumeApis.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumeApis.Controller
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class PhotoAlbumController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;
        public PhotoAlbumController(IHttpClientFactory httpClientFactory)
        {
              this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
       [Route("PhotoAlbum")]
        public async Task <ActionResult<List<Photo>>> MapPhotoAlbum()
        {

            string url1 = "https://jsonplaceholder.typicode.com/photos";
            var t = ConsumeApi(url1);
            string tt = t.Result;
            string url2 = "https://jsonplaceholder.typicode.com/albums";
            var s = ConsumeApi(url2);
            List<Photo> mapList =new List<Photo>();
          List<Photo> p = JsonSerializer.Deserialize<List<Photo>>(tt.ToString());
           List<Album> A = JsonSerializer.Deserialize<List<Album>>(s.Result.ToString());
            foreach(var f in p)
            {
                foreach(var h in A)
                {
                    if (f.id == h.id)
                    {
                        mapList.Add(f); 
                    }
                }
            }

            return Ok(mapList);
        }
        [HttpGet]
        [Route("PhotoDetails")]
        public async Task<ActionResult> GetPhotoDetails()
        {
            string url = "https://jsonplaceholder.typicode.com/photos";
            var t = ConsumeApi(url);
            return Ok(t.Result.ToString());
        }
        [HttpGet]
        [Route("CsvButton")]
        public async Task CsvButton(string records) //
        {
            var workbook = new GrapeCity.Documents.Excel.Workbook();

        }
        private async Task<string> ConsumeApi(string url)
        {
            HttpClient client = httpClientFactory.CreateClient("photoClient");
            HttpResponseMessage httpResponse = await client.GetAsync(url);
            var f = httpResponse.Content.ReadAsStringAsync();
            return f.Result;
        }
    }
}
