using System.Runtime.InteropServices;
using Foundation;
using UIKit;

namespace IDTechSwiperBindingIOS
{
	public enum UmReader : uint
	{
		Unknown,
		UnimagOriginal,
		UnimagPro,
		UnimagIi,
		Shuttle
	}

	public enum UmTask : uint
	{
		None,
		Connect,
		Swipe,
		Cmd,
		FwUpdate
	}

	public enum UmRet : uint
	{
		Success,
		NoReader,
		SdkBusy,
		MonoAudio,
		AlreadyConnected,
		LowVolume,
		NotConnected,
		NotApplicable,
		InvalidArg,
		UfInvalidStr,
		UfNoFile,
		UfInvalidFile
	}

	public enum UmUfCode : uint
	{
		SendingBlock = 21,
		VerifyingChecksum = 30,
		ResendingBlock = 40,
		FailedToEnterBootloaderMode = 303,
		FailedToSendBlock = 305,
		FailedToVerifyChecksum = 306,
		Canceled = 307
	}

}
