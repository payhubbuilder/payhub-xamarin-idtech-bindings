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
            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("iMagDidConnectNotification"), OnConnect);
            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("iMagDidReceiveDataNotification"), OnReceivedDataNotification);

            ((App)App.Current).AddLogMessage("Initialized swiper");

            return true;
        }

        void OnConnect(NSNotification obj)
        {
            ((App)App.Current).AddLogMessage("Swiper Connected");
        }

        void OnReceivedDataNotification(NSNotification obj)
        {
            ((App)App.Current).AddLogMessage("Card Data Received");
        }

        public bool StartSwipeCard()
        {

            IDTechSwiperBindingIOS.UmRet ret = _reader.StartUniMag(true);
            IDTechSwiperBindingIOS.UmRet ret = _reader.RequestSwipe();
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