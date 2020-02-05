using BitMiracle.LibTiff.Classic;
using System;
using System.Diagnostics;

namespace xraylib
{
    public class TiffLibWrapper
    {
        public static bool SaveImage(byte[] bytes, string filename, int width, int height)
        {
            try
            {
                using (var tiff = Tiff.Open(filename, "w"))
                {
                    tiff.SetField(TiffTag.IMAGEWIDTH, width);
                    tiff.SetField(TiffTag.IMAGELENGTH, height);
                    tiff.SetField(TiffTag.BITSPERSAMPLE, 16);
                    tiff.SetField(TiffTag.SAMPLESPERPIXEL, 1);
                    tiff.SetField(TiffTag.ROWSPERSTRIP, 1);
                    tiff.SetField(TiffTag.ORIENTATION, Orientation.TOPLEFT);
                    tiff.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISBLACK);
                    tiff.SetField(TiffTag.COMPRESSION, Compression.NONE);
                    tiff.SetField(TiffTag.FILLORDER, FillOrder.MSB2LSB);

                    Trace.WriteLine("Writing tiff");
                    for (var i = 0; i < height; i++)
                    {
                        tiff.WriteScanline(bytes, (i * width) * 2, 0, 0);
                    }

                    tiff.Flush();
                }
            } catch (Exception ex)
            {
                Trace.WriteLine("Error in saving tiff");
                ex.Trace();
            }
            return false;
        }

        public static byte[] ReadImageBytes(string filename)
        {
            byte[] imageBuffer;

            using (var tiff = Tiff.Open(filename, "r"))
            {
                var imgSizeInBytes = 0;
                try
                {
                    var scanlineSize = tiff.ScanlineSize();
                    var len = tiff.GetField(TiffTag.IMAGELENGTH)[0].ToInt();

                    imgSizeInBytes = scanlineSize * len;

                    imageBuffer = new byte[imgSizeInBytes];


                    for (var i = 0; i < len; i++)
                        tiff.ReadScanline(imageBuffer, i * scanlineSize, i, 0);

                    return imageBuffer;
                }
                catch (Exception ex)
                {
                    ex.Trace();
                    Trace.WriteLine("Failed when trying open tiff");

                    throw;
                }
            }
        }
    }
}
