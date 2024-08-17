## NetPinProc
---

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)  [![netpinproc](https://github.com/FlippingFlips/NetPinProc/actions/workflows/netpinproc.release-nuget.yml/badge.svg)](https://github.com/FlippingFlips/NetPinProc/actions/workflows/netpinproc.release-nuget.yml)

Dotnet `net6.0`;`net7.0` wrapper implementation for the `libpinproc` ProcDevice. This is for interfacing with a P-ROC or P3-ROC controller board over USB.

This resides in it's own library because it depends on the `libpinproc` native library and has no game. The `NetPinProc.Game` uses this in which you can use through that

- Windows, Mac, Linux included.

Libpinproc builds are included and can be used from the `lib/x64` or `lib/x86`.

### Sources
---

* #### PinProc.cs

PinProc, a C# wrapper class for libpinproc. Basically a set of P/Invoke methods and structures to make it pretty easy to call libpinproc functions from .Net. Note that this requires that libpinproc be available as a DLL (on Windows) or a dylib on Mac or Linux.

* #### ProcDevice.cs

ProcDevice, a wrapper class for PinProc, which makes controlling the P-ROC hardware even smoother in a .Net environment. Although it is far from complete, it is a useful as an example of how to call PinProc's methods. If you improve upon it we would be happy to incorporate your changes.

* [libpinproc](https://github.com/preble/libpinproc)

* [libpinproc-builds](https://github.com/FlippingFlips/libpinproc)

## NetPinProc
---

The initial netpinproc version was started on 10/31/2010 and was written by Adam Preble.

[preble/netpinproc](https://github.com/preble/netpinproc)

Other versions used and written by Jimmy Lipham:

[compy/NetPinProcgame](https://github.com/Compy/NetPinProcGame)

## Dependencies
---

[NetPinProc.Domain](https://github.com/FlippingFlips/NetPinProc.Domain/pkgs/nuget/NetPinProc.Domain)

## Licenses
---

See `licenses` directory for other licenses.

[License](../LICENSE.md)
