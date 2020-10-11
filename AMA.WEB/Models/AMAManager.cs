using AMA.BusinessLayer.AbstractFactory;
using AMA.DataLayer.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AMA.WEB.Models
{
    public class AMAManager
    {
        public readonly AMAClient Client;
        public AMAManager()
        {
            Client = new AMAClient();

        }

        public static List<CategoryModel> FillRecursive(List<Category> flatObjects, int? parentId = null)
        {
            return flatObjects.Where(x => x.ParentId.Equals(parentId)).Select(item => new CategoryModel
            {
                Name = item.Name,
                Id = item.Id,
                Description = item.Description,
                SiteName = item.SiteName,
                ParentCategory = item,
                Children = FillRecursive(flatObjects, item.Id)
            }).ToList();
        }

        public static async Task<HttpResponseMessage> GetClientResponse(string APIurl, object requestModel)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                var jsonRequest = JsonConvert.SerializeObject(requestModel);
                var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIurl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(APIurl),
                        Content = stringContent,
                    };

                    response = client.SendAsync(request).Result;
                    response.Headers.TransferEncodingChunked = false;

                }

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static HttpResponseMessage GetClientResponse(string APIurl)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIurl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    Task.Run(() =>
                    {
                        response = client.GetAsync(APIurl).Result;

                    }).Wait();


                }

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static HttpResponseMessage GetSearchClientResponse(string APIurl, object parameters)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIurl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    Task.Run(() =>
                    {
                        var myContent = JsonConvert.SerializeObject(parameters);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        response = client.PostAsync(APIurl, byteContent).Result;

                    }).Wait();


                }

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}