using FileDriveWebAPI.Enums;
using FileDriveWebAPI.Models;
using FileDriveWebAPI.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.BL.Conversion.Converters
{
    public class JPGToPNGConverter : FileConverter
    {
        public JPGToPNGConverter() : base(ENUMConverterType.JPGToPNG, "png") { }

        public override byte[] Convert(TreeEntity file)
        {
            Image pngImage = null;
            byte[] pngImageBytes = null;

            try
            {
                using (var origImageStream = new MemoryStream(file.File))
                using (var jpgImageStream = new MemoryStream())
                {
                    pngImage = Image.FromStream(origImageStream);
                    pngImage.Save(jpgImageStream, ImageFormat.Jpeg);
                    pngImageBytes = jpgImageStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new ConversionFailedException("jpg", "png");
            }
            finally
            {
                pngImage.Dispose();
            }

            if (pngImageBytes == null)
            {
                throw new ConversionFailedException("jpg", "png");
            }

            return pngImageBytes;
        }
    }
}
