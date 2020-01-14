using System;
using System.Runtime.InteropServices;

namespace xraylib
{
    public class Acq
    {
        //////////////////////////////////////////////////////
        // IMPORTANT:
        // When implementing the XISL dll into a .net environment some datatypes might have to be changed to to size definition:
        //
        // e.g. 
        // XISL dll: long has 4 byte
        // .net: int has 4 byte, long has 8 byte
        // so long has to be important as int
        // 
        // XISL dll: char has 1 byte
        // .net: char has 2 bytes
        //
        //////////////////////////////////////////////////////

        /*
            #define DATASHORT               2 //2 byte integer
            #define DATALONG                4 //4 byte integer
            #define DATAFLOAT               8 //8 byte double
            #define DATASIGNED              16 // signed


            #define WINRESTSIZE					34
            #define WINHARDWAREHEADERSIZE		32
            #define WINRESTSIZE101				32
            #define WINHARDWAREHEADERSIZEID15	2048
            #define DETEKTOR_DATATYPE_18BIT 16
            #define MAX_GREY_VALUE_18BIT  262144

            #define __GBIF_Types
	        #define GBIF_IP_MAC_NAME_CHAR_ARRAY_LENGTH		16
	        #define GBIF_STRING_DATATYPE unsigned char
	        #define GBIF_STRING_DATATYPE_ELTEC char

        */



