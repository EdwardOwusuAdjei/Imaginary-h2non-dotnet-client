using System.Net.Http;
using System.Threading.Tasks;

namespace Hubtel.Images.Dotnet.Client
{
    public class NonFluent: HttpCaller, INonFluentInterface
    {
        private readonly byte[] _imageBytes;
        private readonly string _imageType;
        private readonly string _uri;
        public static string Endpoint { get; set; } = "http://127.0.0.1:9000";

        /// <summary>
        ///     Url Containing image to call
        /// </summary>
        /// <param name="uri"></param>
        public NonFluent(string uri)
        {
            _uri = uri;
        }

        public NonFluent(byte[] imageBytes, string imageType)
        {
            _imageBytes = imageBytes;
            _imageType = imageType;
        }

        public async Task<HttpResponseMessage> Fit(int width, int height)
        {
            var query = $"width={width.ToString()}&height={height.ToString()}";
            if (_imageBytes is null)
            {
                var message = await PublicUrlWithEndpoint("fit",_uri,query);
                return message;
            }

            var responseMessage = await PostImageWithEndpoint("fit",_imageBytes, query, _imageType);
            return responseMessage;
        }
    }
}