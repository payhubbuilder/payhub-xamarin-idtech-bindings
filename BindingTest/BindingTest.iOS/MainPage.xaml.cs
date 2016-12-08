﻿using BindingTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BindingTest
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<LogEntry> logs = new ObservableCollection<LogEntry>();

        public MainPage()
        {
            InitializeComponent();
            lvLogs.ItemsSource = logs;
            lvLogs.HasUnevenRows = true;
            lvLogs.RowHeight = -1;
        }

        protected override void OnAppearing()
        {
        }

        private void SwipeCard_Clicked(object sender, EventArgs e)
        {
            bool ret = DependencyService.Get<IIDTechSwiper>().StartSwipeCard();
            if(!ret)
            {
                AddLog("StartSwipeCard Failed");
            }
            else
            {
                ((App)App.Current).AddLogMessage("StartSwipeCard Successful");
                ((App)App.Current).ShowLoadingDialog("Please swipe card now");
            }
        }

        private void Initialize_Clicked(object sender, EventArgs e)
        {
            bool ret = DependencyService.Get<IIDTechSwiper>().Initialize();
            if (!ret)
            {
                AddLog("Initialize Failed");
            }
            else
            {
                ((App)App.Current).AddLogMessage("Initialize Successful");
            }
        }
        

        public void AddLog(string msg)
        {
            LogEntry l = new LogEntry() { Message = msg };
            logs.Add(l);
        }
    }
}
