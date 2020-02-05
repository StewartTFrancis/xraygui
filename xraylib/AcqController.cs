using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static xraylib.Acq;

namespace xraylib
{
    public class AcqController : IDisposable
    {
        public State state = State.CLOSED;
        
        IntPtr deviceHandle;
        GCHandle bufferHandle;
        GCHandle offsetHandle;
        GCHandle gainHandle;


        public uint[] imageBuffer = null;

        uint dwFrames = 0;
        public uint dwRows = 0;
        public uint dwColumns = 0;
        uint dwDataType = 0;
        uint dwSortFlags = 0;
        bool irqEnabled = false;
        ulong acqType = 0;
        ulong systemId = 0;
        ulong syncMode = 0;
        ulong hwAccess = 0;
        public int imageBufferSize { get; private set; }

        HandleRef hwnd;

        double[] intTime = new double[12];
        int nIntTimes = 12;

        bool bufferGood = false;
        bool configGood = false;

        uint corrListSize = 0;
        public int[] corrList = null;

        public ushort[] offsetArr = null;
        public uint[] gainArr = null;

        public delegate void ImageAcquired();
        public event ImageAcquired imageAcquired;

        private event ImageAcquired internalImageAcquired;

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

        private void FrameAcquired(IntPtr dev)
        {
            Trace.WriteLine("FrameAcquired Called.");
            Acquisition_SetReady(dev, true);
        }

        private void AcqDone(IntPtr dev)
        {
            Trace.WriteLine("AcqDone Called.");

            if (imageAcquired != null)
                imageAcquired();

            Acquisition_SetReady(dev, true);
        }

        private void InternalFrameAcquired(IntPtr dev)
        {
            Trace.WriteLine("InternalFrameAcquired Called.");
            Acquisition_SetReady(dev, true);
        }

        private void InternalAcqDone(IntPtr dev)
        {
            Trace.WriteLine("InternalAcqDone Called.");
            
            if(internalImageAcquired != null)
                internalImageAcquired();

            Acquisition_SetReady(dev, true);
        }

