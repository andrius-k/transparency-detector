using System;
using Foundation;
using UIKit;

namespace Transparency
{
	public static class TransparencyDetector
	{
		private static int iOSVersionMajor = UIDevice.CurrentDevice.SystemVersion[0] - '0'; // Convert char to int

		public static bool IsTransparencyAvailable (bool defaultValue = true) // defaultValue is used when running on simulator
		{
			//There is no way to determine device on simulator. Return defaultValue instead
			if(ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.SIMULATOR)
			{
				return defaultValue;
			}

			// Blur effect is only available on iOS 7 and above.
			if(iOSVersionMajor < 7)
			{
				return false;
			}

			if(iOSVersionMajor >= 8)
			{
				// Check if Reduce Transparency option is enabled in Settings.
				// This feature is available since iOS 7 but this property exists only in iOS 8 and up. Yes, strange :)
				if (UIAccessibility.IsReduceTransparencyEnabled)
				{
					return false;
				}
			}

			//Lastly we check if device is one of the supported devices by Apple.
			return DoesDeviceSupportTransparency();
		}


		private static bool DoesDeviceSupportTransparency()
		{
			string[] unsupportedDevices = 
			{	"Unknown",
				"iPad",
				"iPad1,1",
				"iPhone1,1",
				"iPhone1,2",
				"iPhone2,1",
				"iPhone3,1",
				"iPhone3,2",
				"iPhone3,3",
				"iPod1,1",
				"iPod2,1",
				"iPod2,2",
				"iPod3,1",
				"iPod4,1",
				"iPad2,1",
				"iPad2,2",
				"iPad2,3",
				"iPad2,4",
				"iPad3,1",
				"iPad3,2",
				"iPad3,3" };

			int index = Array.IndexOf(unsupportedDevices, HardwareVersionDetector.HardwareVersion);

			return (index == -1);
		}
	}
}

