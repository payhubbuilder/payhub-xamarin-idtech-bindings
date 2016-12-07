using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BindingTest
{
    public partial class App : Application
    {


        public App()
        {
            InitializeComponent();

            MainPage = new BindingTest.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void AddLogMessage(string msg)
        {
            ((MainPage)MainPage).AddLog(msg);
        }

        public void ShowLoadingDialog(string msg)
        {
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading(title:msg, maskType:Acr.UserDialogs.MaskType.Black);
        }

        public void HideLoadingDialog()
        {
            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
        }
    }
}
