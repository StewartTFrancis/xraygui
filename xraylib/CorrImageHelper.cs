using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace xraylib
{
    public class CorrImageHelper
    {
        public static void WriteCorrOut(string filename, CorrImageType type, Object data, uint width, uint height)
        {
            WriteCorrOut(filename, new CorrOutStructure(type, data, width, height));
        }

        public static void WriteCorrOut(string filename, CorrOutStructure corrOut)
        {
            try
            {
                using (var fs = File.OpenWrite(filename))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, corrOut);
                }
            } catch (Exception ex)
            {
                Trace.Write("Writing correction image out failed. Filename: " + filename);
                ex.Trace();
                throw;
            }
        }

        public static CorrOutStructure ReadCorrIn(string filename)
        {
            try
            {
                using (var fs = File.OpenRead(filename))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var objIn = bf.Deserialize(fs);

                    if (objIn is CorrOutStructure)
                        return (CorrOutStructure)objIn;
                    else
                        throw new InvalidDataException("File isn't a correction image.");
                }
            }
            catch (Exception ex)
            {
                Trace.Write("Reading correction image failed. Filename: " + filename);
                ex.Trace();
                throw;
            }
        }

        public static string MapCorrTypeToExt(CorrImageType type)
        {
            switch(type)
            {
                case CorrImageType.Gain:
                    return "gain";
                case CorrImageType.Offset:
                    return "offset";
                case CorrImageType.PixelCorrection:
                    return "pixcorr";
            }
            return null;
        }
    }

    public enum CorrImageType
    {
        Offset,
        Gain,
        PixelCorrection
    }

    [Serializable]
    public struct CorrOutStructure
    {
        public CorrImageType type { get; set; }
        public Object data { get; set; }
        public uint width { get; set; }
        public uint height { get; set; }

        public CorrOutStructure(CorrImageType type, Object data, uint width, uint height)
        {
            this.type = type;
            this.data = data;
            this.width = width;
            this.height = height;
        }
    }
}