        public bool OpenDevice(IntPtr windowHandle)
        {
            if (windowHandle != null && windowHandle != IntPtr.Zero)
                hwnd = new HandleRef(this, windowHandle);

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

                Acquisition_SetCallbacksAndMessages(deviceHandle, hwnd, 0, 0, FrameAcquired, AcqDone);

                SetFOVMode(fov);
                SetBinningMode(binning);
                SetGainMode(gain);
                SetCameraMode(7);

                resp = GetConfiguration();

                dwFrames = 1;

                resp = SetBuffer();

                // resp = CreatePixelMap();

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

        public uint[] GetGainImage()
        {
            try
            {
                if (gainHandle != null && gainHandle.IsAllocated)
                    gainHandle.Free();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed to free offsetHandle!");
                ex.Trace();
            }

            gainArr = new uint[this.imageBufferSize];
            gainHandle = GCHandle.Alloc(offsetArr, GCHandleType.Pinned);

            var keepWaiting = true;

            internalImageAcquired = () => { Trace.WriteLine("Gain image returned");  keepWaiting = false;  };

            Acquisition_SetCallbacksAndMessages(deviceHandle, hwnd, 0, 0, InternalFrameAcquired, InternalAcqDone);
            var resp = Acquisition_Acquire_GainImage(deviceHandle, offsetHandle.AddrOfPinnedObject(), gainHandle.AddrOfPinnedObject(), dwRows, dwColumns, dwFrames);
            Trace.WriteLine("Acquisition_Acquire_GainImage: " + resp);

            if (resp == HIS_RETURN.HIS_ALL_OK)
            {
                for (var i = 0; i < 30; i++)
                {
                    if (keepWaiting)
                        Task.WaitAll(Task.Delay(100));
                    else
                        return gainArr;
                }
            }

            return gainArr;
        }

        public ushort[] GetOffsetImage()
        {
            try
            {
                if (offsetHandle != null && offsetHandle.IsAllocated)
                    offsetHandle.Free();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed to free offsetHandle!");
                ex.Trace();
            }

            offsetArr = new ushort[this.imageBufferSize];

            offsetHandle = GCHandle.Alloc(offsetArr, GCHandleType.Pinned);
            
            var keepWaiting = true;
            internalImageAcquired = () => { Trace.WriteLine("Offset image returned");  keepWaiting = false; };
            Acquisition_SetCallbacksAndMessages(deviceHandle, hwnd, 0, 0, InternalFrameAcquired, InternalAcqDone);

            var resp = Acquisition_Acquire_OffsetImage(deviceHandle, offsetHandle.AddrOfPinnedObject(), dwRows, dwColumns, dwFrames);
            Trace.WriteLine("Acquisition_Acquire_OffsetImage: " + resp);

            if (resp == HIS_RETURN.HIS_ALL_OK)
            {
                for (var i = 0; i < 30; i++)
                {
                    if (keepWaiting)
                        Task.WaitAll(Task.Delay(100));
                    else
                        return offsetArr;
                }
            }

            return offsetArr;
        }

        public int[] GetPixelCorrection()
        {
            var resp = CreatePixelMap();
            Trace.WriteLine("Called CreatePixelmap: " + resp);

            return corrList;
        }

        public HIS_RETURN SetCameraMode(uint mode)
        {
            var resp = Acquisition_SetCameraMode(deviceHandle, mode);

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
            if (!bufferGood || imageBuffer.Length != dwFrames * dwRows * dwColumns * 2)
            {
                Trace.WriteLine("Buffer bad.");
                return false;
            }

            Trace.WriteLine("Buffer good.");
            return true;
        }

        private HIS_RETURN SetBuffer()
        {
            if(state != State.OPEN)
                throw new InvalidOperationException("Can't set buffer before opening");

            HIS_RETURN resp = HIS_RETURN.HIS_ERROR_ALREADY_EXISTS;

            if (!CheckBuffer())
            {
                // For consuming app, single image buffer size is:
                imageBufferSize = (int)(dwRows * dwColumns * 2);

                try
                {
                    if (bufferHandle != null && bufferHandle.IsAllocated)
                        bufferHandle.Free();
                } catch(Exception ex)
                {
                    Trace.WriteLine("Failed to free bufferHandle!");
                    ex.Trace();
                }
                
                var buffSize = dwFrames * dwRows * dwColumns * 2;
                
                imageBuffer = new uint[buffSize];

                bufferHandle = GCHandle.Alloc(imageBuffer, GCHandleType.Pinned);
                
                resp = Acquisition_DefineDestBuffers(deviceHandle, bufferHandle.AddrOfPinnedObject(), dwFrames, dwRows, dwColumns);
                Trace.WriteLine("DefineDestBuffers Device response: " + resp.ToString());


                if (resp == HIS_RETURN.HIS_ALL_OK)
                    bufferGood = true;
            }

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

            
            corrListSize = 0;
            var resp = Acquisition_CreatePixelMap(imageBuffer, dwRows, dwColumns, corrList, ref corrListSize);

            Trace.WriteLine("First call to Acquisition_CreatePixelMap Device response: " + resp.ToString());
            Trace.WriteLine("corrListSize: " + corrListSize.ToString());

            if (resp == HIS_RETURN.HIS_ALL_OK && corrListSize > 0)
            {
                corrList = new int[corrListSize];

                resp = Acquisition_CreatePixelMap(imageBuffer, dwRows, dwColumns, corrList, ref corrListSize);
                Trace.WriteLine("Second call to Acquisition_CreatePixelMap Device response: " + resp.ToString());
            }
            

            return resp;
        }

        public HIS_RETURN AcquireImage()
        {
            try
            {
                if (state != State.OPEN)
                    throw new InvalidOperationException("Can't acquire before opening");

                if (!configGood)
                    GetConfiguration();

                if (!CheckBuffer())
                {
                    Trace.WriteLine("Buffer not good and we're in AcquireImage, calling SetBuffer");
                    Trace.WriteLine("SetBuffer Device response: " + SetBuffer());
                }
                else
                {
                    Trace.WriteLine("Buffer good in AcquireImage");
                }

                Acquisition_SetCallbacksAndMessages(deviceHandle, hwnd, 0, 0, FrameAcquired, AcqDone);

                corrList = null;

                var resp = Acquisition_Acquire_Image(deviceHandle, dwFrames, 0, HIS_SEQ.AVERAGE, offsetArr, gainArr, corrList);
                Trace.WriteLine("Acquisition_Acquire_Image Device response: " + resp.ToString());

                return resp;
            } catch (Exception ex)
            {
                Trace.WriteLine("Error in AcquireImage");
                ex.Trace();

                return HIS_RETURN.HIS_ERROR_ACQUISITION;
            }
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
                
                if (bufferHandle != null && bufferHandle.IsAllocated)
                    bufferHandle.Free();

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
