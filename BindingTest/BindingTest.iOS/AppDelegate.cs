using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using HockeyApp.iOS;

namespace BindingTest.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;;
			var manager = BITHockeyManager.SharedHockeyManager;
			manager.LogLevel = BITLogLevel.Debug;
			manager.Configure("5a3f6903a7a047a2b6505441b1661e37");
			manager.StartManager();
			manager.Authenticator.AuthenticateInstallation(); // This line is obsolete in crash only builds

			HockeyApp.MetricsManager.TrackEvent("FinishedLaunching called");

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			HockeyApp.MetricsManager.TrackEvent("Native Exception Caught: " + e.ToString());

		}
	}
}
