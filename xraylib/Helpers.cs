using System;
using System.Collections.Generic;
using System.Text;

namespace xraylib
{
    public static class Helpers
    {
        public static void Trace(this Exception ex)
        {
            System.Diagnostics.Trace.WriteLine("\nMessage ---\n{0}", ex.Message);
            System.Diagnostics.Trace.WriteLine(
                "\nHelpLink ---\n{0}", ex.HelpLink);
            System.Diagnostics.Trace.WriteLine("\nSource ---\n{0}", ex.Source);
            System.Diagnostics.Trace.WriteLine(
                "\nStackTrace ---\n{0}", ex.StackTrace);
        }
    }
}
