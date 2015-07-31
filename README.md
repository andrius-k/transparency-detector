# transparency-detector
Xamarin.iOS class that checks if iOS device supports transparency effect. 

It takes into account iOS version, hardware version and Reduce Transparency option in iOS setting.

# Usage

```c#
if(TransparencyDetector.IsTransparencyAvailable ())
{
  // Transparency Available
}
else
{
  // Transparency NOT Available
}
```
