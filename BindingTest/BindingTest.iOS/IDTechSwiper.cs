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
            

            //Examples of receiving events back from the SDK
            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagDidReceiveDataNotification"), OnReceivedDataNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagInvalidSwipeNotification"), OnReceivedInvalidSwipeNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagSwipeNotification"), OnReceivedSwipeNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagDidConnectNotification"), OnReceivedDidConnectNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagTimeoutNotification"), OnReceivedTimeoutNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(new NSString("uniMagPoweringNotification"), OnReceivedPoweringNotification);

            ((App)App.Current).AddLogMessage("Initialized swiper");

            return true;
        }

        void OnReceivedDataNotification(NSNotification obj)
        {
			((App)App.Current).AddLogMessage("Card Data Notification Received: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
        }

		void OnReceivedInvalidSwipeNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Invalid Swipe Notification Received: " + obj.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}

		void OnReceivedSwipeNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Swipe Notification Received: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}

		void OnReceivedDidConnectNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Did Connect Notification Received: " + obj?.Object?.ToString());
		}

		void OnReceivedTimeoutNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Timeout Notification Received: " + obj?.Object?.ToString());
			((App)App.Current).HideLoadingDialog();
		}

		void OnReceivedPoweringNotification(NSNotification obj)
		{
			((App)App.Current).AddLogMessage("Powering Notification Received: " + obj?.Object?.ToString());
		}





        public bool StartSwipeCard()
        {

            IDTechSwiperBindingIOS.UmRet ret = _reader.StartUniMag(true);
            ret = _reader.RequestSwipe();
            ((App)App.Current).AddLogMessage("Started RequestSwipe");
            return true;
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