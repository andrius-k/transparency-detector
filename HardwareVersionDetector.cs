using System;
using ObjCRuntime;
using System.Runtime.InteropServices;

namespace Transparency
{
	public static class HardwareVersionDetector
	{
		private const string HardwareProperty = "hw.machine";

		[DllImport(Constants.SystemLibrary)]
		internal static extern int sysctlbyname([MarshalAs(UnmanagedType.LPStr)] string property,
			IntPtr output,
			IntPtr oldLen,
			IntPtr newp,
			uint newlen
		);

		public static string HardwareVersion
		{
			get
			{
				try 
				{
					// get the length of the string
					var pLen = Marshal.AllocHGlobal(sizeof(int));
					sysctlbyname(HardwareProperty, IntPtr.Zero, pLen, IntPtr.Zero, 0);

					var length = Marshal.ReadInt32(pLen);

					// check to see if we got a length
					if (length == 0)
					{
						Marshal.FreeHGlobal(pLen);
						return "Unknown";
					}

					// get the hardware string
					var pStr = Marshal.AllocHGlobal(length);
					sysctlbyname(HardwareProperty, pStr, pLen, IntPtr.Zero, 0);

					// convert the native string into a C# string
					var hardwareStr = Marshal.PtrToStringAnsi(pStr);

					// cleanup
					Marshal.FreeHGlobal(pLen);
					Marshal.FreeHGlobal(pStr);

					return hardwareStr;
				} 
				catch (Exception ex) 
				{
					Console.WriteLine("HardwareVersionDetector.HardwareVersion Ex: " + ex.Message);
				}

				return "Unknown";
			}
		}
	}
}

