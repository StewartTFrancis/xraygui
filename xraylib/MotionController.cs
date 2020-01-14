using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Zaber.Motion.Binary;

namespace xraylib
{

    public enum MovementType
    {
        Relative,
        Absolute
    }

    public class MotionController : IDisposable
    {
        Connection conn;
        Device dev;
        public MotionController()
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
                    // TODO: dispose managed state (managed objects).
                }

                try
                {
                    if (conn != null)
                        conn.Dispose();

                }
                catch (Exception ex)
                {
                    ex.Trace();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public bool OpenDevice()
        {
            try
            {
                conn = Connection.OpenSerialPort("COM1");
                dev = conn.DetectDevices()[0];
            }
            catch (Exception ex)
            {
                ex.Trace();
                return false;
            }

            return true;
        }

        public bool CloseDevice()
        {
            try
            {
                if (dev != null)
                    conn.Close();

                dev = null;
            }
            catch (Exception ex)
            {
                ex.Trace();
                return false;
            }

            return true;
        }

        private void checkState()
        {
            if (dev == null)
                throw new InvalidOperationException("Device isn't open.");
            if (disposedValue)
                throw new InvalidOperationException("We've already been disposed.");
        }

        public bool Move(double angle, MovementType movType)
        {
            checkState();
            try
            {
                if (movType == MovementType.Absolute)
                    dev.MoveAbsolute(angle, Zaber.Motion.Units.Angle_Degrees);
                else
                    dev.MoveRelative(angle, Zaber.Motion.Units.Angle_Degrees);

                // This blocks until the devices finishes movement.
                dev.WaitUntilIdle();
            }
            catch (Exception ex)
            {
                ex.Trace();
                return false;
            }

            return true;
        }

        public bool Home()
        {
            checkState();

            try
            {
                dev.Home();
            }
            catch (Exception ex)
            {
                ex.Trace();
                return false;
            }

            return true;
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~MotionController()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
