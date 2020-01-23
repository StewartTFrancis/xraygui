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

        bool bufferGood = false;
        bool configGood = false;

        uint corrListSize = 0;
        uint[] corrList = null;

        public enum State
        {
            CLOSED,
            OPEN,
            DISPOSED
        }

        XRD4343_FOV fov = XRD4343_FOV.f432x432mm2;
        XRD4343_Binning binning = XRD4343_Binning.b1x1;
        XRD4343_Gain gain = XRD4343_Gain.eADU_114_default;

        public AcqController()
        {}

        public bool SetImageCount(int imgCount)
        {
            if (state != State.OPEN)
                return false;

            if (imgCount != dwFrames)
                dwFrames = (uint)imgCount;

            return SetBuffer() == HIS_RETURN.HIS_ALL_OK;
        }

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

                state = State.OPEN;

                SetFOVMode(fov);
                SetBinningMode(binning);
                SetGainMode(gain);

                resp = GetConfiguration();


                dwFrames = 1;

                resp = SetBuffer();

                //resp = CreatePixelMap();

                return resp == HIS_RETURN.HIS_ALL_OK;

            } catch (Exception ex)
            { 
                ex.Trace();
            }

            return false;
        }

        private HIS_RETURN GetConfiguration()
        {
            // Docs say we can just ask the device for this info.
            var resp = Acquisition_GetConfiguration(deviceHandle, ref dwFrames, ref dwRows, ref dwColumns, ref dwDataType, ref dwSortFlags, ref irqEnabled, ref acqType, ref systemId, ref syncMode, ref hwAccess);
            Trace.WriteLine("GetConfiguration Device response: " + resp.ToString());

            if(resp == HIS_RETURN.HIS_ALL_OK)
                configGood = true;

            return resp;
        }

        public HIS_RETURN SetBinningMode(XRD4343_Binning mode)
        {
            configGood = false;
            binning = mode;

            if (state != State.OPEN)
                return HIS_RETURN.HIS_ERROR_NOT_INITIALIZED;


            var resp = Acquisition_SetCameraBinningMode(deviceHandle, mode);
            Trace.WriteLine("SetCameraBinningMode Device response: " + resp.ToString());

            return resp;
        }

        public HIS_RETURN SetFOVMode(XRD4343_FOV mode)
        {
            configGood = false;
            fov = mode;

            if (state != State.OPEN)
                return HIS_RETURN.HIS_ERROR_NOT_INITIALIZED;


            var resp = Acquisition_SetCameraFOVMode(deviceHandle, mode);
            Trace.WriteLine("SetCameraFOVMode Device response: " + resp.ToString());

            return resp;
        }

        public HIS_RETURN SetGainMode(XRD4343_Gain mode)
        {
            gain = mode;
            if (state != State.OPEN)
                return HIS_RETURN.HIS_ERROR_NOT_INITIALIZED;


            var resp = Acquisition_SetCameraGain(deviceHandle, mode);
            Trace.WriteLine("SetCameraGain Device response: " + resp.ToString());

            return resp;
        }

        public void SetFrameCount(uint frameCount)
        {
            bufferGood = false;
            dwFrames = frameCount;
        }

        private bool CheckBuffer()
        {
            var buffSize = dwFrames * dwRows * dwColumns * 2;
            if (!bufferGood || imageBuffer.Length != dwFrames * dwRows * dwColumns * 2)
                return false;

            return true;
        }

        private HIS_RETURN SetBuffer()
        {
            if(state != State.OPEN)
                throw new InvalidOperationException("Can't set buffer before opening");

            if (!CheckBuffer())
            {
                var buffSize = dwFrames * dwRows * dwColumns * 2;
                imageBuffer = new ushort[buffSize];
                bufferGood = true;
            }

            var resp = Acquisition_DefineDestBuffers(deviceHandle, ref imageBuffer, dwFrames, dwRows, dwColumns);

            return resp;
        }

        public HIS_RETURN CreatePixelMap()
        {
            if (!configGood)
                GetConfiguration();

            if (!CheckBuffer())
            {
                Trace.WriteLine("Buffer not good and we're in CreatePixelMap, calling SetBuffer");
                Trace.WriteLine("SetBuffer Device response: " + SetBuffer());
            }

            corrList = null;
            corrListSize = 0;
            var resp = Acquisition_CreatePixelMap(ref imageBuffer, dwRows, dwColumns, ref corrList, ref corrListSize);

            Trace.WriteLine("First call to Acquisition_CreatePixelMap Device response: " + resp.ToString());

            if (resp == HIS_RETURN.HIS_ALL_OK && corrListSize > 0)
            {
                corrList = new uint[corrListSize];

                resp = Acquisition_CreatePixelMap(ref imageBuffer, dwRows, dwColumns, ref corrList, ref corrListSize);
                Trace.WriteLine("Second call to Acquisition_CreatePixelMap Device response: " + resp.ToString());
            }
            

            return resp;
        }

        public HIS_RETURN AcquireImage()
        {
            if (state != State.OPEN)
                throw new InvalidOperationException("Can't acquire before opening");

            if (!configGood)
                GetConfiguration();

            ushort[] offsetArr = new ushort[dwColumns * dwRows];
            uint[] gainArr = new uint[dwColumns * dwRows];
            
            CreatePixelMap();

            if (!CheckBuffer())
            {
                Trace.WriteLine("Buffer not good and we're in AcquireImage, calling SetBuffer");
                Trace.WriteLine("SetBuffer Device response: " + SetBuffer());
            }

            var resp = Acquisition_Acquire_Image(deviceHandle, dwFrames, 0, HIS_SEQ.AVERAGE, ref offsetArr, ref gainArr, ref corrList);
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
