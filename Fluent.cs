using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hubtel.Images.Dotnet.Client
{
    public class ImageApiFluent : HttpCaller, IFluentChainedMethods
    {
        //pipeline accepts tens
        private readonly List<BuilderObject> _dynamicBuilderObjects = new List<BuilderObject>(10);
        private readonly byte[] _imageBytes;
        private readonly string _imageType;
        private readonly string _uri;

        /// <summary>
        ///     Url Containing image to call
        /// </summary>
        /// <param name="uri"></param>
        public ImageApiFluent(string uri)
        {
            _uri = uri;
        }

        public ImageApiFluent(byte[] imageBytes, string imageType)
        {
            _imageBytes = imageBytes;
            _imageType = imageType;
        }

        public static string Endpoint { get; set; } = "http://127.0.0.1:9000/pipeline";

        public IFluentChainedMethods Crop(int width, int height)
        {
            ShapedOnes("crop", width, height);
            return this;
        }

        public IFluentChainedMethods Resize(int width, int height)
        {
            ShapedOnes("resize", width, height);
            return this;
        }

        public IFluentChainedMethods Enlarge(int width, int height)
        {
            ShapedOnes("enlarge", width, height);
            return this;
        }

        public IFluentChainedMethods Extract(int top, int areaWidth)
        {
            var builderObject = new BuilderObject();
            dynamic expandoObject = new ExpandoObject();
            builderObject.operation = "extract";
            expandoObject.top = top;
            expandoObject.areawidth = areaWidth;
            expandoObject.areaheight = areaWidth;
            builderObject.@params = expandoObject;
            _dynamicBuilderObjects.Add(builderObject);
            return this;
        }

        public IFluentChainedMethods Rotate(int multipleOf90LessThan361)
        {
            var builderObject = new BuilderObject();
            dynamic expandoObject = new ExpandoObject();
            builderObject.operation = "rotate";
            expandoObject.rotate = multipleOf90LessThan361 % 90 == 0 ? multipleOf90LessThan361 : 90;
            builderObject.@params = expandoObject;
            _dynamicBuilderObjects.Add(builderObject);
            return this;
        }

        public IFluentChainedMethods ThumbNail(int width, int height)
        {
            ShapedOnes("thumbnail", width, height);
            return this;
        }

        public IFluentChainedMethods Zoom(int factor)
        {
            var builderObject = new BuilderObject();
            dynamic expandoObject = new ExpandoObject();
            builderObject.operation = "zoom";
            expandoObject.factor = factor;
            builderObject.@params = expandoObject;
            _dynamicBuilderObjects.Add(builderObject);
            return this;
        }

        public IFluentChainedMethods Convert(ConvertTo type)
        {
            var builderObject = new BuilderObject();
            dynamic expandoObject = new ExpandoObject();
            builderObject.operation = "convert";
            if (type is ConvertTo.png)
                expandoObject.type = nameof(ConvertTo.png);
            else if (type is ConvertTo.jpeg)
                expandoObject.type = nameof(ConvertTo.jpeg);
            else if (type is ConvertTo.auto)
                expandoObject.type = nameof(ConvertTo.auto);
            else if (type is ConvertTo.webp) expandoObject.type = nameof(ConvertTo.webp);

            builderObject.@params = expandoObject;
            _dynamicBuilderObjects.Add(builderObject);
            return this;
        }

        public IFluentChainedMethods WaterMark(string text, int textWidth, Color color, string font = "sans bold 12",
            double opacity = 0.5)
        {
            var builderObject = new BuilderObject();
            dynamic expandoObject = new ExpandoObject();
            builderObject.operation = "watermark";
            expandoObject.text = text;
            expandoObject.textwidth = textWidth;
            expandoObject.font = font;
            expandoObject.opacity = opacity;
            expandoObject.color = $"{color.R.ToString()},{color.G.ToString()},{color.B.ToString()}";
            builderObject.@params = expandoObject;
            _dynamicBuilderObjects.Add(builderObject);
            return this;
        }

        public IFluentChainedMethods Flip()
        {
            var builderObject = new BuilderObject
            {
                operation = "flip"
            };
            _dynamicBuilderObjects.Add(builderObject);
            return this;
        }

        public IFluentChainedMethods Flop()
        {
            var builderObject = new BuilderObject {operation = "flop"};
            _dynamicBuilderObjects.Add(builderObject);
            return this;
        }

        public IFluentChainedMethods SmartCrop(int width, int height)
        {
            ShapedOnes("smartcrop", width, height);
            return this;
        }

        public IFluentChainedMethods Blur(double sigma)
        {
            var builderObject = new BuilderObject();
            dynamic expandoObject = new ExpandoObject();
            builderObject.operation = "blur";
            expandoObject.sigma = sigma;
            builderObject.@params = expandoObject;
            _dynamicBuilderObjects.Add(builderObject);
            return this;
        }


        public async Task<HttpResponseMessage> Run()
        {
            if (_dynamicBuilderObjects.Count == 0) throw new Exception("No image operation chosen");

            var json = JsonConvert.SerializeObject(_dynamicBuilderObjects);
            Console.WriteLine(json);
            if (_imageBytes is null)
            {
                var message = await PublicUrl(_uri, json);
                return message;
            }

            var responseMessage = await PostImage(_imageBytes, json, _imageType);
            return responseMessage;
        }

        private void ShapedOnes(string name, int width, int height)
        {
            var builderObject = new BuilderObject();
            dynamic expandoObject = new ExpandoObject();
            builderObject.operation = name;
            expandoObject.width = width;
            expandoObject.height = height;
            builderObject.@params = expandoObject;
            _dynamicBuilderObjects.Add(builderObject);
        }
    }
}