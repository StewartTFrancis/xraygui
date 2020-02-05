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
	        #define GBIF_STRING_DATATYPE Byte
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

            int dwIPCurrentBootOptions;

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

        public enum HIS_GbIF : uint
        {
            FIRST_CAM = 0,
            IP = 1,
            MAC = 2,
            NAME = 3
        }

        public enum HIS_GbIF_IP : uint
        {
            STATIC = 1,
            DHCP = 2,
            LLA = 4
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct CHwHeaderInfo
        {
            int dwPROMID;
            int dwHeaderID;
            bool bAddRow;
            bool bPwrSave;
            int dwNrRows;
            int dwNrColumns;
            int dwZoomULRow;
            int dwZoomULColumn;
            int dwZoomBRRow;
            int dwZoomBRColumn;
            int dwFrmNrRows;
            int dwFrmRowType;
            int dwFrmFillRowIntervalls;
            int dwNrOfFillingRows;
            int dwDataType;
            int dwDataSorting;
            int dwTiming;
            int dwAcqMode;
            int dwGain;
            int dwOffset;
            int dwAccess;
            bool bSyncMode;
            int dwBias;
            int dwLeakRows;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct CHwHeaderInfoEx
        {
            ushort wHeaderID;     // 0
            ushort wPROMID;       // 1
            ushort wResolutionX;  // 2
            ushort wResolutionY;  // 3
            ushort wNrRows;       // 4
            ushort wNrColumns;        // 5
            ushort wZoomULRow;        // 6
            ushort wZoomULColumn; // 7
            ushort wZoomBRRow;        // 8
            ushort wZoomBRColumn; // 9
            ushort wFrmNrRows;        // A
            ushort wFrmRowType;   // B
            ushort wRowTime;      // C << 6
            ushort wClock;            // D << 6
            ushort wDataSorting;  // E
            ushort wTiming;       // F
            ushort wGain;         // 10
            ushort wLeakRows;     // 11
            ushort wAccess;       // 12
            ushort wBias;         // 13
            ushort wUgComp;       // 14
            ushort wCameratype;   // 15
            ushort wFrameCnt;     // 16
            ushort wBinningMode;  // 17
            ushort wRealInttime_milliSec; // 18 
            ushort wRealInttime_microSec; // 19
            ushort wStatus;       // 1A
            ushort wCommand1;     // 1B
            ushort wCommand2;     // 1C
            ushort wCommand3;     // 1D
            ushort wCommand4;     // 1E
            ushort wDummy;			// 1F
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct WinHeaderType
        {

            ushort FileType;          // File ID (0x7000)
            ushort HeaderSize;        // Size of this file header in Bytes
            ushort HeaderVersion;     // 100
            uint FileSize;          // Size of the whole file in Bytes ( HeaderSize+ImageHeaderSize+Frames*rows*columns*datatypesize )
            ushort ImageHeaderSize;   // Size of the image header in Bytes
            ushort ULX, ULY, BRX, BRY;// bounding rectangle of the image
            ushort NrOfFrames;        // Nr of Frames in seq
            ushort Correction;        // 0 = none, 1 = offset, 2 = gain, 4 = bad pixel, (ored) can be 0
            double IntegrationTime; // frame time in microseconds can by 0
            ushort TypeOfNumbers;     // refer to enum XIS_FileType
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = WINRESTSIZE)]
            string x;        // fill up to 68 byte
        }




        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct WinHeaderType101
        {

            ushort FileType;          // File ID (0x7000)
            ushort HeaderSize;        // Size of this file header in Bytes
            ushort HeaderVersion;     // 101
            uint FileSize;          // Size of the whole file in Bytes ( HeaderSize+ImageHeaderSize+Frames*rows*columns*datatypesize )
            ushort ImageHeaderSize;   // Size of the image header in Bytes
            ushort ULX, ULY, BRX, BRY;// bounding rectangle of the image
            ushort NrOfFrames;        // Nr of Frames in seq
            ushort Correction;        // 0 = none, 1 = offset, 2 = gain, 4 = bad pixel, (ored) can be 0
            double IntegrationTime; // frame time in microseconds
            ushort TypeOfNumbers;     // refer to enum XIS_FileType
            ushort wMedianValue;      // median of the image / can be 0. use 0 for onboard corrections. meadian for gain corr will be automatically calculated
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = WINRESTSIZE101)]
            string x; // fill up to 68 byte
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct WinImageHeaderType
        {

            uint dwPROMID;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 6)]
            string strProject;     // project / cam-nr

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 3)]
            string strSystemused;  //

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 9)]
            string strPrefilter;   // used filter
            float fKVolt;               // 
            float fAmpere;          //
            ushort n_avframes;          // average count

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct FPGAType
        {

            byte wTiming;      //	Timing und Triggermode
            byte wValue0;      // 
            byte wValue1;
            byte wValue2;
            byte wValue3;
            byte wValue4;
            byte wValue5;
            byte wValue6;
        }           // 8 Byte werden bertragen


        public static uint EPC_REGISTER_LENGTH = 1024;

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct RTC_STRUCT
        {
            uint year;
            uint month;                                // e.g.: 5 for may
            uint day;
            uint hour;
            uint minute;
            uint second;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct DETECTOR_BATTERY
        {
            uint status;                           // D0: present
                                                   // D1: charging
            uint serial_no;
            uint cycle_count;
            uint temperature;                          // e.g.: 2510 for 25.1 C 
            uint voltage;                          // in mV
            uint current;                          // in mA (positive or negative)
            uint capacity;                         // in %
            uint energy;                               // in mWh
            uint charge;                               // in mAh

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct EPC_REGISTER
        {
            uint version;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            uint[] temperature_value;             // in 1/1000 C (e.g. 43000 for 43 C)

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            uint[] temperature_warning_level;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            uint[] temperature_error_level;

            RTC_STRUCT rtc_value;
            DETECTOR_BATTERY battery;
            uint power_state;
            uint sdcard_state;                     // D0: is mounted flag
            uint sdcard_usage;                     // in %
            uint active_network_config;
            uint lan_status_register;              // D0: LAN enabled
                                                   // D1: LAN up
                                                   // D2: LAN used for image transfer
            uint wlan_status_register;             // D0: WLAN enabled
                                                   // D1: WLAN up
                                                   // D2: WLAN used for image transfer
                                                   // D3: is accesspoint (0 for station)
                                                   // D4: HT20 mode
                                                   // D5: HT40+ mode
                                                   // D6: HT40- mode
            uint signal_strength;                  // in dBm
            uint channel;
            uint exam_flag;
            uint spartan_id;
            CHwHeaderInfoEx spartan_register;
        }

        /* internal voltages and currents of Gen2 detector */
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct DETECTOR_CURRENT_VOLTAGE
        {
            int iV1;
            int imA1;
            int iV2;
            int imA2;
            int iV3;
            int imA3;
        }

        /** Possible system control actions for XRpad 
        * @ingroup enum
*/
        public enum XRpad_SystemControlEnum : uint
        {
            XRpad_SYSTEM_CONTROL_REBOOT = 0,           // restart XRpad 
            XRpad_SYSTEM_CONTROL_RESTART_NETWORK = 1,  // restart XRpad Network 
            XRpad_SYSTEM_CONTROL_SHUTDOWN = 2,         // shutdown XRpad
            XRpad_SYSTEM_CONTROL_SET_DEEP_SLEEP = 3,   // power down analog circuitry and sensor FPGA
            XRpad_SYSTEM_CONTROL_SET_IDLE = 4          // power up analog circuitry and sensor FPGA
        }

        /**
        * @ingroup enum
*/
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct XRpad_TempSensor
        {
            char index;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string name;

            bool is_virtual;
            byte warn_level;
            double temperature;
        }


        /**
        * @ingroup enum
*/
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct XRpad_TempSensorReport
        {
            byte system_warn_level;
            byte sensor_count;
            uint shutdown_time;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            XRpad_TempSensor[] sensors;
        }


        /**
        * @ingroup enum
*/
        public enum XRpad_ChargeMode : uint
        {
            XRpad_NOT_CHARGING = 0,
            XRpad_CHARGING_SLOW = 1,
            XRpad_CHARGING_NORMAL = 2,
            XRpad_CHARGING_FAST = 3,
            XRpad_FULLY_CHARGED = 4,
            XRpad_DISCHARGING = 5
        }


        /**
        * @ingroup enum
*/
        public enum XRpad_BatteryPresence : uint
        {
            XRpad_NO_BATTERY = 0,
            XRpad_BATTERY_INSERTED = 1,
            XRpad_DUMMY_INSERTED = 2
        }


        /**
        * @ingroup enum
*/
        public enum XRpad_BatteryHealth : uint
        {
            XRpad_BATTERY_OK = 0x0000,
            XRpad_COMMUNICATION_ERROR = 0x0001,
            XRpad_TERMINATE_DISCHARGE_ALARM = 0x0002,
            XRpad_UNDERVOLTAGE_ALARM = 0x0004,
            XRpad_OVERVOLTAGE_ALARM = 0x0008,
            XRpad_OVERTEMPERATURE_ALARM = 0x0010,
            XRpad_BATTERY_UNKNOWN_ERROR = 0x0080,

        }


        /**
        * @ingroup enum
*/
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct XRpad_BatteryStatus
        {
            XRpad_BatteryPresence presence;
            int design_capacity;
            int remaining_capacity;
            int charge_state;
            XRpad_ChargeMode charge_mode;
            int cycle_count;
            int temperature;
            int authenticated;
            int health;
        }



        /**
         * @ingroup enum
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct XRpad_ShockEvent
        {
            uint timestamp;
            uint critical_sensor1;
            uint critical_sensor2;
            uint critical_sensor3;
            uint warning_sensor1;
            uint warning_sensor2;
            uint warning_sensor3;
        }



        /**
         * @ingroup enum
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct XRpad_ShockSensorReport
        {
            XRpad_ShockEvent largest;
            XRpad_ShockEvent latest;
        }




        /**
         * @ingroup enum
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct XRpad_VersionInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            string subversion;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string linux_kernel;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string software;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string hwdriver;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string zynq_firmware;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string spartan_firmware;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string msp_firmware;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string pld_firmware;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string xrpd;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string wlan;
        }



        /** Possible image transfer channels for XRpad
        * @ingroup enum
*/
        public enum XRpad_DataInterfaceControlEnum : uint
        {
            XRpad_DATA_VIA_LAN = 0,             // use LAN
            XRpad_DATA_VIA_WLAN = 1             // use WLAN
        }


        /**
        * @ingroup enum
*/
        public enum XislLoggingLevels : uint
        {
            LEVEL_TRACE = 0,
            LEVEL_DEBUG,
            LEVEL_INFO,
            LEVEL_WARN,
            LEVEL_ERROR,
            LEVEL_FATAL,
            LEVEL_ALL,
            LEVEL_NONE
        }

        /**
        * @ingroup enum
*/
        // typedef void* XislFtpSession; // IntPtr

        /**
        * @ingroup enum
*/
        // typedef void* XislFileHandle; // IntPtr

        /**
        * @ingroup enum
*/
        public enum XislFileEntryType : uint
        {
            XFT_File = 1,
            XFT_Directory = 2,
            XFT_Link = 4,
            // ...
            XFT_Other = 0x80000000,
            XFT_Any = 0xFFFFFFFF
        }

        /**
        * @ingroup enum
*/
        public enum XislFileStorageLocation : uint
        {
            XFSL_Local = 0,
            XFSL_FTP = 1
        }


        public struct XislFileInfo
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            string filename;

            [MarshalAs(UnmanagedType.LPWStr)]
            string directory;

            [MarshalAs(UnmanagedType.LPWStr)]
            string address;
            uint filesize; // Originally size_t
            XislFileEntryType type;

            [MarshalAs(UnmanagedType.LPWStr)]
            string timestamp;
            // XislFileStorageLocation location;
        }

        /**
        * @ingroup enum
*/
        public enum ProcScriptOperation : uint
        {
            PREBINNING,
            PREMEAN,
            PRESTOREBUFFER,
            OFFSET,
            GAIN,
            MEAN,
            PREVIEW,
            BINNING,
            STOREBUFFER,
            STORESD,
            SEND
        }

        /**
        * @ingroup enum
*/
        public enum OnboardBinningMode : uint
        {
            ONBOARDBINNING2x1 = 0,
            ONBOARDBINNING2x2 = 1,
            ONBOARDBINNING4x1 = 2,
            ONBOARDBINNING4x4 = 3,
            ONBOARDBINNING3x3 = 4,
            ONBOARDBINNING9to4 = 5
        }

        /**
        * @ingroup enum
*/
        public enum XIS_DetectorTriggerMode : uint
        {
            TRIGGERMODE_DDD,
            TRIGGERMODE_DDD_WO_CLEARANCE,
            TRIGGERMODE_STARTSTOP,
            TRIGGERMODE_FRAMEWISE,
            TRIGGERMODE_AED,
            TRIGGERMODE_ROWTAG,
            TRIGGERMODE_DDD_POST_OFFSET,
            TRIGGERMODE_DDD_DUAL_POST_OFFSET
        }

        /**
        * @ingroup enum
*/
        public enum XIS_Detector_TRIGOUT_SignalMode : uint
        {
            TRIGOUT_SIGNAL_FRM_EN_PWM,
            TRIGOUT_SIGNAL_FRM_EN_PWM_INV,
            TRIGOUT_SIGNAL_EP,
            TRIGOUT_SIGNAL_EP_INV,
            TRIGOUT_SIGNAL_DDD_Pulse,
            TRIGOUT_SIGNAL_DDD_Pulse_INV,
            TRIGOUT_SIGNAL_GND,
            TRIGOUT_SIGNAL_VCC
        }

        /**
        * @ingroup enum
*/
        public enum XIS_FileType : uint
        {
            PKI_RESERVED = 1,
            PKI_DOUBLE = 2,
            PKI_SHORT = 4,
            PKI_SIGNED = 8,
            PKI_ERRORMAPONBOARD = 16,
            PKI_LONG = 32,
            PKI_SIGNEDSHORT = PKI_SHORT | PKI_SIGNED,
            PKI_SIGNEDLONG = PKI_LONG | PKI_SIGNED,
            PKI_FAULTMASK = PKI_LONG | PKI_RESERVED
        }

        /**
        * @ingroup enum
*/
        public enum XIS_Event : uint
        {
            XE_ACQUISITION_EVENT = 0x00000001,
            XE_SENSOR_EVENT = 0x00000002,
            XE_SDCARD_EVENT = 0x00000004,
            XE_BATTERY_EVENT = 0x00000005,
            XE_LOCATION_EVENT = 0x00000006,
            XE_NETWORK_EVENT = 0x00000007,
            XE_DETECTOR_EVENT = 0x00000008,
            XE_LIBRARY_EVENT = 0x00000009,
            XE_SDCARD_FSCK_EVENT = 0x0000000A,
        }

        /**
        * @ingroup enum
*/
        public enum XIS_Acquisition_Event : uint
        {
            XAE_TRIGOUT = 0x00000002,
            XAE_READOUT = 0x00000004,
        }

        /**
        * @ingroup enum
*/
        public enum XIS_Sensor_Event : uint
        {
            XSE_HALL = 0x00000001,
            XSE_SHOCK = 0x00000010,
            XSE_TEMPERATURE = 0x00000020,
            XSE_TEMPERATURE_BACK_TO_NORMAL = 0x00000021,
            XSE_THERMAL_SHUTDOWN = 0x00000022,
        }

        /**
        * @ingroup enum
*/
        public enum XIS_Battery_Event : uint
        {
            XBE_BATTERY_REPORT = 0x00000001,
            XBE_BATTERY_WARNING = 0x00000002,
        }

        /**
        * @ingroup enum
*/
        public enum XIS_Detector_Event : uint
        {
            XDE_BUFFERS_IN_USE = 0x00000001,
            XDE_STORED_IMAGE = 0x00000002,
            XDE_DROPPED_IMAGE = 0x00000003,
        }

        /**
        * @ingroup enum
*/
        public enum XIS_Library_Event : uint
        {
            XLE_HIS_ERROR_PACKET_LOSS = 0x00000001, //!< Frame is lost. not all network packages could be received
        }


        // See: https://stackoverflow.com/questions/5235445/pinvoke-c-function-takes-pointer-to-function-as-argument
        // typedef void (* XIS_EventCallback) (XIS_Event, uint, uint, void*, void*);

        // wpe library includes
        /* Error codes */



        /* #ifdef _DLL_EXPORT
        #include "wpe200def.h"
        #else*/
        public enum WPE_ERR : int
        {
            OK = 0,  //!< No error
            ILLEGAL_BUFFER = -10000,//!< A buffer supplied is 0, or a buffer length to small
            JSON_PARSE = -10001,//!< Json parse error
            JSON_UNPACK = -10002,//!< Json unpack error
            SERVER_ERROR = -10003,//!< Web server error
            CURL_ERROR = -10004,//!< Error returned by the curl library
            NO_NET_ADAPTER = -10005,//!< No network adapters found
            ILLEGAL_PARAM = -10006,//!< Illegal parameter
            BASE64_ENCODE = -10007,//!< Error during base64 encoding
            FORCE_IP = -10008,//!< Error force IP
            NET_ADAPTER = -10009,//!< Error getting network adapters
            JSON_CREATE = -10010,//!< Json creation error
            PROPSTORE = -10011  //!< Error while using the PropertyStore
        }


        /**
         * The device info
         *
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        struct deviceInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string device_version;             //!< The device version

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string spec_version;               //!< GigE vision spec version

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string manufacturer_name;          //!< The manufactures name

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            string model_name;                 //!< The model name

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string serial_number;              //!< The serial number

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            string manufacturer_specific;      //!< Some manufacture info

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string user_name;                  //!< Device specific
        }


        /**
         * The network information
         *
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        struct networkInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string ip;        //!< The IP (v4) address as string

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string mask;      //!< The IP mask as string

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string broadcast; //!< The IP broadcast as string

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            string mac;       //!< The MAC address as string    
        }


        /**
        * Information about a single discovery Reply network packet
        *
*/
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        struct discoveryReplyMsg
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string remote_ip;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string local_ip;
        }


        /**
        * The original discovery reply
        *
*/
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        struct discoveryReply
        {
            deviceInfo deviceInfo; //!< The device information
            networkInfo lanInfo;   //!< The LAN network setup
            networkInfo wlanInfo;  //!< The WLAN network setup

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string gvcp_ip;             //!< Which IP address is used for image transfer
        }


        /**
        * The extended discovery reply
        * Can carry additional information
*/
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        struct discoveryReplyEx
        {
            deviceInfo deviceInfo;   //!< The device information
            networkInfo lanInfo;     //!< The LAN network setup
            networkInfo wlanInfo;    //!< The WLAN network setup


            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            string gvcp_ip;               //!< Which IP address is used for image transfer

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            discoveryReplyMsg[] messages; //!< Info about the received reply packets
            uint messageCount;          //!< How many messages this reply carries
        };

        /**
        * Structure for holding the adapter
        * part of a configuration
        *
        *
*/

        
        // See: https://stackoverflow.com/questions/5235445/pinvoke-c-function-takes-pointer-to-function-as-argument

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EndFrameCallback(IntPtr pAcqDesc);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EndAcqCallback(IntPtr pAcqDesc);


        [DllImport("XISL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern HIS_RETURN Acquisition_SetCallbacksAndMessages(IntPtr pAcqDesc,
                      HandleRef hWnd,
                      uint dwErrorMsg, uint dwLoosingFramesMsg,
                      [MarshalAs(UnmanagedType.FunctionPtr)]EndFrameCallback lpfnEndFrameCallback,
                      [MarshalAs(UnmanagedType.FunctionPtr)]EndAcqCallback lpfnEndAcqCallback
                      );

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_EnumSensors(ref uint pdwNumSensors, bool bEnableIRQ, bool bAlwaysOpen);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_GetNextSensor(ref IntPtr Pos, IntPtr phAcqDesc);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_GetCommChannel(IntPtr pAcqDesc, ref uint pdwChannelType, ref int pnChannelNr);
        
        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_DefineDestBuffers(IntPtr pAcqDesc, IntPtr pProcessedData, uint nFrames, uint nRows, uint nColumns);
        

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_Acquire_Image(IntPtr pAcqDesc, uint dwFrames, uint dwSkipFrms, HIS_SEQ dwOpt, [In] ushort[] pwOffsetData, [In] uint[] pdwGainData, [In] int[] pdwPxlCorrList);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_Acquire_Image_Ex(IntPtr hAcqDesc, uint dwFrames, uint dwSkipFrms, uint dwOpt,
                                              ref ushort pwOffsetData, uint dwGainFrames, ref ushort pwGainData,
                                              ref ushort pwGainAvgData, ref uint pdwGainData, ref uint pdwPxlCorrList);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_Abort(IntPtr hAcqDesc);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_SetFPGACameraMode(IntPtr hAcqDesc, FPGAType FPGACommand, bool bInverse);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_SetCameraMode(IntPtr hAcqDesc, uint dwMode);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_Acquire_OffsetImage(IntPtr hAcqDesc, IntPtr pOffsetData, uint nRows, uint nCols, uint nFrames);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_Acquire_OffsetImage_Ex(IntPtr hAcqDesc, IntPtr pOffsetData, uint nRows, uint nCols, uint nFrames, uint dwOpt);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_Acquire_GainImage(IntPtr hAcqDesc, IntPtr pOffsetData, IntPtr pGainData, uint nRows, uint nCols, uint nFrames);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_Acquire_GainImage_Ex(IntPtr hAcqDesc, IntPtr pOffsetData, IntPtr pGainData, uint nRows, uint nCols, uint nFrames, uint dwOpt);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_CreateGainMap(ref ushort pGainData, ref ushort pGainAVG, int nCount, int nFrame);
        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_CreatePixelMap([In] ushort[] pData, uint nDataRows, uint nDataColumns, [In, Out] int[] pCorrList, ref uint nCorrListSize);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_DoOffsetCorrection(ushort* pSource, ushort* pDest, ushort* pOffsetData, int nCount);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_DoOffsetGainCorrection(ushort* pSource, ushort* pDest, ushort* pOffsetData, uint* pGainData, int nCount);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_DoOffsetGainCorrection_Ex(ushort* pSource, ushort* pDest, ushort* pOffsetData, ushort* pGainData,
        //                                                                    ushort* pGainAVG, int nCount, int nFrame);  //	19.09.02


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_DoOffsetCorrection32(unsigned long* pSource, unsigned long* pDest, unsigned long* pOffsetData, int nCount); // val 20070124

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_DoOffsetGainCorrection32(unsigned long* pSource, unsigned long* pDest, unsigned long* pOffsetData, unsigned long* pGainData, int nCount);// val 20070124

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_DoOffsetGainCorrection_Ex32(unsigned long* pSource, unsigned long* pDest, unsigned long* pOffsetData, unsigned long* pGainData,
        //                                    unsigned long* pGainAVG, int nCount, int nFrame); // val 20070124

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_CreateGainMap32(unsigned long* pGainData, unsigned long* pGainAVG, int nCount, int nFrame);



        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_DoPixelCorrection(ushort* pData, int* pCorrList);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_IsAcquiringData(IntPtr hAcqDesc);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_Close(IntPtr hAcqDesc);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_CloseAll();

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_SetReady(IntPtr hAcqDesc, bool ready);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_GetReady(IntPtr hAcqDesc);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetErrorCode(IntPtr hAcqDesc, uint* dwHISError, uint* dwBoardError);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_GetConfiguration(IntPtr hAcqDesc,
                            ref uint dwFrames, ref uint dwRows, ref uint dwColumns, ref uint dwDataType,
                            ref uint dwSortFlags, ref bool bIRQEnabled, ref ulong dwAcqType, ref ulong dwSystemID,
                            ref ulong dwSyncMode, ref ulong dwHwAccess);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_GetIntTimes(IntPtr hAcqDesc, ref double[] dblIntTime, ref int nIntTimes);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetWinHandle(IntPtr hAcqDesc, HWND* hWnd);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetActFrame(IntPtr hAcqDesc, uint* dwActAcqFrame, uint* dwActSecBuffFrame);
        //# ifdef XIS_OS_64

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetAcqData(IntPtr hAcqDesc, void* AcqData);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetAcqData(IntPtr hAcqDesc, void** VoidAcqData);
        //#else

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetAcqData(IntPtr hAcqDesc, uint dwAcqData);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetAcqData(IntPtr hAcqDesc, uint* dwAcqData);
        //#endif // XIS_OS_64

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetHwHeaderInfo(IntPtr hAcqDesc, CHwHeaderInfo* pInfo);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetFrameSync(IntPtr hAcqDesc);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetFrameSyncMode(IntPtr hAcqDesc, uint dwMode);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetTimerSync(IntPtr hAcqDesc, uint* dwCycleTime);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_AbortCurrentFrame(IntPtr hAcqDesc);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetCorrData(IntPtr hAcqDesc, unsigned short* pwOffsetData, uint* pdwGainData, uint* pdwPxlCorrList);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetCorrData_Ex(IntPtr hAcqDesc, unsigned short* pwOffsetData, unsigned short* pwGainData,
        //                                            unsigned short* pwGainAvgData, uint nGainFrames,
        //                                            uint* pdwGainData, uint* pdwPxlCorrList);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetCorrData(IntPtr hAcqDesc, unsigned short** ppwOffsetData, uint** ppdwGainData, uint** ppdwPxlCorrList);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetCorrData_Ex(IntPtr hAcqDesc, unsigned short** ppwOffsetData, unsigned short** ppwGainData,
        //                                            unsigned short** ppwGainAvgData, uint** nGainFrames, uint** pdwGainData, uint** pdwPxlCorrList);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_SetCameraGain(IntPtr hAcqDesc, XRD4343_Gain wMode);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetFrameSyncTimeMode(IntPtr hAcqDesc, unsigned int uiMode, unsigned int dwDelayTime);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Acquire_GainImage_Ex_ROI(IntPtr hAcqDesc, ushort* pOffsetData, uint* pGainData, uint nRows, uint nCols, uint nFrames, uint dwOpt, uint uiULX, uint uiULY, uint uiBRX, uint uiBRY, uint uiMode);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Acquire_Image_PreloadCorr(IntPtr hAcqDesc, uint dwFrames, uint dwSkipFrms, uint dwOpt);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Acquire_OffsetImage_PreloadCorr(IntPtr hAcqDesc, ushort* pwOffsetData, uint nRows, uint nColumns, uint nFrames, uint dwOpt);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetHwHeader(IntPtr hAcqDesc, byte* pData, unsigned int uiSize);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Acquire_GainImage_Ex_ROI_PreloadCorr(IntPtr hAcqDesc, uint* pGainData, uint nRows, uint nCols, uint nFrames, uint dwOpt, uint uiULX, uint uiULY, uint uiBRX, uint uiBRY, uint uiMode);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Acquire_GainImage_PreloadCorr(IntPtr hAcqDesc, uint* pGainData, uint nRows, uint nCols, uint nFrames);


        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_SetCameraBinningMode(IntPtr hAcqDesc, XRD4343_Binning wMode);

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_GetCameraBinningMode(IntPtr hAcqDesc, ref XRD4343_Binning wMode);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_ResetFrameCnt(IntPtr hAcqDesc);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetLatestFrameHeader(IntPtr hAcqDesc, CHwHeaderInfo* pInfo, CHwHeaderInfoEx* pInfoEx);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetHwHeaderInfoEx(IntPtr hAcqDesc, CHwHeaderInfo* pInfo, CHwHeaderInfoEx* pInfoEx);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetCameraTriggerMode(IntPtr hAcqDesc, ushort wMode);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetCameraTriggerMode(IntPtr hAcqDesc, ushort* wMode);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetRotationAngle(IntPtr hAcqDesc, long lRotAngle); // FG-E only can be -90 | 0 | 90

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetRotationAngle(IntPtr hAcqDesc, long* lRotAngle);



        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_Init(IntPtr* pIntPtr,
        //                                                            int nChannelNr,
        //                                                            bool bEnableIRQ,
        //                                                            uint uiRows, uint uiColumns,
        //                                                            bool bSelfInit, bool bAlwaysOpen,
        //                                                            long lInitType,
        //                                                            GBIF_STRING_DATATYPE* ucAddress
        //                                                          );


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_GetDeviceList(GBIF_DEVICE_PARAM* pGBIF_DEVICE_PARAM, int nDeviceCnt);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_GetDevice(GBIF_STRING_DATATYPE* ucAddress, uint dwAddressType, GBIF_DEVICE_PARAM* pDevice);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_GetDeviceCnt(long* plNrOfboards);

        //        //HIS_RETURN Acquisition_GbIF_UploadPKIFirmware(			GBIF_STRING_DATATYPE* cMacAddress,
        //        //													void* pBitStream, long lBitStreamLength
        //        //										);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_SetConnectionSettings(GBIF_STRING_DATATYPE* cMAC,
        //                                                            unsigned long ulBootOptions,
        //                                                            GBIF_STRING_DATATYPE* cDefIP,
        //                                                            GBIF_STRING_DATATYPE* cDefSubNetMask,
        //                                                            GBIF_STRING_DATATYPE* cStdGateway
        //                                                        );


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_GetConnectionSettings(GBIF_STRING_DATATYPE* ucMAC,
        //                                                            unsigned long* ulBootOptions,
        //                                                            GBIF_STRING_DATATYPE* ucDefIP,
        //                                                            GBIF_STRING_DATATYPE* ucDefSubNetMask,
        //                                                            GBIF_STRING_DATATYPE* ucStdGateway
        //                                                        );


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_GetPacketDelay(IntPtr hAcqDesc,
        //                                                            long* lPacketdelay
        //                                                        );


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_SetPacketDelay(IntPtr hAcqDesc,
        //                                                            long lPacketdelay
        //                                                    );


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_ForceIP(GBIF_STRING_DATATYPE* cMAC,
        //                                                            GBIF_STRING_DATATYPE* cDefIP,
        //                                                            GBIF_STRING_DATATYPE* cDefSubNetMask,
        //                                                            GBIF_STRING_DATATYPE* cStdGateway
        //                                            );


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_GetFilterDrvState(IntPtr hAcqDesc


        //                                                      );


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_CheckNetworkSpeed(IntPtr hAcqDesc, ushort* wTiming, long* lPacketDelay, long lMaxNetworkLoadPercent);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_GetDetectorProperties(IntPtr hAcqDesc, GBIF_Detector_Properties* pDetectorProperties);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_GetDeviceParams(IntPtr hAcqDesc, GBIF_DEVICE_PARAM* pDevice);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_GetVersion(int* pMajor, int* pMinor, int* pRelease, char* pStrVersion, int iStrLength);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_DiscoverDetectors();

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_DiscoveredDetectorCount(long* pDeviceCount);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_DiscoveredDetectorByIndex(long lIndex, GBIF_DEVICE_PARAM* pDevice);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GbIF_SetDiscoveryTimeout(long timeout);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_GetVersionNEW(int* major, int* minor, int* release, int* build);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acq_WPE_Init();


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_ForceIP(const char* macAddress, struct networkConfiguration* config, int port, int* isAnswered);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_ChangeNetworkConfig(const char* ipAddress, int configIndex, struct networkConfiguration * config);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_FillDefaultNetworkConfiguration(struct networkConfiguration * config);

        //// FTP specific

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_FTP_InitSession(IntPtr hAcqDesc, XislFtpSession* session);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_FTP_CloseSession(XislFtpSession session);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetMissedImageCount(XislFtpSession session, uint* count);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_OpenMissedImage(XislFtpSession session, uint index, XislFileHandle* fileHandle);

        //        // Common (multi-purpose) file handling

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetFileInfo(XislFileHandle fileHandle, XislFileInfo* fileInfo);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_LoadFile(XislFileHandle fileHandle, byte** buffer);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_DeleteFile(XislFileHandle fileHandle);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_CloseFile(XislFileHandle fileHandle);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acq_wpe_LoadCorrectionImageToBuffer(IntPtr hAcqDesc, const char* pccCorrectionFilePath, ProcScriptOperation Operation);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_AcknowledgeImage(IntPtr hAcqDesc, const char* tag);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_SetUniqueImageTag(IntPtr hAcqDesc, const char* imageTag);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Reset_OnboardOptions(IntPtr hAcqDesc);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Set_OnboardOptionsPostOffset(IntPtr hAcqDesc, bool bNoOnboardCorr, bool bSendPreviewFrist, bool bSendFULLFirst, bool bEnableAckFirst, bool bEnableAckSecond, bool bEnableOffsetFirst, bool bEnablePostOffsetCorr, bool bGain, bool bPixel);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Set_OnboardOptionsPostOffsetEx(IntPtr hAcqDesc, bool bNoOnboardCorr, bool bSendPreviewFrist, bool bSendFULLFirst, bool bEnableAckFirst, bool bEnableAckSecond, bool bEnableOffsetFirst, bool bEnablePostOffsetCorr, bool bGain, bool bPixel, bool bStoreOffsetToSD);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_SetMaxOnboardCorrValue(IntPtr hAcqDesc, unsigned short usMax, unsigned short usReplace);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Set_OnboardOffsetImageAcquisition(IntPtr hAcqDesc, bool bEnable, bool bSend, bool bStoreSD);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Set_OnboardOptions(IntPtr hAcqDesc, bool bStoreSD, bool bOffset, bool bGain, bool bPixel);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_ActivateServiceMode(IntPtr hAcqDesc, bool bActivate);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetCameraROI(IntPtr hAcqDesc, unsigned short usActivateGrp); // val 2010-05-12

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetCameraROI(IntPtr hAcqDesc, unsigned short* usActivateGrp); // val 2010-05-17


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetTriggerOutSignalOptions(IntPtr hAcqDesc, unsigned short usTiggerOutSignalMode,
        //                                                                            unsigned short usEP_SeqLength,
        //                                                                            unsigned short usEP_FirstBrightFrm,
        //                                                                            unsigned short usEP_LastBrightFrm,
        //                                                                            unsigned short usEP_Delay1,
        //                                                                            unsigned short usEP_Delay2,
        //                                                                            unsigned short usDDD_Delay,
        //                                                                            int iTriggerOnRisingEdgeEnable,
        //                                                                            int iSaveAsDefault
        //                                                                            ); // val 2010-05-12

        //        // mk 2013-04-19:

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_GetVersion(int* major, int* minor, int* release, int* build);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_getAvailableSystems(struct discoveryReply * reply, int* numDevices, int timeout,int port);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_GetNetworkConfigs(const char* ipAddress, struct networkConfiguration * configs, int* arrayLength, int* activeConfig);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_ActivateNetworkConfig(const char* ipAddress, int configIndex);


        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_GetErrorCode(void);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_wpe_GetErrorCodeEx(char* pBuffer, long len);

        //        // mk 2013-04-19 end

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetTriggerOutStatus(IntPtr hAcqDesc, int* iTriggerStatus); /// 2013-04-22 Val GetTriggerStatus GbIF 


        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_SetCameraFOVMode(IntPtr hAcqDesc,  XRD4343_FOV wMode); // 2013-07-03 val R&F Field Of View 

        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        public static extern HIS_RETURN Acquisition_GetCameraFOVMode(IntPtr hAcqDesc, ref XRD4343_FOV wMode); // 2013-07-03 val R&F Field Of View 

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //public static extern HIS_RETURN Acquisition_wpe_ReadCameraRegisters(const char* ipAddress, unsigned long* buffer);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //public static extern HIS_RETURN Acquisition_wpe_GetExamFlag(const char* ipAddress, unsigned long* pExamFlag);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acq_wpe_SystemControl(const char* ipAddress, XRpad_SystemControlEnum eAction);
        //[DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acq_wpe_SetImageTransferInterface(const char* ipAddress, XRpad_DataInterfaceControlEnum eInterface);
        //[DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acq_wpe_GetSystemInformation(const char* ipAddress, char* buffer, int bufferLen);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetFTPFile(const char* ipAddress, const char* filename, void** databuffer, long* filesize); //2013-07-17 mk
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_FreeFTPFileBuffer(void* databuffer); //2013-09-19 mv
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetFTPFile(const char* ipAddress, const char* filename, void* databuffer, long filesize); //2013-07-17 mk

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetVersion(int* major, int* minor, int* release, int* build);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetProvidedEnhancedFeatures(IntPtr hAcqDesc, unsigned int* uiEnhancesFeatures);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Set_OnboardOptionPreview(IntPtr hAcqDesc, bool bEnablePreview, bool bPreviewOptionSendFull, OnboardBinningMode eMode, unsigned int uiSelectedScript);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_IsPreviewImage(IntPtr hAcqDesc, unsigned int* uiIsPreview);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetPhototimedParams(IntPtr hAcqDesc, unsigned short usNrOfScrubs, unsigned short usMaxDelay);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_EnableLogging(bool onOff);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetLogLevel(XislLoggingLevels xislLogLvl);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetLogLevel(XislLoggingLevels* xislLogLvl);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_TogglePerformanceLogging(bool onOff);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetLogOutput(const char* filePath, bool consoleOnOff);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetFileLogging(const char* filename, bool enableLogging);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetConsoleLogging(bool enableConsole);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetXISFileBufferSize(size_t* pFileSize, uint dwRows, uint dwColumns, uint dwFrames, bool uiOnboardFileHeader, XIS_FileType filetype);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_CreateOnboardPixelMaskFrom16BitPixelMask(unsigned short* uspPixelMaskSrc, uint dwRows, uint dwColumns, byte* bpOnboardPixelMask);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_CreateXISFileInMemory(void* pMemoryFileBuffer, void* pDataBuffer, uint dwRows, uint dwColumns, uint dwFrames, bool uiOnboardFileHeader, XIS_FileType filetype);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SaveFile(const char* filename, void* pImageBuffer, uint dwRows, uint dwColumns, uint dwFrames, bool uiOnboardFileHeader, XIS_FileType usTypeOfNumbers);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SaveRawData(const char* filename, const byte* buffer, size_t bufferSize);
        //[DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_LoadXISFileToMemory(const char* filename, void* pMemoryFileBuffer, size_t bufferSize);

        //[DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetDACOffsetFloorValueByMode(IntPtr hAcqDesc, unsigned int uiMode);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetDACOffsetFloorValueInFlash(IntPtr hAcqDesc, unsigned int uiMode, ushort wValue);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetDACOffsetFloorValueFromFlash(IntPtr hAcqDesc, unsigned int uiMode, ushort* pwValue);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetDetectorProperties(IntPtr hAcqDesc, GBIF_Detector_Properties* pDetectorProperties);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetDACoffset(IntPtr hAcqDesc, ushort wDACoffsetValue);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetDACoffsetBinningFPS(IntPtr hAcqDesc, ushort wBinningMode, double dblFps, ushort* pwValueToFPGA);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Enable_EMI_Data_Readout(IntPtr hAcqDesc, unsigned int uiOnOff);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetGridSensorStatus(IntPtr hAcqDesc, unsigned int* uiStatus);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetConnectionStatus(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Set_FPGA_Power_Mode(IntPtr hAcqDesc, unsigned int uiMode);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetTailTimeforTriggerMode(IntPtr hAcqDesc, unsigned short usTailTime, XIS_DetectorTriggerMode eTriggerMode);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetEventCallback(IntPtr hAcqDesc, XIS_EventCallback EventCallback, void* userData);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_DisableEventCallback(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_ResetOnboardShockEvent(IntPtr hAcqDesc, unsigned int latestShock_Timestamp);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetSDCardForceFsck(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_AckSDCardForceFsck(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_AckSDCardForceFsckError(IntPtr hAcqDesc);

        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetSDCardInfo(IntPtr hAcqDesc, unsigned int* total, unsigned int* avail);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetFakeTemperature(IntPtr hAcqDesc, bool bEnableFakeMode, int iFakeTemperature);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_IdentifyDevice(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Resend_All_Messages(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetLocation(IntPtr hAcqDesc, unsigned int* location);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetNetwork(IntPtr hAcqDesc, unsigned int* network);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetNetworkSpeed(IntPtr hAcqDesc, unsigned int network);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetIdleTimeout(IntPtr hAcqDesc, unsigned short timeout);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetChargeMode(IntPtr hAcqDesc, byte charge_mode);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_VerifyGenuineness(IntPtr hAcqDesc, char (* msg)[128], size_t* msg_len, byte (* md)[20]);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetPrivateKey(IntPtr hAcqDesc, byte (* key_old)[64], byte (* key_new)[64]);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetTemperatureTimeout(IntPtr hAcqDesc, unsigned short timeout);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_ResetTemperatureTimeout(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetTemperatureThresholds(IntPtr hAcqDesc, unsigned int threshold_warning, unsigned int threshold_critical);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetTemperatureThresholds(IntPtr hAcqDesc, unsigned int* threshold_warning, unsigned int* threshold_critical);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetBatteryStatus(IntPtr hAcqDesc, XRpad_BatteryStatus* batteryStatus);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Get_Current_Voltage(IntPtr hAcqDesc, DETECTOR_CURRENT_VOLTAGE* pstructCurrentVoltage);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_CreateFakeShockWarningLevel(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_CreateFakeShockCriticalLevel(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_FactoryResetShock(IntPtr hAcqDesc);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetSystemTime(IntPtr hAcqDesc, char* cDateTime);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetPowerstate(IntPtr hAcqDesc, unsigned int* powerstate);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetAutoPowerOnLocations(IntPtr hAcqDesc, unsigned int* autopoweronlocations);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetAutoPowerOnLocations(IntPtr hAcqDesc, unsigned int autopoweronlocations);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetChargeMode(IntPtr hAcqDesc, byte* charge_mode_req, byte* charge_mode_charger);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_SetSDCardTimeout(IntPtr hAcqDesc, unsigned short sdcard_timeout);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetSDCardTimeout(IntPtr hAcqDesc, unsigned short* sdcard_timeout);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetVersionInfo(IntPtr hAcqDesc, XRpad_VersionInfo* versionInfo);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_GetIpAdress(IntPtr hAcqDesc, const char** ipAddress);
        //        [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
        //        public static extern HIS_RETURN Acquisition_Test_SDCardPerformance(IntPtr hAcqDesc, unsigned int buffersize,
        //                                                      double* wbitrate, unsigned int* wmicroseconds,
        //                                                      double* rbitrate, unsigned int* rmicroseconds);


        //    //error codes


        //    /** No error @ingroup enum **/
        //#define HIS_ALL_OK							0
        //    /** Memory couldn't be allocated. @ingroup enum **/
        //#define HIS_ERROR_MEMORY					1
        //    /** Unable to initialize board. @ingroup enum **/
        //#define HIS_ERROR_BOARDINIT					2
        //    /** Got a time out. May be no detector present. @ingroup enum **/
        //#define HIS_ERROR_NOCAMERA					3
        //    /** Your correction files do not have a proper size. @ingroup enum **/
        //#define HIS_ERROR_CORRBUFFER_INCOMPATIBLE	4
        //    /** Acquisition is already running. @ingroup enum **/
        //#define HIS_ERROR_ACQ_ALREADY_RUNNING		5
        //    /** Got a time out from hardware. @ingroup enum **/
        //#define HIS_ERROR_TIMEOUT					6
        //    /** Acquisition descriptor invalid. @ingroup enum **/
        //#define HIS_ERROR_INVALIDACQDESC			7
        //    /** Unable to find VxD. @ingroup enum **/
        //#define HIS_ERROR_VXDNOTFOUND				8
        //    /** Unable to open VxD. @ingroup enum **/
        //#define HIS_ERROR_VXDNOTOPEN				9
        //    /** Unknown error during VxD loading. @ingroup enum **/
        //#define HIS_ERROR_VXDUNKNOWNERROR			10
        //    /** VxD Error: GetDmaAddr failed. @ingroup enum **/
        //#define HIS_ERROR_VXDGETDMAADR				11
        //    /** An unexpected acquisition abort occurred. @ingroup enum **/
        //#define HIS_ERROR_ACQABORT					12
        //    /** error occurred during data acquisition. @ingroup enum **/
        //#define HIS_ERROR_ACQUISITION				13
        //    /** Unable to register interrupt. @ingroup enum **/
        //#define HIS_ERROR_VXD_REGISTER_IRQ			14
        //    /** Register status address failed. @ingroup enum **/
        //#define HIS_ERROR_VXD_REGISTER_STATADR		15
        //    /** Getting version of operating system failed. @ingroup enum **/
        //#define HIS_ERROR_GETOSVERSION				16
        //    /** Can not set frame sync. @ingroup enum **/
        //#define HIS_ERROR_SETFRMSYNC				17
        //    /** Can not set frame sync mode. @ingroup enum **/
        //#define HIS_ERROR_SETFRMSYNCMODE			18
        //    /** Can not set timer sync. @ingroup enum **/
        //#define HIS_ERROR_SETTIMERSYNC				19
        //    /** Invalid function call. @ingroup enum **/
        //#define HIS_ERROR_INVALID_FUNC_CALL			20
        //    /** Aborting current frame failed. @ingroup enum **/
        //#define HIS_ERROR_ABORTCURRFRAME			21
        //    /** Getting hardware header failed. @ingroup enum **/
        //#define HIS_ERROR_GETHWHEADERINFO			22
        //    /** Hardware header is invalid. @ingroup enum **/
        //#define HIS_ERROR_HWHEADER_INV				23
        //    /** Setting line trigger mode failed. @ingroup enum **/
        //#define HIS_ERROR_SETLINETRIG_MODE			24
        //    /** Writing data failed. @ingroup enum **/
        //#define HIS_ERROR_WRITE_DATA				25
        //    /** Reading data failed. @ingroup enum **/
        //#define HIS_ERROR_READ_DATA					26
        //    /** Setting baud rate failed. @ingroup enum **/
        //#define HIS_ERROR_SETBAUDRATE				27
        //    /** No acquisition descriptor available. @ingroup enum **/
        //#define HIS_ERROR_NODESC_AVAILABLE			28
        //    /** Buffer space not sufficient. @ingroup enum **/
        //#define HIS_ERROR_BUFFERSPACE_NOT_SUFF		29
        //    /** Setting detector mode failed. @ingroup enum **/
        //#define HIS_ERROR_SETCAMERAMODE				30
        //    /** Frame invalid. @ingroup enum **/
        //#define HIS_ERROR_FRAME_INV					31
        //    /** System to slow. @ingroup enum **/
        //#define HIS_ERROR_SLOW_SYSTEM				32
        //    /** Error during getting number of boards. @ingroup enum **/
        //#define HIS_ERROR_GET_NUM_BOARDS			33
        //    /** Communication channel already opened by another process. @ingroup enum **/
        //#define HIS_ERROR_HW_ALREADY_OPEN_BY_ANOTHER_PROCESS	34
        //    /** Error creating memory mapped file. @ingroup enum **/
        //#define HIS_ERROR_CREATE_MEMORYMAPPING				35
        //    /** Error registering DMA address. @ingroup enum **/
        //#define HIS_ERROR_VXD_REGISTER_DMA_ADDRESS			36
        //    /** Error registering static address. @ingroup enum **/
        //#define HIS_ERROR_VXD_REGISTER_STAT_ADDR			37
        //    /** Unable to unmask interrupt. @ingroup enum **/
        //#define HIS_ERROR_VXD_UNMASK_IRQ					38
        //    /** Unable to load driver. @ingroup enum **/
        //#define HIS_ERROR_LOADDRIVER						39
        //    /** Function is not implemented. @ingroup enum **/
        //#define HIS_ERROR_FUNC_NOTIMPL						40
        //    /** Unable to create memory mapping. @ingroup enum **/
        //#define HIS_ERROR_MEMORY_MAPPING					41
        //    /** Could not create Mutex. @ingroup enum **/
        //#define HIS_ERROR_CREATE_MUTEX						42
        //    /** Error starting the acquisition. @ingroup enum **/
        //#define HIS_ERROR_ACQ								43
        //    /** Acquisition descriptor is not local. @ingroup enum **/
        //#define HIS_ERROR_DESC_NOT_LOCAL					44
        //    /** Invalid Parameter. @ingroup enum **/
        //#define HIS_ERROR_INVALID_PARAM						45
        //    /** Error during abort acquisition function. @ingroup enum **/
        //#define HIS_ERROR_ABORT								46
        //    /** The wrong board is selected. @ingroup enum **/
        //#define HIS_ERROR_WRONGBOARDSELECT					47
        //    /** Change of Detector Mode during Acquisition. @ingroup enum **/
        //#define HIS_ERROR_WRONG_CAMERA_MODE					48	 
        //    /** The number of images for frame grabber onboard averaging must be 2 to the power of n. @ingroup enum **/
        //#define HIS_ERROR_AVERAGED_LOST						49	 
        //    /** Parameter for (onboard) sorting not valid. @ingroup enum **/
        //#define HIS_ERROR_BAD_SORTING_PARAM					50	
        //    /** Connection to Network Detector cannot be opened due to invalid IP address / MAC / Detector name. @ingroup enum **/
        //#define HIS_ERROR_UNKNOWN_IP_MAC_NAME				51	
        //    /** Detector could not be found in the Subnet. @ingroup enum **/
        //#define HIS_ERROR_NO_BOARD_IN_SUBNET				52
        //    /** Unable to open connection to Network Detector. @ingroup enum **/
        //#define HIS_ERROR_UNABLE_TO_OPEN_BOARD				53
        //    /** Unable to close connection to Network Detector. @ingroup enum **/
        //#define HIS_ERROR_UNABLE_TO_CLOSE_BOARD				54
        //    /** Unable to access the flash memory of Detector. @ingroup enum **/
        //#define HIS_ERROR_UNABLE_TO_ACCESS_DETECTOR_FLASH	55
        //    /** No frame header received from Detector. @ingroup enum **/
        //#define HIS_ERROR_HEADER_TIMEOUT					56
        //    /** Command not acknowledged. @ingroup enum **/
        //#define HIS_ERROR_NO_FPGA_ACK						57
        //    /** Number of boards within network changed during broadcast. @ingroup enum **/
        //#define HIS_ERROR_NR_OF_BOARDS_CHANGED				58
        //    /** Unable to set the exam flag. @ingroup enum **/
        //#define HIS_ERROR_SETEXAMFLAG						59
        //    /** Error Function called with an illegal index number. @ingroup enum **/
        //#define HIS_ERROR_ILLEGAL_INDEX                     60
        //    /** Error Function or function environment not correctly initialised. @ingroup enum **/
        //#define HIS_ERROR_NOT_INITIALIZED                   61
        //    /** Error No detectors discovered yet. @ingroup enum **/
        //#define HIS_ERROR_NOT_DISCOVERED                    62
        //    /** Error onbaord averaging failed. @ingroup enum **/
        //#define HIS_ERROR_ONBOARDAVGFAILED                  63
        //    /** Error getting onboard offset. @ingroup enum **/
        //#define HIS_ERROR_GET_ONBOARD_OFFSET				64
        //    /** Error CURL. @ingroup enum **/
        //#define HIS_ERROR_CURL                              65
        //    /** Error setting onboard offset corr mode. @ingroup enum **/
        //#define HIS_ERROR_ENABLE_ONBOARD_OFFSET				66
        //    /** Error setting onboard mean corr mode. @ingroup enum **/
        //#define HIS_ERROR_ENABLE_ONBOARD_MEAN				67
        //    /** Error setting onboard gain corr mode. @ingroup enum **/
        //#define HIS_ERROR_ENABLE_ONBOARD_GAINOFFSET			68
        //    /** Error setting onboard preview mode. @ingroup enum **/
        //#define HIS_ERROR_ENABLE_ONBOARD_PREVIEW			69
        //    /** Error setting onboard binning mode. @ingroup enum **/
        //#define HIS_ERROR_SET_ONBOARD_BINNING				70
        //    /** Error Loading image from SD to onboard buffer. @ingroup enum **/
        //#define HIS_ERROR_LOAD_COORECTIONIMAGETOBUFFER		71
        //    /** Error Invalid pointer/buffer passed as parameter. @ingroup enum **/
        //#define HIS_ERROR_INVALIDBUFFERNR					72
        //    /** Error Invalid SHOCKID. @ingroup enum **/
        //#define HIS_ERROR_INVALID_HANDLE                    73
        //    /** Error Invalid filename, file already exists. @ingroup enum **/
        //#define HIS_ERROR_ALREADY_EXISTS                    74
        //    /** Error Invalid filename type does not exist. @ingroup enum **/
        //#define HIS_ERROR_DOES_NOT_EXIST                    75
        //    /** Error Invalid filename for image tag or log file. @ingroup enum **/
        //#define HIS_ERROR_OPEN_FILE                         76
        //    /** Error Invalid filename for image tag or log file. @ingroup enum **/
        //#define HIS_ERROR_INVALID_FILENAME                  77
        //    /** Error setting gbif discovery timeout. @ingroup enum **/
        //#define HIS_ERROR_SETDISCOVERYTIMEOUT               78
        //    // DEXELA
        //#define HIS_ERROR_SERIALREAD						100
        //    // DEXELA
        //#define HIS_ERROR_SERIALWRITE						101
        //    // 1313
        //#define HIS_ERROR_SETDAC							102
        //    // 1313
        //#define HIS_ERROR_SETADC							103
        //    /** Error setting the onboard image tag. @ingroup enum **/
        //#define HIS_ERROR_SET_IMAGE_TAG						104
        //    /** Error setting the onboard process script. @ingroup enum **/
        //#define HIS_ERROR_SET_PROC_SCRIPT					105
        //    /** Error Image tag length exceeded 128char (including path: autosave/). @ingroup enum **/
        //#define HIS_ERROR_SET_IMAGE_TAG_LENGTH				106
        //    /** Error retrieving the enhanced header. @ingroup enum **/
        //#define HIS_ERROR_RETRIEVE_ENHANCED_HEADER			107
        //    /** Error enabling XRPD interrupts. @ingroup enum **/
        //#define HIS_ERROR_ENABLE_INTERRUPTS                 108
        //    /** Error XRPD session Error. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SESSION_ERROR                109
        //    /** Error No interface to communicate event messages active. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_EVENT                    110
        //    /** Error No interface to communicate event messages active. @ingroup enum **/
        //#define HIS_ERROR_XRPD_NO_EVENT_INTERFACE           111
        //    /** Error creating fake shock events. @ingroup enum **/
        //#define HIS_ERROR_XRPD_CREATE_FAKE_SHOCK_EVENT      112
        //    /** Error retrieving the sd card info. @ingroup enum **/
        //#define HIS_ERROR_XRPD_GET_SDCARD_INFO              113
        //    /** Error activating the fake temperatur mode on the detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_TEMP_FAKE_MODE           114
        //    /** Error the requested EMI readout mode was not reported by the detector. @ingroup enum **/
        //#define HIS_ERROR_EMI_NOT_SET                       115
        //    /** Error retrieving the location info from the detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_NO_LOCATION                  116
        //    /** Error setting the on detector idle timeout. @ingroup enum **/
        //#define HIS_ERROR_SET_IDLE_TIMEOUT                  117
        //    /** Error setting the software requested charge mode. @ingroup enum **/
        //#define HIS_ERROR_SET_CHARGE_MODE                   118
        //    /** Error creating critical level fake shock events. @ingroup enum **/
        //#define HIS_ERROR_XRPD_CREATE_FAKE_SHOCK_EVENT_CRIT 119
        //    /** Error creating warning level fake shock events. @ingroup enum **/
        //#define HIS_ERROR_XRPD_CREATE_FAKE_SHOCK_EVENT_WARN 120
        //    /** Error resetting the shock events to factory values. @ingroup enum **/
        //#define HIS_ERROR_XRPD_FACTORY_RESET_SHOCK_EVENT    121
        //    /** Error getting the on detector LAN network speed. @ingroup enum **/
        //#define HIS_ERROR_XRPD_NO_NETWORK                   122
        //    /** Error setting the on detector LAN network speed. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_NETWORK                  123
        //    /** Error verifying the private key for genuiness. @ingroup enum **/
        //#define HIS_ERROR_XRPD_VERIFY_GENUINENESS           124
        //    /** Error setting the private key for genuiness. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_PRIVATE_KEY              125
        //    /** Error setting the temperature timeout. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_TEMPERATURE_TIMEOUT      126
        //    /** Error resetting the temperature timeout counter. @ingroup enum **/
        //#define HIS_ERROR_XRPD_RESET_TEMPERATURE_TIMEOUT    127
        //    /** Error setting the temperature thresholds on the detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_TEMPERATURE_THRESHOLDS   128
        //    /** Error getting the temperature thresholds from the detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_GET_TEMPERATURE_THRESHOLDS   129
        //    /** Error no eventcallback defined for irq messages. @ingroup enum **/
        //#define HIS_ERROR_XRPD_NO_EVENTCALLBACK_DEFINED     130
        //    /** Error setting on detectors date and time. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_DATE_TIME                131
        //    /** Error triggering the resend of all current messages by the XRPD. @ingroup enum **/
        //#define HIS_ERROR_XRPD_RESEND_ALL_MSG               132
        //    /** Error acknowledging the image. @ingroup enum **/
        //#define HIS_ERROR_ACKNOWLEDGE_IMAGE                 133
        //    /** Error connecting to the on detector XRPD process. @ingroup enum **/
        //#define HIS_ERROR_XRPD_CONNECT                      134
        //    /** Error resetting the shock event. @ingroup enum **/
        //#define HIS_ERROR_XRPD_RESET_SHOCK                  135
        //    /** Error setting the power state on the detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_REQUEST_POWERSTATE           136
        //    /** Error retrieving the auto power on locations from the detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_GET_AUTOPOWERONLOCATIONS     137
        //    /** Error setting the auto power on locations on the detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_AUTOPOWERONLOCATIONS     138
        //    /** Error retrieving the requested charge mode from the detector. @ingroup enum **/
        //#define HIS_ERROR_GET_CHARGE_MODE                   139
        //    /** Error requesting an fscheck on next boot. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_FORCE_FSCK               140
        //    /** Error setting the sd card timeout on the detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_SDCARD_TIMEOUT           141
        //    /** Error getting the sd card timeout from the detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_GET_SDCARD_TIMEOUT           142
        //    /** Error not connected to on detector XRPD process. @ingroup enum **/
        //#define HIS_ERROR_MISSING_VERSION_INFORMATION       143
        //    /** Error not connected to on detector XRPD process. @ingroup enum **/
        //#define HIS_ERROR_XRPD_NOT_CONNECTED                144
        //    /** Error retrieving the SD card performance. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SDCARDPERFORMANCE            145
        //    /** Requested channel is already openend. @ingroup enum **/
        //#define HIS_ERROR_HW_BOARD_CHANNEL_ALREADY_USED		146
        //    /** Error retrieving the Voltage or Current from detector. @ingroup enum **/
        //#define HIS_ERROR_XRPD_GET_CURRENT_VOLTAGE			147
        //    /** Unable to set on detector CPU govenor. @ingroup enum **/
        //#define HIS_ERROR_XRPD_SET_CPUFREQ_GOVERNOR         148


        //    //sort definitions

        //#define HIS_SORT_NOSORT						0
        //#define HIS_SORT_QUAD						1
        //#define HIS_SORT_COLUMN						2
        //#define HIS_SORT_COLUMNQUAD					3
        //#define HIS_SORT_QUAD_INVERSE				4
        //#define HIS_SORT_QUAD_TILE					5
        //#define HIS_SORT_QUAD_TILE_INVERSE			6
        //#define HIS_SORT_QUAD_TILE_INVERSE_SCRAMBLE	7
        //#define HIS_SORT_OCT_TILE_INVERSE			8		//	1640 and 1620
        //#define HIS_SORT_OCT_TILE_INVERSE_BINDING	9		//	1680
        //#define HIS_SORT_OCT_TILE_INVERSE_DOUBLE	10		//	1620 reverse
        //#define HIS_SORT_HEX_TILE_INVERSE			11		//	1621 ADIC
        //#define HIS_SORT_HEX_CS						12		//	1620/1640 continous scan
        //#define HIS_SORT_12x1						13		//	12X1 Combo
        //#define HIS_SORT_14							14		//	
        //#define HIS_SORT_TOP_BOTTOM					15		//	2013-07-01 val 1717 RNF  full lines top row bottom row


        //sequence acquisition options
        public enum HIS_SEQ : uint
        {
            TWO_BUFFERS		=	0x1   ,
            ONE_BUFFER			=	0x2   ,
            AVERAGE				=	0x4   ,
            DEST_ONE_FRAME		=	0x8   ,
            COLLATE				=	0x10  ,
            CONTINUOUS			=	0x100,		
            LEAKAGE				=	0x1000,
            NONLINEAR			=	0x2000,
            AVERAGESEQ			=	0x4000,	// sequence of averaged frames
            PREVIEW				=	0x8000
        }




        //#define HIS_SYNCMODE_SOFT_TRIGGER			1
        //#define HIS_SYNCMODE_INTERNAL_TIMER			2
        //#define HIS_SYNCMODE_EXTERNAL_TRIGGER		3
        //#define HIS_SYNCMODE_FREE_RUNNING			4
        //#define HIS_SYNCMODE_AUTO_TRIGGER			8
        //#define HIS_SYNCMODE_EXTERNAL_TRIGGER_FG	16

        //#define HIS_CAMMODE_SETSYNC		0x8
        //#define HIS_CAMMODE_TIMEMASK	0x7
        //#define HIS_CAMMODE_FPGA		0x7F


        //#define HIS_MAX_TIMINGS							0x8


        //    /// detector supported options
        //#define XIS_DETECTOR_SERVICE_MODE_AVAILABLE		0x1 
        //#define XIS_DETECTOR_TRIGGER_SOURCE_SELECTABLE	0x2
        //#define XIS_DETECTOR_SUPPORTS_PING				0x3
        //#define XIS_DETECTOR_SUPPORTED_ROI_MODES		0x4
        //#define XIS_DETECTOR_SUPPORTED_BINNING_MODES	0x5
        //#define XIS_DETECTOR_SUPPORTS_GAIN_CHANGE		0x6
        //#define XIS_DETECTOR_SUPPORTS_MULTIPLE_TRIGGER_MODES	0x7
        //#define XIS_DETECTOR_SUPPORTS_CONFIGURABLE_TRIGGER_OUT	0x8
        //#define XIS_DETECTOR_GRPSIZE_ROI_Y				0x9
        //#define XIS_DETECTOR_LIVEBUFFERSIZE				0xA
        //#define XIS_DETECTOR_CMD_EXECUTION_DELAY		0xB
        //#define XIS_DETECTOR_AUTO_TRIGGER_SELECTABLE	0xC
        //#define XIS_DETECTOR_SUPPORTED_FOV_MODES		0xD
        //#define XIS_DETECTOR_SUPPORTS_EXAM_FLAG			0xE
        //#define XIS_DETECTOR_SUPPORTS_IMAGE_TAG			0xF
        //#define XIS_DETECTOR_SUPPORTS_PROC_SCRIPT		0x10
        //#define XIS_DETECTOR_SUPPORTS_ENHANCED_FEATURES	0x11
        //#define XIS_DETECTOR_SUPPORTS_EMI				0x12


        //    //Grps 1&2&3&4, 3&4, 2&3, 1&2 ,4, 3, 2, 1
        //#define XIS_DETECTOR_PROVIDES_ROI_GRP_1			0x1
        //#define XIS_DETECTOR_PROVIDES_ROI_GRP_2			0x2
        //#define XIS_DETECTOR_PROVIDES_ROI_GRP_3			0x4
        //#define XIS_DETECTOR_PROVIDES_ROI_GRP_4			0x8
        //#define XIS_DETECTOR_PROVIDES_ROI_GRP_5			0x10 // val 2011-11-14 1642
        //#define XIS_DETECTOR_PROVIDES_ROI_GRP_6			0x20 // val 2011-11-14 1642
        //#define XIS_DETECTOR_PROVIDES_ROI_GRP_7			0x40 // val 2011-11-14 1642
        //#define XIS_DETECTOR_PROVIDES_ROI_GRP_8			0x80 // val 2011-11-14 1642

        //#define XIS_DETECTOR_PROVIDES_ROI_1_GRP			0x1
        //#define XIS_DETECTOR_PROVIDES_ROI_2_GRPS		0x2
        //#define XIS_DETECTOR_PROVIDES_ROI_3_GRPS		0x4
        //#define XIS_DETECTOR_PROVIDES_ROI_4_GRPS		0x8
        //#define XIS_DETECTOR_PROVIDES_ROI_ALL_GRPS		0x1000

        //#define XIS_DETECTOR_PROVIDES_FOV_1				0x10	// 2013-07-03 val R&F Field Of View
        //#define XIS_DETECTOR_PROVIDES_FOV_2				0x20	// 2013-07-03 val R&F Field Of View 2
        //#define XIS_DETECTOR_PROVIDES_FOV_3				0x40	// 2013-07-03 val R&F Field Of View 3
        //#define XIS_DETECTOR_PROVIDES_FOV_4				0x80	// 2013-07-03 val R&F Field Of View 4
        //#define XIS_DETECTOR_PROVIDES_FOV_5				0x100	// 2013-07-03 val R&F Field Of View 5


        //    /*
        //    #define XIS_DETECTOR_PROVIDES_ROI_GRP_1_2		0x10
        //    #define XIS_DETECTOR_PROVIDES_ROI_GRP_2_3		0x20
        //    #define XIS_DETECTOR_PROVIDES_ROI_GRP_3_4		0x40
        //    #define XIS_DETECTOR_PROVIDES_ROI_GRP_1_2_3_4	0x80
        //    */
        //    // BINNING MODES

        public enum DETECTOR_BINNING : uint
        {
            BINNING_1x1		=0x1,
            BINNING_2x2		=0x2,
            BINNING_4x4		=0x4,
            BINNING_1x2		=0x8,
            BINNING_1x4		=0x10
        }





        //#define XIS_DETECTOR_PROVIDES_BINNING_3x3		0x20
        //#define XIS_DETECTOR_PROVIDES_BINNING_9to4		0x40

        //#define XIS_DETECTOR_PROVIDES_BINNING_AVG		0x100
        //#define XIS_DETECTOR_PROVIDES_BINNING_SUM		0x200
        //    // AVG bit 8
        //    // SUM bit 9
        //    /******/

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
            HIS_ERROR_XRPD_SET_CPUFREQ_GOVERNOR = 148
        }
        public enum HIS_BOARD_TYPE : uint
    {
        NOONE = 0x0,
        ELTEC = 0x1,
        DIPIX = 0x2,
        RS232 = 0x3,
        USB = 0x4,
        ELTEC_XRD_FGX = 0x8,
        ELTEC_XRD_FGE_Opto = 0x10,
        ELTEC_GbIF = 0x20,
        ELTEC_WPE = 0x40,    // mk 2013-04-16 additional functions for wpe lib
        ELTEC_EMBEDDED = 0x60,   // mk 2013-04-16 embedded is gbif and wpe
        CMOS = 0x100,                                // msi 2013-06-20 CMOS are all devices with CMOS FW (until now 1512, 13x13) / => HIS_BOARD_TYPE_DEXELA_1512 | define HIS_BOARD_TYPE_ELTEC_13x13
        ELTEC_13x13 = 0x320, // msi 2013-06-21 13x13 includes gbif and CMOS / => 0x200 | HIS_BOARD_TYPE_ELTEC_GbIF | HIS_BOARD_TYPE_CMOS
        DEXELA_1512CL = 0x500,   // msi 2013-06-21 13x13 includes and CMOS / => 0x400 | HIS_BOARD_TYPE_CMOS
    }

    [DllImport("XISL.dll", CharSet = CharSet.Ansi)]
    public static extern HIS_RETURN Acquisition_Init(ref IntPtr phAcqDesc,
                  HIS_BOARD_TYPE dwChannelType, int nChannelNr,
                  bool bEnableIRQ,
                  uint Rows, uint Columns,
                  uint dwSortFlags,
                  bool bSelfInit, bool bAlwaysOpen
                  );
    }

    public enum XRD4343_Gain : uint
    {
        eADU_29 = 1,
        eADU_57 = 2,
        eADU_114_default = 3,
        eADU_229 = 4,
        eADU_457 = 5,
        eADU_686 = 6,
        eADU_914 = 7,
    }

    public enum XRD4343_Binning : uint
    {
        b1x1 = 1,
        b2x2 = 2,
        b3x3 = 3
    }

    public enum XRD4343_FOV : ushort
    {
        f432x432mm2 = 1,
        f288x288mm2 = 2,
        f216x216mm2 = 3,
        f432x216mm2 = 4,
        f432x72mm2 = 5
    }
}

