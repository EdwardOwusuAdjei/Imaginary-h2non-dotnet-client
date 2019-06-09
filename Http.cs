using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Hubtel.Images.Dotnet.Client
{
    public class HttpCaller
    {
        protected async Task<HttpResponseMessage> PostImage(byte[] imageBytes, string queryParams,string imageType)
        {
            using (var stream = new MemoryStream(imageBytes))
            {
                var response = await ImageApiFluent.Endpoint.SetQueryParams(new {operations = queryParams})
                    .AllowAnyHttpStatus()
                    .PostMultipartAsync(
                        multi => multi.AddFile("file", stream, $"image.{imageType}",$"image/{imageType}")
                    );
                return response;
            }
            
        }

        protected async Task<HttpResponseMessage> PublicUrl(string uri, string queryParams)
        {
            var response = await ImageApiFluent.Endpoint.SetQueryParams(new
                {
                    operations = queryParams,
                    url = uri
                }).AllowAnyHttpStatus()
                .GetAsync();
            return response;
        }
        protected async Task<HttpResponseMessage> PublicUrlWithEndpoint(string endpoint, string uri, string queryParams)
        {
            var urly = Url.Combine(NonFluent.Endpoint, $"{endpoint}?",queryParams);
            var response = await urly
                .SetQueryParams(new
                {
                    url = uri
                }).AllowAnyHttpStatus()
                .GetAsync();
            return response;
        }
        protected async Task<HttpResponseMessage> PostImageWithEndpoint(string endpoint,byte[] imageBytes, string queryParams,string imageType)
        {
            var urly = Url.Combine(NonFluent.Endpoint, $"{endpoint}?",queryParams);
            using (var stream = new MemoryStream(imageBytes))
            {
                var response = await urly
                    .AllowAnyHttpStatus()
                    .PostMultipartAsync(
                        multi => multi.AddFile("file", stream, $"image.{imageType}",$"image/{imageType}")
                    );
                return response;
            }
            
        }
        
    }
}