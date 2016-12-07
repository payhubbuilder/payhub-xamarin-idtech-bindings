using System;
using ObjCRuntime;

[assembly: LinkWith("IDTECH_UniMag.a", LinkTarget.ArmV6 | LinkTarget.ArmV7 | LinkTarget.Arm64 | LinkTarget.Simulator, ForceLoad = true, Frameworks = "AVFoundation AudioToolbox MediaPlayer ExternalAccessory MessageUI Foundation")]
