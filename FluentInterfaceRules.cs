using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hubtel.Images.Dotnet.Client
{
    public interface IFluentChainedMethods
    {
        IFluentChainedMethods Crop(int width, int height);
        IFluentChainedMethods Resize(int width, int height);
        IFluentChainedMethods Enlarge(int width, int height);
        IFluentChainedMethods Extract(int top, int areaWidth);
        IFluentChainedMethods Rotate(int multipleOf90LessThan361);
        IFluentChainedMethods ThumbNail(int width, int height);
        IFluentChainedMethods Zoom(int factor);
        IFluentChainedMethods Convert(ConvertTo type);

        IFluentChainedMethods WaterMark(string text, int textWidth, Color color, string font = "sans bold 12",
            double opacity = 0.2);

        IFluentChainedMethods Flip();
        IFluentChainedMethods Flop();
        IFluentChainedMethods SmartCrop(int width, int height);
        IFluentChainedMethods Blur(double sigma);
        Task<HttpResponseMessage> Run();
    }
}