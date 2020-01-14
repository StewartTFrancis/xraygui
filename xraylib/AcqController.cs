using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace xraylib
{
    public class AcqController : IDisposable
    {
        IntPtr deviceHandle;
        public AcqController()
        {

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Free managed resources
                }

                // TODO: set large fields to null.

                Acq.Acquisition_CloseAll();

                disposedValue = true;
            }
        }

        public bool OpenDevice()
        {
            try
            {
                deviceHandle = IntPtr.Zero;
                var resp = Acq.Acquisition_Init(ref deviceHandle, Acq.HIS_BOARD.HIS_BOARD_TYPE_ELTEC_XRD_FGE_Opto, 0, true, 0, 0, 0, true, true);
                
                Trace.WriteLine("Open Device response: " + resp.ToString());

                return resp == Acq.HIS_RETURN.HIS_ALL_OK;

            } catch (Exception ex)
            { 
                ex.Trace();
            }

            return false;
        }

        public bool CloseDevice()
        {
            try
            {
                deviceHandle = IntPtr.Zero;
                var resp = Acq.Acquisition_CloseAll();

                Trace.WriteLine("Close Device response: " + resp.ToString());

                return resp == Acq.HIS_RETURN.HIS_ALL_OK;

            }
            catch (Exception ex)
            {
                ex.Trace();
            }

            return false;
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~AcqController()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion


    }
}
