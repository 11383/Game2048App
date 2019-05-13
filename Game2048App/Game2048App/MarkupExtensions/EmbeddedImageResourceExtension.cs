using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace Game2048App.MarkupExtensions
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Source")]
    public class ImageResource : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source);

            return imageSource;
        }
    }
}