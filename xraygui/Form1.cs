using AForge.Imaging.Filters;
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

		private Action<byte[]> imageDelegate;

		private ushort[] imageData;

		private DebounceDispatcher debounceWindowLevel = new DebounceDispatcher();

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

			cbIntegration.SelectedIndex = 7;

			cbOffset.SelectedIndex = 0;
			cbGainImage.SelectedIndex = 0;
			cbPixelCorr.SelectedIndex = 0;

			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

			
			acquire.imageAcquired += Acquire_imageAcquired;
        }

		private void Acquire_imageAcquired()
		{
			// Spawn an async image update.

			try
			{
				// We're copying the current imageBytes to a new array because as soon as we return the device will continue to aquire the next image.
				ushort[] imageData = new ushort[acquire.imageBufferSize];

				Buffer.BlockCopy(acquire.imageBuffer, 0, imageData, 0, acquire.imageBufferSize);

				// Wrap this all in a task so we can continue, but do the image correct syncronously before calling update/image delegate.
				Task.Run(() =>
				{
					// DoImageRotation(ref imageData);
					this.imageData = imageData;

					byte[] imageBytes = Helpers.getBytesFromUshort(imageData);

					Task.Run(() => UpdateImage(imageBytes));

					if (imageDelegate != null)
						Task.Run(() => imageDelegate(imageBytes));
				});

			} catch(Exception ex)
			{
				Trace.WriteLine("Error in imageAcquired");
				ex.Trace();
			}
		}

		private void DoImageRotation(ref uint[] imageData)
		{
			var newData = new uint[imageData.Length];

			int width = (int)acquire.dwColumns;
			int height = (int)acquire.dwRows;

			for (var x = 0; x < width; x++)
				for (var y = 0; y < height; y++)
				{
					newData[y + (y * width)] = imageData[x + (y * width)];
					//newData[y + (x * width + 1)] = imageData[x + (y * width) + 1];
				}

			imageData = newData;
		}

		private ushort[] ApplyWindowLevel(ushort[] imageData)
		{
			//((pixelData[i] - (wLevel - wWidth / 2)) *255) * wWidth
			
			return imageData.Select((pix)=> {
				return (ushort)(((pix - sLevel.Value - sWindow.Value / 2) * ushort.MaxValue) * sWindow.Value);
			}).ToArray();
		}

		private void UpdateImage(byte[] imageData)
		{
			try
			{
				using (var image = AForge.Imaging.UnmanagedImage.Create((int)acquire.dwColumns, (int)acquire.dwRows, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale))
				{
					Marshal.Copy(imageData, 0, image.ImageData, acquire.imageBufferSize);

					var currImage = pictureBox1.Image;

					pictureBox1.Image = image.ToManagedImage();

					//If it exists, dispose if it so we aren't leaking memory.
					if (currImage != null)
						currImage.Dispose();
				}
			} catch (Exception ex)
			{
				Trace.WriteLine("Error updating live image.");
				ex.Trace();
			}
			
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
			try
			{
				var opened = motion.OpenDevice();
				var acqOpened = acquire.OpenDevice(this.Handle);

				if(!opened || !acqOpened)
				{
					MessageBox.Show("Error opening! Check log.");
					return;
				}

				Trace.WriteLine("Getting Curr Angle.");
				lblCurrAngle.Text = "Current Angle: " + motion.CurrentAngle;
				tblblCurrAngle.Text = lblCurrAngle.Text;

				this.lblStatus.Text = "Current Status: " + acquire.state;


				Trace.WriteLine("Updating buttons.");
				UpdateButtons();
			} catch (Exception ex)
			{
				Trace.WriteLine("Error opening!");
				ex.Trace();
			}
		}

		private void btnAcq_Click(object sender, EventArgs e)
		{
			if (acquire.state != AcqController.State.OPEN)
			{
				MessageBox.Show("Device isn't open");
				return;
			}
			try
			{
				var save = GetFileNameToSave(".tiff");

				if (!string.IsNullOrEmpty(save))
				{
					imageDelegate = (bytes) => {
						try
						{
							TiffLibWrapper.SaveImage(bytes, save, (int)acquire.dwColumns, (int)acquire.dwRows);
						} catch (Exception ex)
						{
							Trace.WriteLine("Error saving image to tiff.");
							ex.ToString();
						}
					};

					acquire.SetImageCount((int)numAcqCount.Value);
					acquire.AcquireImage();
				}
			} catch (Exception ex)
			{
				MessageBox.Show("Error occurred trying to acquire. Check log!");
				Trace.WriteLine("Error acquiring.");
				ex.Trace();
			}
			
		}

		private string GetSaveFolder()
		{
			using (var dialog = new FolderBrowserDialog())
			{
				//dialog.RootFolder = Environment.SpecialFolder.MyDocuments;

				if (dialog.ShowDialog() == DialogResult.OK)
					return dialog.SelectedPath;

				return null;
			}
		}

		private void btnCTAcq_Click(object sender, EventArgs e)
		{
			if (acquire.state != AcqController.State.OPEN)
			{
				MessageBox.Show("Device isn't open");
				return;
			}

			var saveToFolder = GetSaveFolder();

			acquire.SetImageCount((int)numCtCount.Value);

			double startAngle = (double)numStartAngle.Value;
			double endAngle = (double)numEndAngle.Value;

			double step = startAngle < endAngle ? (double)numAngleIncr.Value : (double)-numAngleIncr.Value;

			Trace.WriteLine("Start: " + startAngle + "; End: " + endAngle + "; Step: " + step);

			double currAngle = startAngle;

			imageDelegate = (imageBytes) => {
				try
				{
					var makeFileName = "IMAGE-{0:N2}.tif";

					TiffLibWrapper.SaveImage(imageBytes, Path.Combine(saveToFolder, string.Format(makeFileName, currAngle)), (int)acquire.dwColumns, (int)acquire.dwRows);
				} catch (Exception ex)
				{
					Trace.WriteLine("Error in save.");
					ex.Trace();
				}
			};

			for(currAngle = startAngle; step > 0 ? currAngle <= endAngle : currAngle >= endAngle ; currAngle += step)
			{
				motion.Move(currAngle, MovementType.Absolute);
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
			btnAcquireOnly.Enabled = acquire.state == AcqController.State.OPEN;

			openDeviceToolStripMenuItem.Visible = acquire.state == AcqController.State.CLOSED;
			closeDeviceToolStripMenuItem.Visible = acquire.state == AcqController.State.OPEN;
		}

		private void testToolStripMenuItem_Click(object sender, EventArgs e)
		{
		
		}

		private void cbIntegration_SelectedIndexChanged(object sender, EventArgs e)
		{
			acquire.SetCameraMode((uint)((ComboBox)sender).SelectedIndex);
		}

		private string GetFileNameToLoad(string ext)
		{
			using(OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.Filter = string.Format("*.{0}|{0}", ext);
				ofd.DefaultExt = ext;

				if (ofd.ShowDialog() == DialogResult.OK)
					return ofd.FileName;
			}
			
			return null;
		}

		private string GetFileNameToSave(string ext)
		{
			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				sfd.Filter = string.Format("*.{0}|{0}", ext);

				if (sfd.ShowDialog() == DialogResult.OK)
					return sfd.FileName;
			}

			return null;
		}

		private void CheckIntentAndSave(CorrImageType type, Object data)
		{
			var res = MessageBox.Show("Save to file?", "Save?", MessageBoxButtons.YesNo);
			if (res == DialogResult.Yes)
			{
				var filename = GetFileNameToSave(CorrImageHelper.MapCorrTypeToExt(type));
				if (!string.IsNullOrEmpty(filename))
					CorrImageHelper.WriteCorrOut(filename, type, data, acquire.dwColumns, acquire.dwRows);
			}
		}

		private CorrOutStructure LoadCorr(CorrImageType type)
		{
			var filename = GetFileNameToLoad(CorrImageHelper.MapCorrTypeToExt(type));
			if (!string.IsNullOrEmpty(filename))
				try
				{
					return CorrImageHelper.ReadCorrIn(filename);
				} catch (Exception ex)
				{
					if(ex is InvalidDataException)
					{
						MessageBox.Show("Not a valid corr image file.");
					} else
					{
						MessageBox.Show("Couldn't load, check the log for more info.");
					}
				}
			throw new Exception("Failed to load.");
		}

		

		private void cbOffset_SelectedIndexChanged(object sender, EventArgs e)
		{
			var cbSender = ((ComboBox)sender);
			var index = ((ComboBox)sender).SelectedIndex;

			switch(index)
			{
				case 0: // None
					acquire.offsetArr = null;
					break;
				case 1: // Acquire New
					var offset = acquire.GetOffsetImage();

					if (offset == null || offset.Length == 0)
					{
						MessageBox.Show("Acquiring offset image failed");
						cbSender.SelectedIndex = 0; // Back to None for you.
					}
					else
						CheckIntentAndSave(CorrImageType.Offset, offset);
					break;
				case 2: // Load
					try
					{
						var loaded = LoadCorr(CorrImageType.Offset);

						if (loaded.type != CorrImageType.Offset)
						{
							MessageBox.Show("Not an offset type. Tried to open " + loaded.type.ToString());
							cbSender.SelectedIndex = 0; // Back to None for you.
							return;
						}

						if (acquire.dwColumns != loaded.width || acquire.dwRows != loaded.height)
						{
							Trace.WriteLine(string.Format("Saved width: {0}; height: {1}; Current width: {2}; Current height: {3}", loaded.width, loaded.height, acquire.dwColumns, acquire.dwRows));
							MessageBox.Show("Loaded data wasn't saved for the current width/height, this might cause failures.");
						}

						acquire.offsetArr = (ushort[])loaded.data;

					} catch (Exception ex)
					{
						cbSender.SelectedIndex = 0; // Back to None for you.
						//We already showed a msgbox here.
					}
					break;
			}
		}

		private void cbGainImage_SelectedIndexChanged(object sender, EventArgs e)
		{
			var cbSender = ((ComboBox)sender);
			var index = ((ComboBox)sender).SelectedIndex;

			switch (index)
			{
				case 0: // None
					acquire.gainArr = null;
					break;
				case 1: // Acquire New
					var gain = acquire.GetGainImage();

					if (gain == null || gain.Length == 0)
					{
						MessageBox.Show("Acquiring gain image failed");
						cbSender.SelectedIndex = 0; // Back to None for you.
						return;
					}
					else
						CheckIntentAndSave(CorrImageType.Gain, gain);
					break;
				case 2: // Load
					try
					{
						var loaded = LoadCorr(CorrImageType.Gain);

						if (loaded.type != CorrImageType.Gain)
						{
							MessageBox.Show("Not an gain type. Tried to open " + loaded.type.ToString());
							cbSender.SelectedIndex = 0; // Back to None for you.
							return;
						}

						if (acquire.dwColumns != loaded.width || acquire.dwRows != loaded.height)
						{
							Trace.WriteLine(string.Format("Saved width: {0}; height: {1}; Current width: {2}; Current height: {3}", loaded.width, loaded.height, acquire.dwColumns, acquire.dwRows));
							MessageBox.Show("Loaded data wasn't saved for the current width/height, this might cause failures.");
						}

						acquire.gainArr = (uint[])loaded.data;

					}
					catch (Exception ex)
					{
						cbSender.SelectedIndex = 0; // Back to None for you.
													//We already showed a msgbox here.
					}
					break;
			}
		}

		private void cbPixelCorr_SelectedIndexChanged(object sender, EventArgs e)
		{
			var cbSender = ((ComboBox)sender);
			var index = ((ComboBox)sender).SelectedIndex;

			switch (index)
			{
				case 0: // None
					acquire.corrList = null;
					break;
				case 1: // Acquire New
					var pixCorr = acquire.GetPixelCorrection();

					if (pixCorr == null || pixCorr.Length == 0)
					{ 
						MessageBox.Show("Pixel Correction came back empty");
						cbSender.SelectedIndex = 0; // Back to None for you.
						return;
					}
					else
						CheckIntentAndSave(CorrImageType.PixelCorrection, pixCorr);
					break;
				case 2: // Load
					try
					{
						var loaded = LoadCorr(CorrImageType.PixelCorrection);

						if (loaded.type != CorrImageType.PixelCorrection)
						{
							MessageBox.Show("Not a pixel correction type. Tried to open " + loaded.type.ToString());
							cbSender.SelectedIndex = 0; // Back to None for you.
							return;
						}

						if (acquire.dwColumns != loaded.width || acquire.dwRows != loaded.height)
						{
							Trace.WriteLine(string.Format("Saved width: {0}; height: {1}; Current width: {2}; Current height: {3}", loaded.width, loaded.height, acquire.dwColumns, acquire.dwRows));
							MessageBox.Show("Loaded data wasn't saved for the current width/height, this might cause failures.");
						}

						acquire.corrList = (int[])loaded.data;

					}
					catch (Exception ex)
					{
						cbSender.SelectedIndex = 0; // Back to None for you.
													//We already showed a msgbox here.
					}
					break;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void tmrPollAngle_Tick(object sender, EventArgs e)
		{
			if (acquire.state == AcqController.State.OPEN)
			{
				lblCurrAngle.Text = "Current Angle: " + motion.CurrentAngle;
				tblblCurrAngle.Text = lblCurrAngle.Text;
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (acquire.state != AcqController.State.OPEN)
			{
				MessageBox.Show("Device isn't open");
				return;
			}
			try
			{
				imageDelegate = null;

				acquire.SetImageCount((int)numAcqCount.Value);
				acquire.AcquireImage();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error occurred trying to acquire. Check log!");
				Trace.WriteLine("Error acquiring.");
				ex.Trace();
			}
		}

		private void testToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			acquire.GetOffsetImage();
		}



		private void windowLevelChange(object sender, EventArgs e)
		{
			//Debounce call so we don't flood w/ requests.
			debounceWindowLevel.Debounce(2000, (obj) => {
				UpdateImage(Helpers.getBytesFromUshort(ApplyWindowLevel(this.imageData))); 
			});
		}
	}
}
