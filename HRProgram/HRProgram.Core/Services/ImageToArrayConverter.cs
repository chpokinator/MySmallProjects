using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.Core.Services
{
    static public class ImageToArrayConverter
    {
        public static byte[] ImgToByte(string path)
        {
            Bitmap img = new Bitmap(path, true);
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
