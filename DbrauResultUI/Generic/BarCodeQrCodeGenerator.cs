using QRCoder;
using Rotativa.AspNetCore.Options;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;

namespace DbrauResultUI.Generic
{
    public class BarCodeQrCodeGenerator
    {
        public static string QrCode(string qrText)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            return $"data:image/png;base64,{qrCode.GetGraphic(20)}";
        }
		public static string QrCode(string qrText, string darkColorHtmlHex)
		{
			QRCodeGenerator qrGenerator = new QRCodeGenerator();
			QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
			Base64QRCode qrCode = new Base64QRCode(qrCodeData);
			return $"data:image/png;base64,{qrCode.GetGraphic(20, darkColorHtmlHex, "#ffffff", false)}";
		}
		public static string GenerateBarcodeBase64(string text)
        {
            // Create an instance of BarcodeWriterPixelData
            var barcodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128, // You can change to other barcode formats
                Options = new EncodingOptions
                {
                    Height = 150,  // Height of the barcode
                    Width = 300,   // Width of the barcode
                    Margin = 10    // Margin around the barcode
                }
            };

            // Write the barcode and get the pixel data
            var pixelData = barcodeWriter.Write(text);

            // Create a bitmap from the raw pixel data
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppArgb))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
                try
                {
                    // Copy the pixel data into the bitmap
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                // Convert the bitmap to a base64 string
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    string base64String = Convert.ToBase64String(ms.ToArray());
                    return $"data:image/png;base64,{base64String}";
                }
            }
        }
    }
}
