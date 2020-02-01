using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xraylib;

namespace xraygui
{
    public partial class Form1 : Form
    {
		AcqController acquire = new AcqController();
		MotionController motion = new MotionController();

		
		public Form1()
        {
            InitializeComponent();

			cbGain.DataSource = Helpers.gainMap;
			cbGain.DisplayMember = "Display";
			cbGain.ValueMember = "Value";
			cbGain.SelectedIndex = 2;

			cbBinning.DataSource = Helpers.binningMap;
			cbBinning.DisplayMember = "Display";
			cbBinning.ValueMember = "Value";
			cbBinning.SelectedIndex = 0;

			cbFOV.DataSource = Helpers.fovMap;
			cbFOV.DisplayMember = "Display";
			cbFOV.ValueMember = "Value";
			cbFOV.SelectedIndex = 0;


			cbMoveType.SelectedIndex = 0;
        }

		

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			acquire.Dispose();
			motion.Dispose();
		}

		private void btnHome_Click(object sender, EventArgs e)
		{
			if (!motion.Home())
				MessageBox.Show("Homing failed, check the log");
		}

		private void btnMove_Click(object sender, EventArgs e)
		{
			motion.Move((double)numMoveTo.Value, (MovementType)cbMoveType.SelectedValue);
		}

		private void closeDeviceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			motion.CloseDevice();
			acquire.CloseDevice();

			UpdateButtons();

			this.lblStatus.Text = "Current Status: " + acquire.state;
		}

		private void openDeviceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var opened = motion.OpenDevice();
			acquire.OpenDevice();

			lblCurrAngle.Text = "Current Angle: " + motion.CurrentAngle;
			tblblCurrAngle.Text = lblCurrAngle.Text;

			this.lblStatus.Text = "Current Status: " + acquire.state;

			UpdateButtons();
		}

		private void tmrCurrAngle_Tick(object sender, EventArgs e)
		{
			lblCurrAngle.Text = "Current Angle: " + motion.CurrentAngle;
			tblblCurrAngle.Text = lblCurrAngle.Text;
		}

		private void btnAcq_Click(object sender, EventArgs e)
		{
			if (acquire.state != AcqController.State.OPEN)
			{
				MessageBox.Show("Device isn't open");
				return;
			}

			acquire.SetImageCount((int)numAcqCount.Value);

			acquire.AcquireImage();

			using (var fs = File.Create("image.dat"))
			{
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				formatter.Serialize(fs, acquire.imageBuffer);
			}
			
			
		}

		private void btnCTAcq_Click(object sender, EventArgs e)
		{
			if (acquire.state != AcqController.State.OPEN)
			{
				MessageBox.Show("Device isn't open");
				return;
			}

			acquire.SetImageCount((int)numCtCount.Value);

			double startAngle = (double)numStartAngle.Value;
			double endAngle = (double)numEndAngle.Value;

			double step = startAngle < endAngle ? (double)numAngleIncr.Value : (double)-numAngleIncr.Value;

			Trace.WriteLine("Start: " + startAngle + "; End: " + endAngle + "; Step: " + step);

			for(double x = startAngle; step > 0 ? x <= endAngle : x >= endAngle ; x += step)
			{
				motion.Move(x, MovementType.Absolute);

				Thread.Sleep((int)(numSettle.Value * 1000));

				acquire.AcquireImage();
			}
		}

		private void cbGain_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selValue = ((ComboBox)sender).SelectedValue;

			if (selValue is XRD4343_Gain)
				acquire.SetGainMode((XRD4343_Gain)selValue);
		}
		private void cbBinning_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selValue = ((ComboBox)sender).SelectedValue;

			if (selValue is XRD4343_Binning)
				acquire.SetBinningMode((XRD4343_Binning)selValue);
		}

		private void cbFOV_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selValue = ((ComboBox)sender).SelectedValue;

			if (selValue is XRD4343_FOV)
				acquire.SetFOVMode((XRD4343_FOV)selValue);
		}
		private void cbMoveType_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selValue = ((ComboBox)sender).SelectedValue;
			
			if (!(selValue is MovementType))
				return;
				
			if ((MovementType)selValue == MovementType.Absolute)
			{
				numMoveTo.Value = numMoveTo.Value < 0 ? numMoveTo.Value + 360 : numMoveTo.Value;

				numMoveTo.Minimum = 0;
				numMoveTo.Maximum = 360;
			} else
			{
				numMoveTo.Minimum = -360;
				numMoveTo.Maximum = 360;
			}
		}

		private void UpdateButtons()
		{
			btnAcq.Enabled = acquire.state == AcqController.State.OPEN;
			btnCTAcq.Enabled = acquire.state == AcqController.State.OPEN;
			btnHome.Enabled = acquire.state == AcqController.State.OPEN;
			btnMove.Enabled = acquire.state == AcqController.State.OPEN;

			openDeviceToolStripMenuItem.Visible = acquire.state == AcqController.State.CLOSED;
			closeDeviceToolStripMenuItem.Visible = acquire.state == AcqController.State.OPEN;
		}

		private void testToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			ushort[] imagedata = null;

			using (var fs = File.OpenRead("image.dat"))
				imagedata = (ushort[])bf.Deserialize(fs);

			byte[] bytes = new byte[imagedata.Length * 2];

			Buffer.BlockCopy(imagedata, 0, bytes, 0, bytes.Length);


			var image = AForge.Imaging.UnmanagedImage.Create(2880, 2880, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);

			var ptr = image.ImageData;

			Marshal.Copy(bytes, 0, ptr, 2880 * 2880 * 2);

			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

			pictureBox1.Image = image.ToManagedImage();
		}
	}
}
