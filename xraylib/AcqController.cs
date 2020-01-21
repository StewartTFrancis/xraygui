using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static xraylib.Acq;

namespace xraylib
{
    public class AcqController : IDisposable
    {
        public State state = State.CLOSED;
        
        IntPtr deviceHandle;

        ushort[] imageBuffer = null;

        uint dwFrames = 0;
        uint dwRows = 0;
        uint dwColumns = 0;
        uint dwDataType = 0;
        uint dwSortFlags = 0;
        bool irqEnabled = false;
        ulong acqType = 0;
        ulong systemId = 0;
        ulong syncMode = 0;
        ulong hwAccess = 0;

        double[] intTime = new double[12];
        int nIntTimes = 12;

        public enum State
        {
            CLOSED,
            OPEN,
            DISPOSED
        }

        public AcqController()
        {}



        public bool OpenDevice()
        {
            if (state != State.CLOSED)
                throw new InvalidOperationException("Can't open if we aren't closed.");
            
            try
            {
                deviceHandle = IntPtr.Zero;
                var resp = Acquisition_Init(ref deviceHandle, HIS_BOARD_TYPE.ELTEC_XRD_FGE_Opto, 0, true, 0, 0, 0, true, true);
                
                Trace.WriteLine("Open Device response: " + resp.ToString());

                if (resp != HIS_RETURN.HIS_ALL_OK)
                {
                    return false;
                }

                // Docs say we can just ask the device for this info.
                Acquisition_GetConfiguration(ref deviceHandle, ref dwFrames, ref dwRows, ref dwColumns, ref dwDataType, ref dwSortFlags, ref irqEnabled, ref acqType, ref systemId, ref syncMode, ref hwAccess);
               Trace.WriteLine("GetConfiguration Device response: " + resp.ToString());

                // Get valid integration times
                Acquisition_GetIntTimes(ref deviceHandle, ref intTime, ref nIntTimes);
                Trace.WriteLine("GetIntTimes Device response: " + resp.ToString());

                foreach(var time in intTime)
                    Trace.WriteLine("intTime: " + time);


                if (resp == HIS_RETURN.HIS_ALL_OK)
                    state = State.OPEN;

                return resp == HIS_RETURN.HIS_ALL_OK;

            } catch (Exception ex)
            { 
                ex.Trace();
            }

            return false;
        }

        public HIS_RETURN SetBinningMode(DETECTOR_BINNING mode)
        {
            if(state != State.OPEN)
                throw new InvalidOperationException("Can't set binning mode before opening");


            var resp = Acquisition_SetCameraBinningMode(ref deviceHandle, mode);
            Trace.WriteLine("SetCameraBinningMode Device response: " + resp.ToString());

            return resp;
        }

        public HIS_RETURN AcquireImage()
        {
            ushort[] nullshort = null;
            uint[] nulluint = null;

            var resp = Acquisition_Acquire_Image(ref deviceHandle, 1, 0, HIS_SEQ.AVERAGE, ref nullshort, ref nulluint, ref nulluint);
            Trace.WriteLine("Acquisition_Acquire_Image Device response: " + resp.ToString());

            return resp;
        }

        public bool CloseDevice()
        {
            if (state == State.DISPOSED)
                throw new InvalidOperationException("Can't close once we're disposed.");

            try
            {
                deviceHandle = IntPtr.Zero;
                var resp = Acquisition_CloseAll();

                Trace.WriteLine("Close Device response: " + resp.ToString());

                return resp == HIS_RETURN.HIS_ALL_OK;

            }
            catch (Exception ex)
            {
                ex.Trace();
            }

            return false;
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
                    state = State.DISPOSED;
                }

                // TODO: set large fields to null.

                Acquisition_CloseAll();

                disposedValue = true;
            }
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