        public const int WINRESTSIZE = 34;
        public const int WINHARDWAREHEADERSIZE = 32;
        public const int WINRESTSIZE101 = 32;
        public const int WINHARDWAREHEADERSIZEID15 = 2048;
        public const int DETEKTOR_DATATYPE_18BIT = 16;
        public const int AX_GREY_VALUE_18BIT = 262144;
        public const int GBIF_IP_MAC_NAME_CHAR_ARRAY_LENGTH = 16;

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct GBIF_DEVICE_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = GBIF_IP_MAC_NAME_CHAR_ARRAY_LENGTH)]
            string ucMacAddress;          // unsigned since the adress components can be higher than 128

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = GBIF_IP_MAC_NAME_CHAR_ARRAY_LENGTH)]
            string ucIP;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = GBIF_IP_MAC_NAME_CHAR_ARRAY_LENGTH)]
            string ucSubnetMask;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = GBIF_IP_MAC_NAME_CHAR_ARRAY_LENGTH)]
            string ucGateway;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = GBIF_IP_MAC_NAME_CHAR_ARRAY_LENGTH)]
            string ucAdapterIP;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = GBIF_IP_MAC_NAME_CHAR_ARRAY_LENGTH)]
            string ucAdapterMask;

            ulong dwIPCurrentBootOptions;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            string cManufacturerName;                                     // PerkinElmer

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            string cModelName;                                                // GBIF

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            string cGBIFFirmwareVersion;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            string cDeviceName;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct GBIF_Detector_Properties
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            string cDetectorType;      // e.g. XRD 0822 AO 14

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 8)]
            string cManufacturingDate;                                                     // e.g. 201012

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 8)]
            string cPlaceOfManufacture;
            // e.g. DE
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            string cUniqueDeviceIdentifier;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            string cDeviceIdentifier;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            string cDummy;
        }


        /******/

        public enum HIS_RETURN : uint
        {
            /** No error @ingroup enum **/
            HIS_ALL_OK = 0,
            /** Memory couldn't be allocated. @ingroup enum **/
            HIS_ERROR_MEMORY = 1,
            /** Unable to initialize board. @ingroup enum **/
            HIS_ERROR_BOARDINIT = 2,
            /** Got a time out. May be no detector present. @ingroup enum **/
            HIS_ERROR_NOCAMERA = 3,
            /** Your correction files do not have a proper size. @ingroup enum **/
            HIS_ERROR_CORRBUFFER_INCOMPATIBLE = 4,
            /** Acquisition is already running. @ingroup enum **/
            HIS_ERROR_ACQ_ALREADY_RUNNING = 5,
            /** Got a time out from hardware. @ingroup enum **/
            HIS_ERROR_TIMEOUT = 6,
            /** Acquisition descriptor invalid. @ingroup enum **/
            HIS_ERROR_INVALIDACQDESC = 7,
            /** Unable to find VxD. @ingroup enum **/
            HIS_ERROR_VXDNOTFOUND = 8,
            /** Unable to open VxD. @ingroup enum **/
            HIS_ERROR_VXDNOTOPEN = 9,
            /** Unknown error during VxD loading. @ingroup enum **/
            HIS_ERROR_VXDUNKNOWNERROR = 10,
            /** VxD Error: GetDmaAddr failed. @ingroup enum **/
            HIS_ERROR_VXDGETDMAADR = 11,
            /** An unexpected acquisition abort occurred. @ingroup enum **/
            HIS_ERROR_ACQABORT = 12,
            /** error occurred during data acquisition. @ingroup enum **/
            HIS_ERROR_ACQUISITION = 13,
            /** Unable to register interrupt. @ingroup enum **/
            HIS_ERROR_VXD_REGISTER_IRQ = 14,
            /** Register status address failed. @ingroup enum **/
            HIS_ERROR_VXD_REGISTER_STATADR = 15,
            /** Getting version of operating system failed. @ingroup enum **/
            HIS_ERROR_GETOSVERSION = 16,
            /** Can not set frame sync. @ingroup enum **/
            HIS_ERROR_SETFRMSYNC = 17,
            /** Can not set frame sync mode. @ingroup enum **/
            HIS_ERROR_SETFRMSYNCMODE = 18,
            /** Can not set timer sync. @ingroup enum **/
            HIS_ERROR_SETTIMERSYNC = 19,
            /** Invalid function call. @ingroup enum **/
            HIS_ERROR_INVALID_FUNC_CALL = 20,
            /** Aborting current frame failed. @ingroup enum **/
            HIS_ERROR_ABORTCURRFRAME = 21,
            /** Getting hardware header failed. @ingroup enum **/
            HIS_ERROR_GETHWHEADERINFO = 22,
            /** Hardware header is invalid. @ingroup enum **/
            HIS_ERROR_HWHEADER_INV = 23,
            /** Setting line trigger mode failed. @ingroup enum **/
            HIS_ERROR_SETLINETRIG_MODE = 24,
            /** Writing data failed. @ingroup enum **/
            HIS_ERROR_WRITE_DATA = 25,
            /** Reading data failed. @ingroup enum **/
            HIS_ERROR_READ_DATA = 26,
            /** Setting baud rate failed. @ingroup enum **/
            HIS_ERROR_SETBAUDRATE = 27,
            /** No acquisition descriptor available. @ingroup enum **/
            HIS_ERROR_NODESC_AVAILABLE = 28,
            /** Buffer space not sufficient. @ingroup enum **/
            HIS_ERROR_BUFFERSPACE_NOT_SUFF = 29,
            /** Setting detector mode failed. @ingroup enum **/
            HIS_ERROR_SETCAMERAMODE = 30,
            /** Frame invalid. @ingroup enum **/
            HIS_ERROR_FRAME_INV = 31,
            /** System to slow. @ingroup enum **/
            HIS_ERROR_SLOW_SYSTEM = 32,
            /** Error during getting number of boards. @ingroup enum **/
            HIS_ERROR_GET_NUM_BOARDS = 33,
            /** Communication channel already opened by another process. @ingroup enum **/
            HIS_ERROR_HW_ALREADY_OPEN_BY_ANOTHER_PROCESS = 34,
            /** Error creating memory mapped file. @ingroup enum **/
            HIS_ERROR_CREATE_MEMORYMAPPING = 35,
            /** Error registering DMA address. @ingroup enum **/
            HIS_ERROR_VXD_REGISTER_DMA_ADDRESS = 36,
            /** Error registering static address. @ingroup enum **/
            HIS_ERROR_VXD_REGISTER_STAT_ADDR = 37,
            /** Unable to unmask interrupt. @ingroup enum **/
            HIS_ERROR_VXD_UNMASK_IRQ = 38,
            /** Unable to load driver. @ingroup enum **/
            HIS_ERROR_LOADDRIVER = 39,
            /** Function is not implemented. @ingroup enum **/
            HIS_ERROR_FUNC_NOTIMPL = 40,
            /** Unable to create memory mapping. @ingroup enum **/
            HIS_ERROR_MEMORY_MAPPING = 41,
            /** Could not create Mutex. @ingroup enum **/
            HIS_ERROR_CREATE_MUTEX = 42,
            /** Error starting the acquisition. @ingroup enum **/
            HIS_ERROR_ACQ = 43,
            /** Acquisition descriptor is not local. @ingroup enum **/
            HIS_ERROR_DESC_NOT_LOCAL = 44,
            /** Invalid Parameter. @ingroup enum **/
            HIS_ERROR_INVALID_PARAM = 45,
            /** Error during abort acquisition function. @ingroup enum **/
            HIS_ERROR_ABORT = 46,
            /** The wrong board is selected. @ingroup enum **/
            HIS_ERROR_WRONGBOARDSELECT = 47,
            /** Change of Detector Mode during Acquisition. @ingroup enum **/
            HIS_ERROR_WRONG_CAMERA_MODE = 48,
            /** The number of images for frame grabber onboard averaging must be  = 2,
             HIS_ERROR_AVERAGED_LOST = 49,
/** Parameter for (onboard) sorting not valid. @ingroup enum **/
            HIS_ERROR_BAD_SORTING_PARAM = 50,
            /** Connection to Network Detector cannot be opened due to invalid IP address / MAC / Detector name. @ingroup enum **/
            HIS_ERROR_UNKNOWN_IP_MAC_NAME = 51,
            /** Detector could not be found in the Subnet. @ingroup enum **/
            HIS_ERROR_NO_BOARD_IN_SUBNET = 52,
            /** Unable to open connection to Network Detector. @ingroup enum **/
            HIS_ERROR_UNABLE_TO_OPEN_BOARD = 53,
            /** Unable to close connection to Network Detector. @ingroup enum **/
            HIS_ERROR_UNABLE_TO_CLOSE_BOARD = 54,
            /** Unable to access the flash memory of Detector. @ingroup enum **/
            HIS_ERROR_UNABLE_TO_ACCESS_DETECTOR_FLASH = 55,
            /** No frame header received from Detector. @ingroup enum **/
            HIS_ERROR_HEADER_TIMEOUT = 56,
            /** Command not acknowledged. @ingroup enum **/
            HIS_ERROR_NO_FPGA_ACK = 57,
            /** Number of boards within network changed during broadcast. @ingroup enum **/
            HIS_ERROR_NR_OF_BOARDS_CHANGED = 58,
            /** Unable to set the exam flag. @ingroup enum **/
            HIS_ERROR_SETEXAMFLAG = 59,
            /** Error Function called with an illegal index number. @ingroup enum **/
            HIS_ERROR_ILLEGAL_INDEX = 60,
            /** Error Function or function environment not correctly initialised. @ingroup enum **/
            HIS_ERROR_NOT_INITIALIZED = 61,
            /** Error No detectors discovered yet. @ingroup enum **/
            HIS_ERROR_NOT_DISCOVERED = 62,
            /** Error onbaord averaging failed. @ingroup enum **/
            HIS_ERROR_ONBOARDAVGFAILED = 63,
            /** Error getting onboard offset. @ingroup enum **/
            HIS_ERROR_GET_ONBOARD_OFFSET = 64,
            /** Error CURL. @ingroup enum **/
            HIS_ERROR_CURL = 65,
            /** Error setting onboard offset corr mode. @ingroup enum **/
            HIS_ERROR_ENABLE_ONBOARD_OFFSET = 66,
            /** Error setting onboard mean corr mode. @ingroup enum **/
            HIS_ERROR_ENABLE_ONBOARD_MEAN = 67,
            /** Error setting onboard gain corr mode. @ingroup enum **/
            HIS_ERROR_ENABLE_ONBOARD_GAINOFFSET = 68,
            /** Error setting onboard preview mode. @ingroup enum **/
            HIS_ERROR_ENABLE_ONBOARD_PREVIEW = 69,
            /** Error setting onboard binning mode. @ingroup enum **/
            HIS_ERROR_SET_ONBOARD_BINNING = 70,
            /** Error Loading image from SD to onboard buffer. @ingroup enum **/
            HIS_ERROR_LOAD_COORECTIONIMAGETOBUFFER = 71,
            /** Error Invalid pointer/buffer passed as parameter. @ingroup enum **/
            HIS_ERROR_INVALIDBUFFERNR = 72,
            /** Error Invalid SHOCKID. @ingroup enum **/
            HIS_ERROR_INVALID_HANDLE = 73,
            /** Error Invalid filename, file already exists. @ingroup enum **/
            HIS_ERROR_ALREADY_EXISTS = 74,
            /** Error Invalid filename type does not exist. @ingroup enum **/
            HIS_ERROR_DOES_NOT_EXIST = 75,
            /** Error Invalid filename for image tag or log file. @ingroup enum **/
            HIS_ERROR_OPEN_FILE = 76,
            /** Error Invalid filename for image tag or log file. @ingroup enum **/
            HIS_ERROR_INVALID_FILENAME = 77,
            /** Error setting gbif discovery timeout. @ingroup enum **/
            HIS_ERROR_SETDISCOVERYTIMEOUT = 78,
            // DEXELA
            HIS_ERROR_SERIALREAD = 100,
            // DEXELA
            HIS_ERROR_SERIALWRITE = 101,
            //  = 1313,
            HIS_ERROR_SETDAC = 102,
            //  = 1313,
            HIS_ERROR_SETADC = 103,
            /** Error setting the onboard image tag. @ingroup enum **/
            HIS_ERROR_SET_IMAGE_TAG = 104,
            /** Error setting the onboard process script. @ingroup enum **/
            HIS_ERROR_SET_PROC_SCRIPT = 105,
            /** Error Image tag length exceeded  = 128,
             HIS_ERROR_SET_IMAGE_TAG_LENGTH = 106,
/** Error retrieving the enhanced header. @ingroup enum **/
            HIS_ERROR_RETRIEVE_ENHANCED_HEADER = 107,
            /** Error enabling XRPD interrupts. @ingroup enum **/
            HIS_ERROR_ENABLE_INTERRUPTS = 108,
            /** Error XRPD session Error. @ingroup enum **/
            HIS_ERROR_XRPD_SESSION_ERROR = 109,
            /** Error No interface to communicate event messages active. @ingroup enum **/
            HIS_ERROR_XRPD_SET_EVENT = 110,
            /** Error No interface to communicate event messages active. @ingroup enum **/
            HIS_ERROR_XRPD_NO_EVENT_INTERFACE = 111,
            /** Error creating fake shock events. @ingroup enum **/
            HIS_ERROR_XRPD_CREATE_FAKE_SHOCK_EVENT = 112,
            /** Error retrieving the sd card info. @ingroup enum **/
            HIS_ERROR_XRPD_GET_SDCARD_INFO = 113,
            /** Error activating the fake temperatur mode on the detector. @ingroup enum **/
            HIS_ERROR_XRPD_SET_TEMP_FAKE_MODE = 114,
            /** Error the requested EMI readout mode was not reported by the detector. @ingroup enum **/
            HIS_ERROR_EMI_NOT_SET = 115,
            /** Error retrieving the location info from the detector. @ingroup enum **/
            HIS_ERROR_XRPD_NO_LOCATION = 116,
            /** Error setting the on detector idle timeout. @ingroup enum **/
            HIS_ERROR_SET_IDLE_TIMEOUT = 117,
            /** Error setting the software requested charge mode. @ingroup enum **/
            HIS_ERROR_SET_CHARGE_MODE = 118,
            /** Error creating critical level fake shock events. @ingroup enum **/
            HIS_ERROR_XRPD_CREATE_FAKE_SHOCK_EVENT_CRIT = 119,
            /** Error creating warning level fake shock events. @ingroup enum **/
            HIS_ERROR_XRPD_CREATE_FAKE_SHOCK_EVENT_WARN = 120,
            /** Error resetting the shock events to factory values. @ingroup enum **/
            HIS_ERROR_XRPD_FACTORY_RESET_SHOCK_EVENT = 121,
            /** Error getting the on detector LAN network speed. @ingroup enum **/
            HIS_ERROR_XRPD_NO_NETWORK = 122,
            /** Error setting the on detector LAN network speed. @ingroup enum **/
            HIS_ERROR_XRPD_SET_NETWORK = 123,
            /** Error verifying the private key for genuiness. @ingroup enum **/
            HIS_ERROR_XRPD_VERIFY_GENUINENESS = 124,
            /** Error setting the private key for genuiness. @ingroup enum **/
            HIS_ERROR_XRPD_SET_PRIVATE_KEY = 125,
            /** Error setting the temperature timeout. @ingroup enum **/
            HIS_ERROR_XRPD_SET_TEMPERATURE_TIMEOUT = 126,
            /** Error resetting the temperature timeout counter. @ingroup enum **/
            HIS_ERROR_XRPD_RESET_TEMPERATURE_TIMEOUT = 127,
            /** Error setting the temperature thresholds on the detector. @ingroup enum **/
            HIS_ERROR_XRPD_SET_TEMPERATURE_THRESHOLDS = 128,
            /** Error getting the temperature thresholds from the detector. @ingroup enum **/
            HIS_ERROR_XRPD_GET_TEMPERATURE_THRESHOLDS = 129,
            /** Error no eventcallback defined for irq messages. @ingroup enum **/
            HIS_ERROR_XRPD_NO_EVENTCALLBACK_DEFINED = 130,
            /** Error setting on detectors date and time. @ingroup enum **/
            HIS_ERROR_XRPD_SET_DATE_TIME = 131,
            /** Error triggering the resend of all current messages by the XRPD. @ingroup enum **/
            HIS_ERROR_XRPD_RESEND_ALL_MSG = 132,
            /** Error acknowledging the image. @ingroup enum **/
            HIS_ERROR_ACKNOWLEDGE_IMAGE = 133,
            /** Error connecting to the on detector XRPD process. @ingroup enum **/
            HIS_ERROR_XRPD_CONNECT = 134,
            /** Error resetting the shock event. @ingroup enum **/
            HIS_ERROR_XRPD_RESET_SHOCK = 135,
            /** Error setting the power state on the detector. @ingroup enum **/
            HIS_ERROR_XRPD_REQUEST_POWERSTATE = 136,
            /** Error retrieving the auto power on locations from the detector. @ingroup enum **/
            HIS_ERROR_XRPD_GET_AUTOPOWERONLOCATIONS = 137,
            /** Error setting the auto power on locations on the detector. @ingroup enum **/
            HIS_ERROR_XRPD_SET_AUTOPOWERONLOCATIONS = 138,
            /** Error retrieving the requested charge mode from the detector. @ingroup enum **/
            HIS_ERROR_GET_CHARGE_MODE = 139,
            /** Error requesting an fscheck on next boot. @ingroup enum **/
            HIS_ERROR_XRPD_SET_FORCE_FSCK = 140,
            /** Error setting the sd card timeout on the detector. @ingroup enum **/
            HIS_ERROR_XRPD_SET_SDCARD_TIMEOUT = 141,
            /** Error getting the sd card timeout from the detector. @ingroup enum **/
            HIS_ERROR_XRPD_GET_SDCARD_TIMEOUT = 142,
            /** Error not connected to on detector XRPD process. @ingroup enum **/
            HIS_ERROR_MISSING_VERSION_INFORMATION = 143,
            /** Error not connected to on detector XRPD process. @ingroup enum **/
            HIS_ERROR_XRPD_NOT_CONNECTED = 144,
            /** Error retrieving the SD card performance. @ingroup enum **/
            HIS_ERROR_XRPD_SDCARDPERFORMANCE = 145,
            /** Requested channel is already openend. @ingroup enum **/
            HIS_ERROR_HW_BOARD_CHANNEL_ALREADY_USED = 146,
            /** Error retrieving the Voltage or Current from detector. @ingroup enum **/
            HIS_ERROR_XRPD_GET_CURRENT_VOLTAGE = 147,
            /** Unable to set on detector CPU govenor. @ingroup enum **/
            HIS_ERROR_XRPD_SET_CPUFREQ_GOVERNOR = 148,
        } 

        public enum HIS_BOARD : uint
        {
            HIS_BOARD_TYPE_NOONE = 0x0,
            HIS_BOARD_TYPE_ELTEC = 0x1,
            HIS_BOARD_TYPE_DIPIX = 0x2,
            HIS_BOARD_TYPE_RS232 = 0x3,
            HIS_BOARD_TYPE_USB = 0x4,
            HIS_BOARD_TYPE_ELTEC_XRD_FGX = 0x8,
            HIS_BOARD_TYPE_ELTEC_XRD_FGE_Opto = 0x10,
            HIS_BOARD_TYPE_ELTEC_GbIF = 0x20,
            HIS_BOARD_TYPE_ELTEC_WPE = 0x40,    // mk 2013-04-16 additional functions for wpe lib
            HIS_BOARD_TYPE_ELTEC_EMBEDDED = 0x60,   // mk 2013-04-16 embedded is gbif and wpe
            HIS_BOARD_TYPE_CMOS = 0x100,                                // msi 2013-06-20 CMOS are all devices with CMOS FW (until now 1512, 13x13) / => HIS_BOARD_TYPE_DEXELA_1512 | define HIS_BOARD_TYPE_ELTEC_13x13
            HIS_BOARD_TYPE_ELTEC_13x13 = 0x320, // msi 2013-06-21 13x13 includes gbif and CMOS / => 0x200 | HIS_BOARD_TYPE_ELTEC_GbIF | HIS_BOARD_TYPE_CMOS
            HIS_BOARD_TYPE_DEXELA_1512CL = 0x500,	// msi 2013-06-21 13x13 includes and CMOS / => 0x400 | HIS_BOARD_TYPE_CMOS
        }

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_Init(ref IntPtr phAcqDesc,
                      HIS_BOARD dwChannelType, int nChannelNr,
                      bool bEnableIRQ,
                      uint Rows, uint Columns,
                      uint dwSortFlags,
                      bool bSelfInit, bool bAlwaysOpen
                      );

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_CloseAll();
    }
}
