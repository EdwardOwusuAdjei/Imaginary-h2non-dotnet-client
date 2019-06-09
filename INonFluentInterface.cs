using System.Net.Http;
using System.Threading.Tasks;

namespace Hubtel.Images.Dotnet.Client
{
    public interface INonFluentInterface
    {
        Task<HttpResponseMessage> Fit(int width,int height);
    }
}