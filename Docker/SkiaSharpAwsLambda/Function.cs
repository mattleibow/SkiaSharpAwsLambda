using System.Reflection;
using Amazon.Lambda.Core;
using SkiaSharp;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SkiaSharpAwsLambda;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public string FunctionHandler(ILambdaContext context)
    {
        var info = new SKImageInfo(256, 256);
        using var surface = SKSurface.Create(info);
        var canvas = surface.Canvas;

        canvas.Clear(SKColors.Red);
        canvas.DrawRect(50, 50, 200, 200, new SKPaint
        {
            Color = SKColors.Blue,
        });

        using var snap = surface.Snapshot();
        using var image = snap.Encode(SKEncodedImageFormat.Png, 100);

        var data = image.ToArray();
        return Convert.ToBase64String(data);
    }
}
