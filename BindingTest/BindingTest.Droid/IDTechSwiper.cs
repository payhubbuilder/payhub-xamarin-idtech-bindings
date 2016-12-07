using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using IDTech.MSR.UniMag;

using BindingTest.Interfaces;
using IDTech.MSR.XMLManager;
using BindingTest.Droid;
using Android.Content.Res;
using System.IO;
using Android.Util;

[assembly: Xamarin.Forms.Dependency(typeof(IDTechSwiper))]

namespace BindingTest.Droid
{
    public class IDTechSwiper : Java.Lang.Object, IUniMagReaderMsg, IIDTechSwiper
    {
        private const string TAG = nameof(IDTechSwiper);

        public IDTechSwiper()
        {

        }

        private UniMagReader _reader;

        public bool Initialize()
        {
            if(_reader != null)
            {
                _reader.UnregisterListen();
                _reader.Release();
                _reader = null;
            }

            _reader = new UniMagReader(this, Xamarin.Forms.Forms.Context, IDTech.MSR.UniMag.UniMagReader.ReaderType.UmOrPro);

            if (_reader == null)
                return false;

            _reader.SetVerboseLoggingEnable(false);

            string fileNameWithPath = GetConfigurationFileFromRaw();
            _reader.SetXMLFileNameWithPath(fileNameWithPath);
            if(!_reader.LoadingConfigurationXMLFile(true))
            {
                return true;
            }
            _reader.RegisterListen();

            return true;
        }

        public bool StartSwipeCard()
        {
            return _reader.StartSwipeCard();
        }

        public bool GetUserGrant(int type, string strMessage)
        {
            ((App)App.Current).AddLogMessage("GetUserGrant: " + strMessage);
            return true;
        }

        public void OnReceiveMsgAutoConfigCompleted(StructConfigParameters profile)
        {
            ((App)App.Current).AddLogMessage("OnReceiveMsgAutoConfigCompleted: " + profile.ToString());
        }

        public void OnReceiveMsgAutoConfigProgress(int progressValue)
        {
            ((App)App.Current).AddLogMessage("OnReceiveMsgAutoConfigProgress: " + progressValue.ToString());
        }

        public void OnReceiveMsgAutoConfigProgress(int percent, double result, string profileName)
        {
            ((App)App.Current).AddLogMessage("OnReceiveMsgAutoConfigProgress: " + percent.ToString());
        }

        public void OnReceiveMsgCardData(sbyte flagOfCardData, byte[] cardData)
        {
            ((App)App.Current).HideLoadingDialog();
            ((App)App.Current).AddLogMessage("OnReceiveMsgCardData: flagOfCardData - " + flagOfCardData.ToString());

            if (cardData.Length > 5)
            {
                if (cardData[0] == 0x25 && cardData[1] == 0x45)
                {
                    ((App)App.Current).AddLogMessage("OnReceiveMsgCardData: Swipe error. Please try again.");
                    return;
                }
            }

            byte flag = (byte)(flagOfCardData & 0x04);
            if (flag == 0x00)
            {
                string msg = String.Concat(Array.ConvertAll(cardData, x => x.ToString("X2")));
                ((App)App.Current).AddLogMessage("OnReceiveMsgCardData: Card Data - " + msg);
            }
            if (flag == 0x04)
            {
                //You need to decrypt the data here first.
                string msg = String.Concat(Array.ConvertAll(cardData, x => x.ToString("X2")));
                ((App)App.Current).AddLogMessage("OnReceiveMsgCardData: Card Data Encrypted - " + msg);
            }
        }

        public void OnReceiveMsgCommandResult(int commandID, byte[] cmdReturn)
        {
            ((App)App.Current).AddLogMessage("OnReceiveMsgCommandResult: " + commandID.ToString());
        }

        public void OnReceiveMsgConnected()
        {
            ((App)App.Current).HideLoadingDialog();
            ((App)App.Current).AddLogMessage("Swiper is now connected.");
        }

        public void OnReceiveMsgDisconnected()
        {
            ((App)App.Current).AddLogMessage("OnReceiveMsgDisconnected");
        }

        public void OnReceiveMsgFailureInfo(int index, string strMessage)
        {
            ((App)App.Current).HideLoadingDialog();
            ((App)App.Current).AddLogMessage("OnReceiveMsgFailureInfo: " + index.ToString() + ", " + strMessage);
        }

        public void OnReceiveMsgProcessingCardData()
        {
            ((App)App.Current).AddLogMessage("Card data is being processed. Please wait.");
        }

        public void OnReceiveMsgSDCardDFailed(string p0)
        {
            ((App)App.Current).AddLogMessage("OnReceiveMsgSDCardDFailed");
        }

        public void OnReceiveMsgTimeout(string strTimeoutMsg)
        {
            ((App)App.Current).HideLoadingDialog();
            ((App)App.Current).AddLogMessage("OnReceiveMsgTimeout: " + strTimeoutMsg);
        }

        public void OnReceiveMsgToCalibrateReader()
        {
            ((App)App.Current).AddLogMessage("OnReceiveMsgToCalibrateReader");
            //throw new NotImplementedException();
        }

        public void OnReceiveMsgToConnect()
        {
            ((App)App.Current).AddLogMessage("Attempting to connect to Swiper...");
            ((App)App.Current).ShowLoadingDialog("Connecting to Swiper");

        }

        public void OnReceiveMsgToSwipeCard()
        {
            ((App)App.Current).AddLogMessage("OnReceiveMsgToSwipeCard");
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

        private string GetXMLFileFromRaw(String fileName)
        {
            //the target filename in the application path
            string fileNameWithPath = null;
            fileNameWithPath = fileName;

            try
            {
                StreamReader input = new StreamReader(Application.Context.Assets.Open("idt_unimagcfg_default.xml"));
                string i = input.ReadToEnd();
                input.Close();

                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder

                fileNameWithPath = path + '/' + fileNameWithPath;
                StreamWriter output = File.CreateText(fileNameWithPath);
                output.Write(i);
                output.Close();
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