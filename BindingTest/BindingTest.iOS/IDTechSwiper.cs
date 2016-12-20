using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using BindingTest.iOS;
using BindingTest.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(IDTechSwiper))]

namespace BindingTest.iOS
{
    public class IDTechSwiper : IIDTechSwiper
    {
        private const string TAG = nameof(IDTechSwiper);

        public IDTechSwiper()
        {

        }

        private IDTechSwiperBindingIOS.uniMag _reader;

		private List<NSObject> _observers = new List<NSObject>();

        public bool Initialize()
        {
            if (_reader != null)
            {
                _reader = null;
            }
            System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");

            _reader = new IDTechSwiperBindingIOS.uniMag();

            if (_reader == null)
                return false;

            _reader.SetAutoConnect(false);

			if(_observers.Count > 0)
				NSNotificationCenter.DefaultCenter.RemoveObservers(_observers);

			_observers.Clear();

				//Examples of receiving events back from the SDK
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagInvalidSwipeNotification"), OnReceivedInvalidSwipeNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagSwipeNotification"), OnReceivedSwipeNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagDidConnectNotification"), OnReceivedDidConnectNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagTimeoutNotification"), OnReceivedTimeoutNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagPoweringNotification"), OnReceivedPoweringNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagDidReceiveDataNotification"), OnReceivedDataNotification));

			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagDidDisconnectNotification"), OnReceivedDidDisconnectNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagAttachmentNotification"), OnReceivedAttachmentNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagDetachmentNotification"), OnReceivedDetachmentNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagInsufficientPowerNotification"), OnReceivedInsufficientPowerNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagMonoAudioErrorNotification"), OnReceivedAudioErrorNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagDataProcessingNotification"), OnReceivedDataProcessingNotification));
			_observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagSystemMessageNotification"), OnReceivedSystemMessageNotification));



            ((App)App.Current).AddLogMessage("Initialized SDK");

            return true;
        }

		void OnReceivedDidDisconnectNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Device Disconnected: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}

		void OnReceivedAttachmentNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Device Attached: " + obj?.Object?.ToString());
			((App)App.Current).AddLogMessage("Calling StartUniMag...");

			IDTechSwiperBindingIOS.UmRet ret = _reader.StartUniMag(true);
			((App)App.Current).AddLogMessage("StartUniMag - Returned: " + ret.ToString());
		}

		void OnReceivedDetachmentNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Device Detached: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}

		void OnReceivedInsufficientPowerNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("OnReceivedInsufficientPowerNotification: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}

		void OnReceivedAudioErrorNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("OnReceivedAudioErrorNotification: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}

		void OnReceivedDataProcessingNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Processing Card Swipe Data (Please Wait): " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}
		void OnReceivedSystemMessageNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("OnReceivedSystemMessageNotification: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}


        void OnReceivedDataNotification(NSNotification obj)
        {
			((App)App.Current).AddLogMessage("Received Card Swipe Data: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
        }

		void OnReceivedInvalidSwipeNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("INVALID SWIPE, PLEASE TRY AGAIN: " + obj.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}

		void OnReceivedSwipeNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Received Notification To Swipe Card: " + obj?.Object?.ToString());
			//((App)App.Current).HideLoadingDialog();
			((App)App.Current).ShowLoadingDialog("Please swipe card now");

		}

		void OnReceivedDidConnectNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Device Connected: " + obj?.Object?.ToString());
		}

		void OnReceivedTimeoutNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("OPERATION TIMED OUT: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}

		void OnReceivedPoweringNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Device Powering Up: " + obj?.Object?.ToString());
		}





        public bool StartSwipeCard()
        {



            IDTechSwiperBindingIOS.UmRet ret = _reader.RequestSwipe();
			((App)App.Current).AddLogMessage("RequestSwipe - Returned: " + ret.ToString());
			if (ret == IDTechSwiperBindingIOS.UmRet.Success)
				return true;
			else
				return false;
        }

        public bool GetUserGrant(int type, string strMessage)
        {
            ((App)App.Current).AddLogMessage("GetUserGrant: " + strMessage);
            return true;
        }
        
        private string GetConfigurationFileFromRaw()
        {
            return GetXMLFileFromRaw("idt_unimagcfg_default.xml");
        }
        private string GetAutoConfigProfileFileFromRaw()
        {
            //share the same copy with the configuration file
            return GetXMLFileFromRaw("idt_unimagcfg_default.xml");
        }

        // If 'idt_unimagcfg_default.xml' file is found in the 'raw' folder, it returns the file path.
        private string GetXMLFileFromRaw(String fileName)
        {
            //the target filename in the application path
            string fileNameWithPath = null;
            fileNameWithPath = fileName;

            try
            {


            }
            catch (Exception e)
            {
                //e.PrintStackTrace();
                fileNameWithPath = null;
            }
            return fileNameWithPath;
        }
    }
}