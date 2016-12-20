using BindingTest.Interfaces;
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

			logs.Add(new LogEntry() { Message = "Hit Initialize Button (Swipe can be plugged in or NOT plugged in for this step" });
			logs.Add(new LogEntry() { Message = "If swiper is NOT plugged in, plug it in" });
			logs.Add(new LogEntry() { Message = "After receiving \"Device Connected\" message, hit \"Swipe Card\"" });
			logs.Add(new LogEntry() { Message = "Dialog will appear saying please swipe now" });
			logs.Add(new LogEntry() { Message = "Swipe the card, and you should see swipe data returned in HEX" });
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

		private void ClearLogs_Clicked(object sender, EventArgs e)
		{
			logs.Clear();
		}

        public void AddLog(string msg)
        {
            LogEntry l = new LogEntry() { Message = msg };
            logs.Add(l);
        }
    }
}
