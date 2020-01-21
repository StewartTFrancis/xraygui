using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xraylib;

namespace xraygui
{
    public partial class Form1 : Form
    {
		AcqController acq = new AcqController();
		MotionController motion = new MotionController();

		public Form1()
        {
            InitializeComponent();

			cbBinning.SelectedIndex = 0;
        }

        private void doSomething()
        {
			{
				Trace.WriteLine("Motion open: " + motion.OpenDevice());
				//Trace.WriteLine("Motion Home: " + motion.Home());
				motion.CloseDevice();

				Trace.WriteLine("Acquire open: " + acq.OpenDevice());

				this.lblStatus.Text = "Current Status: " + acq.state;
				// Trace.WriteLine("Acquire image: " + acq.AcquireImage()); 

				acq.CloseDevice();
				this.lblStatus.Text = "Current Status: " + acq.state;

			}

				/* 
				 * int framesToAcquire = 5;
					printf("\nXIS X-ray Imaging Software - Demo starting\n");

					UINT returnValue = Initialize_Detector();
					if (returnValue != HIS_ALL_OK) {
						std::cout << "Issue with Initialization" << std::endl;
					}


					unsigned short *acquisitionBuffer = (unsigned short *)malloc(2 * rows * columns * sizeof(unsigned short)); //define destination buffer

					returnValue = Acquisition_DefineDestBuffers(detectorDescriptor, acquisitionBuffer, framesToAcquire, rows, columns);
					std::cout << "Destination Buffers Defined" << std::endl;

					//Set_Offset_Image(2, 1);
					//Sleep(3000);
					Acquire_Image(framesToAcquire);
					char filename[] = "output.tif";
					Sleep(3000); //haphazard way to fix timing issues. Will be removed in later versions
					Save_Image(acquisitionBuffer, filename);
					//0 is the number of frames to skip before frames are actually recorded (support x-ray start up transients possibly)
					//HIS_SEQ_AVERAGE is how to handle frames, can be HIS_SEQ_CONTINUOUS or other

					printf("rows: %d\ncolumns: %d\n", rows, columns);
					Acquisition_CloseAll(); */
        }

		private void acquireToolStripMenuItem_Click(object sender, EventArgs e)
		{
			doSomething();
		}

		private void cbBinning_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (acq.state != AcqController.State.OPEN)
				return;

			ComboBox ourCB = (ComboBox)sender;

			Acq.DETECTOR_BINNING binMode = Acq.DETECTOR_BINNING.BINNING_1x1;

			/*
				No binning
				2x2 binning
				4x4 binning (3x3 R&F)
				1x2 binning
				1x4 binning
			*/

			switch(ourCB.SelectedIndex)
			{
				case 0:
					binMode = Acq.DETECTOR_BINNING.BINNING_1x1;
					break;
				case 1:
					binMode = Acq.DETECTOR_BINNING.BINNING_2x2;
					break;
				case 2:
					binMode = Acq.DETECTOR_BINNING.BINNING_4x4;
					break;
				case 3:
					binMode = Acq.DETECTOR_BINNING.BINNING_1x2;
					break;
				case 4:
					binMode = Acq.DETECTOR_BINNING.BINNING_1x4;
					break;
				default:
					// What?
					break;
			}

		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			acq.Dispose();
			motion.Dispose();
		}
	}
}
