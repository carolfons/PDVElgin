using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PDVDroidElgin.ViewHelpers
{
    internal class ImageConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Task.Run(() =>
            {
                try
                {
                    return ImageSource.FromStream(() => new MemoryStream(
                            buffer: System.Convert.FromBase64String(
                                    s: value.ToString()
                                )
                        ));
                }
                catch
                {
                    return null;
                }

            }).GetAwaiter().GetResult();
        }

       

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
