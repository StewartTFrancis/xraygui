using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

			cbBinning.SelectedIndex = 0;
			cbMoveType.SelectedIndex = 0;
        }

		private void cbBinning_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (acquire.state != AcqController.State.OPEN)
				return;

			Acq.DETECTOR_BINNING binMode = mapIndexToBin(((ComboBox)sender).SelectedIndex);

			if (acquire.SetBinningMode(binMode) != Acq.HIS_RETURN.HIS_ALL_OK)
				MessageBox.Show("Error setting binning mode. Check the log");
		}

		private Acq.DETECTOR_BINNING mapIndexToBin(int index)
		{
			/*
				No binning
				2x2 binning
				4x4 binning (3x3 R&F)
				1x2 binning
				1x4 binning
			*/

			switch (index)
			{
				case 0:
					return Acq.DETECTOR_BINNING.BINNING_1x1;
				case 1:
					return Acq.DETECTOR_BINNING.BINNING_2x2;
				case 2:
					return Acq.DETECTOR_BINNING.BINNING_4x4;
				case 3:
					return Acq.DETECTOR_BINNING.BINNING_1x2;
				case 4:
					return Acq.DETECTOR_BINNING.BINNING_1x4;
				default:
					// What? If we're here, someone's changed something
					throw new InvalidOperationException("We got an invalid index in mapIndexToBin");
			}
		}

		private MovementType mapIndexToMovement(int index)
		{
			if (index == 0)
				return MovementType.Relative;
			else if (index == 1)
				return MovementType.Absolute;

			throw new InvalidOperationException("We got an invalid index in mapIndexToMovement");

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
			motion.Move((double)numMoveTo.Value, mapIndexToMovement(cbMoveType.SelectedIndex));
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

		private void cbMoveType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(mapIndexToMovement(((ComboBox)sender).SelectedIndex) == MovementType.Absolute)
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
	}
}
