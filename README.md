# Imaginary.Images.Dotnet.Client

A fluent client which makes it a breeze to talk to imaginary without knowing the details.
This class library is for those who just want to perform some image transformation without stress. It returns an HttpResponseMessage depending on the status code you will know if your image operations were successful. 200 Ok means was transformed image was returned in content. Status 400 means your given values couldn't work on the given image hence the transformations did not work.


## Usage Example

```c#


internal class Program
{
        private static void Main(string[] args)
        {
            var api = new ImageApiFluent(
                "https://raw.githubusercontent.com/h2non/imaginary/master/testdata/smart-crop.jpg");

            api.Crop(400,400).Extract(20,20).Blur(3).WaterMark("Hello", 200, Color.White).Convert(ConvertTo.png)
                .Flip().Run();
           //or basically go step wise when done you call run on the final operation
            api.Convert(ConvertTo.jpeg);
            api.Enlarge(200, 200);
            api.Flip().Flop().Blur(3);
            api.Rotate(90).Run();
            //When run is called here all previous operations are added up.
            //So a Convert,Enlarge,Flip,Flop,Blur,Rotate in this order.

            //Could also be used this way instead of url you can use a stream
           var api = new ImageApiFluent(someStream);
           api.Rotate(90).Run();
            Console.ReadKey();
        }
}
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.



## License
Copyright Edward.