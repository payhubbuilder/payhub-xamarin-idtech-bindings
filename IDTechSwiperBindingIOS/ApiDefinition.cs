using Foundation;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ObjCRuntime;

namespace IDTechSwiperBindingIOS
{
    // @interface uniMag : NSObject
    [BaseType(typeof(NSObject))]
    interface uniMag
    {
        // +(NSString *)SDK_version;
        [Static]
        [Export("SDK_version")]
        string SDK_version { get; }

        // -(BOOL)isReaderAttached;
        [Export("isReaderAttached")]
        bool IsReaderAttached { get; }

        // -(BOOL)getConnectionStatus;
        [Export("getConnectionStatus")]
        bool ConnectionStatus { get; }

        // -(UmTask)getRunningTask;
        [Export("getRunningTask")]
        UmTask RunningTask { get; }

        // -(float)getVolumeLevel;
        [Export("getVolumeLevel")]
        float VolumeLevel { get; }

        // @property (nonatomic) UmReader readerType;
        [Export("readerType", ArgumentSemantic.Assign)]
        UmReader ReaderType { get; set; }

        // -(void)setAutoConnect:(BOOL)autoConnect;
        [Export("setAutoConnect:")]
        void SetAutoConnect(bool autoConnect);

        // -(BOOL)setSwipeTimeoutDuration:(NSInteger)seconds;
        [Export("setSwipeTimeoutDuration:")]
        bool SetSwipeTimeoutDuration(nint seconds);

        // -(void)setAutoAdjustVolume:(BOOL)b;
        [Export("setAutoAdjustVolume:")]
        void SetAutoAdjustVolume(bool b);

        // -(void)setDeferredActivateAudioSession:(BOOL)b;
        [Export("setDeferredActivateAudioSession:")]
        void SetDeferredActivateAudioSession(bool b);

        // -(void)cancelTask;
        [Export("cancelTask")]
        void CancelTask();

        // -(UmRet)startUniMag:(BOOL)start;
        [Export("startUniMag:")]
        UmRet StartUniMag(bool start);

		// -(UmRet)requestSwipe;
		[Export("requestSwipe")]
		UmRet RequestSwipe();

        // -(NSData *)getFlagByte;
        [Export("getFlagByte")]
        NSData FlagByte { get; }

        // -(UmRet)sendCommandGetVersion;
        [Export("sendCommandGetVersion")]
        UmRet SendCommandGetVersion { get; }

        // -(UmRet)sendCommandGetSettings;
        [Export("sendCommandGetSettings")]
        UmRet SendCommandGetSettings { get; }

        // -(UmRet)sendCommandEnableTDES;
        [Export("sendCommandEnableTDES")]
        UmRet SendCommandEnableTDES { get; }

        // -(UmRet)sendCommandEnableAES;
        [Export("sendCommandEnableAES")]
        UmRet SendCommandEnableAES { get; }

        // -(UmRet)sendCommandDefaultGeneralSettings;
        [Export("sendCommandDefaultGeneralSettings")]
        UmRet SendCommandDefaultGeneralSettings { get; }

        // -(UmRet)sendCommandGetSerialNumber;
        [Export("sendCommandGetSerialNumber")]
        UmRet SendCommandGetSerialNumber { get; }

        // -(UmRet)sendCommandGetNextKSN;
        [Export("sendCommandGetNextKSN")]
        UmRet SendCommandGetNextKSN { get; }

        // -(UmRet)sendCommandEnableErrNotification;
        [Export("sendCommandEnableErrNotification")]
        UmRet SendCommandEnableErrNotification { get; }

        // -(UmRet)sendCommandDisableErrNotification;
        [Export("sendCommandDisableErrNotification")]
        UmRet SendCommandDisableErrNotification { get; }

        // -(UmRet)sendCommandEnableExpDate;
        [Export("sendCommandEnableExpDate")]
        UmRet SendCommandEnableExpDate { get; }

        // -(UmRet)sendCommandDisableExpDate;
        [Export("sendCommandDisableExpDate")]
        UmRet SendCommandDisableExpDate { get; }

        // -(UmRet)sendCommandEnableForceEncryption;
        [Export("sendCommandEnableForceEncryption")]
        UmRet SendCommandEnableForceEncryption { get; }

        // -(UmRet)sendCommandDisableForceEncryption;
        [Export("sendCommandDisableForceEncryption")]
        UmRet SendCommandDisableForceEncryption { get; }

        // -(UmRet)sendCommandSetPrePAN:(NSInteger)prePAN;
        [Export("sendCommandSetPrePAN")]
		UmRet SendCommandSetPrePAN { get; }

        // -(UmRet)sendCommandClearBuffer;
        [Export("sendCommandClearBuffer")]
        UmRet SendCommandClearBuffer { get; }

        // -(UmRet)sendCommandResetBaudRate;
        [Export("sendCommandResetBaudRate")]
        UmRet SendCommandResetBaudRate { get; }

        // -(UmRet)sendCommandCustom:(NSData *)cmd;
        [Export("sendCommandCustom:")]
        UmRet SendCommandCustom(NSData cmd);

        // -(UmRet)getAuthentication;
        [Export("getAuthentication")]
        UmRet Authentication { get; }

        // -(BOOL)setFirmwareFile:(NSString *)location;
        [Export("setFirmwareFile:")]
        bool SetFirmwareFile(string location);

        // -(UmRet)updateFirmware:(NSString *)encrytedBytes;
        [Export("updateFirmware:")]
        UmRet UpdateFirmware(string encrytedBytes);

        // -(UmRet)updateFirmware2:(NSString *)string withFile:(NSString *)path;
        [Export("updateFirmware2:withFile:")]
        UmRet UpdateFirmware2(string @string, string path);

        // +(void)enableLogging:(BOOL)enable;
        [Static]
        [Export("enableLogging:")]
        void EnableLogging(bool enable);

        // -(NSData *)getWave;
        [Export("getWave")]
        NSData Wave { get; }

        // -(BOOL)setWavePath:(NSString *)path;
        [Export("setWavePath:")]
        bool SetWavePath(string path);

        // -(void)autoDetect:(BOOL)autoDetect;
        [Export("autoDetect:")]
        void AutoDetect(bool autoDetect);

        // -(void)promptForConnection:(BOOL)prompt;
        [Export("promptForConnection:")]
        void PromptForConnection(bool prompt);

        // -(UmRet)proceedPoweringUp:(BOOL)proceedPowerUp;
        [Export("proceedPoweringUp:")]
        UmRet ProceedPoweringUp(bool proceedPowerUp);

        // -(void)closeConnection;
        [Export("closeConnection")]
        void CloseConnection();

        // -(void)cancelSwipe;
        [Export("cancelSwipe")]
        void CancelSwipe();

        // -(BOOL)setCmdTimeoutDuration:(NSInteger)seconds;
        [Export("setCmdTimeoutDuration:")]
        bool SetCmdTimeoutDuration(nint seconds);


    }
}
