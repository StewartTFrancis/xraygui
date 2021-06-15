using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xraylib
{
    public static class Helpers
    {
        static Helpers()
        {
            // Need to touch the protobuf lib so VS actually brings it in
            Google.Protobuf.JsonParser parser = new Google.Protobuf.JsonParser(Google.Protobuf.JsonParser.Settings.Default);
        }

        public static List<UIMap<XRD4343_Gain>> gainMap = new List<UIMap<XRD4343_Gain>> {
            new UIMap<XRD4343_Gain>("e-/ADU: 29", XRD4343_Gain.eADU_29),
            new UIMap<XRD4343_Gain>("e-/ADU: 57", XRD4343_Gain.eADU_57),
            new UIMap<XRD4343_Gain>("e-/ADU: 114", XRD4343_Gain.eADU_114_default),
            new UIMap<XRD4343_Gain>("e-/ADU: 229", XRD4343_Gain.eADU_229),
            new UIMap<XRD4343_Gain>("e-/ADU: 457", XRD4343_Gain.eADU_457),
            new UIMap<XRD4343_Gain>("e-/ADU: 686", XRD4343_Gain.eADU_686),
            new UIMap<XRD4343_Gain>("e-/ADU: 914", XRD4343_Gain.eADU_914)
            };

        public static List<UIMap<XRD4343_Binning>> binningMap = new List<UIMap<XRD4343_Binning>> {
             new UIMap<XRD4343_Binning>("1x1", XRD4343_Binning.b1x1),
             new UIMap<XRD4343_Binning>("2x2", XRD4343_Binning.b2x2),
             new UIMap<XRD4343_Binning>("3x3", XRD4343_Binning.b3x3)
        };

        public static List<UIMap<XRD4343_FOV>> fovMap = new List<UIMap<XRD4343_FOV>> {
             new UIMap<XRD4343_FOV>("432 x 432 mm2", XRD4343_FOV.f432x432mm2),
             new UIMap<XRD4343_FOV>("288 x 288 mm2", XRD4343_FOV.f288x288mm2),
             new UIMap<XRD4343_FOV>("216 x 216 mm2", XRD4343_FOV.f216x216mm2),
             new UIMap<XRD4343_FOV>("432 x 216 mm2", XRD4343_FOV.f432x216mm2),
             new UIMap<XRD4343_FOV>("432 x 72 mm2", XRD4343_FOV.f432x72mm2),
        };

        public static List<UIMap<MovementType>> movementMap = new List<UIMap<MovementType>> {
             new UIMap<MovementType>("Absolute", MovementType.Absolute),
             new UIMap<MovementType>("Relative", MovementType.Relative),
        };

        public static void Trace(this Exception ex)
        {
            System.Diagnostics.Trace.WriteLine("\nMessage ---\n{0}", ex.Message);
            System.Diagnostics.Trace.WriteLine(
                "\nHelpLink ---\n{0}", ex.HelpLink);
            System.Diagnostics.Trace.WriteLine("\nSource ---\n{0}", ex.Source);
            System.Diagnostics.Trace.WriteLine(
                "\nStackTrace ---\n{0}", ex.StackTrace);
            ex.InnerException?.Trace();
            
        }

        public static byte[] getBytesFromUshort(ushort[] data)
        {
            var start = DateTime.Now;

            System.Diagnostics.Trace.WriteLine("Starting convert ushort to bytes");
            var bytes = data.SelectMany((val) => BitConverter.GetBytes(val)).ToArray();
            System.Diagnostics.Trace.WriteLine("Done convert ushort to bytes took: " + (DateTime.Now - start).Milliseconds);
            return bytes;
        }

    }

    public class UIMap<T>
    {
        public string Display { get; }
        public T Value { get; }

        public UIMap(string display, T value)
        {
            Display = display;
            Value = value;
        }

    }

    


}
