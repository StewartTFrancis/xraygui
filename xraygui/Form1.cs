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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			using (AcqController acq = new AcqController())
			using (MotionController motion = new MotionController())
			{
				Trace.WriteLine("Motion open: " + motion.OpenDevice());
				Trace.WriteLine("Motion Home: " + motion.Home());
				motion.CloseDevice();



				Trace.WriteLine("Acquire open: " + acq.OpenDevice());
				acq.CloseDevice();
				
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

				try
				{





				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			 
        }
    }
}
