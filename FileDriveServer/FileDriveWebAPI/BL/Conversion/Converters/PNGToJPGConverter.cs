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
    public class PNGToJPGConverter : FileConverter
    {

        public PNGToJPGConverter() : base(ENUMConverterType.PNGToJPG, "jpg") { }
        public override byte[] Convert(TreeEntity file)
        {
            Image jpgImage = null;
            byte[] jpgImageBytes = null;

            try
            {
                using (var origImageStream = new MemoryStream(file.File))
                using (var jpgImageStream = new MemoryStream())
                {
                    jpgImage = Image.FromStream(origImageStream);
                    jpgImage.Save(jpgImageStream, ImageFormat.Jpeg);
                    jpgImageBytes = jpgImageStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new ConversionFailedException("png", "jpg");
            }
            finally 
            {
                jpgImage.Dispose();
            }

            if (jpgImageBytes == null) 
            {
                throw new ConversionFailedException("png", "jpg");
            }

            return jpgImageBytes;
        }
    }
}
