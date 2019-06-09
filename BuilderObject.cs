// ReSharper disable InconsistentNaming
namespace Hubtel.Images.Dotnet.Client
{
    internal class BuilderObject
    {
        public string operation { get; set; }

        //  public bool ignore_failure = true;
        public dynamic @params { get; set; }
    }
    //Don't change casing
    public enum ConvertTo
    {
        png,
        jpeg,
        webp,
        auto
    }
}