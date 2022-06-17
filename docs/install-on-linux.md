# Install Vocup on Linux

> As of Vocup 1.9, Linux is not supported anymore due to no users. If you want to use the latest version on Linux, you have to build Vocup yourself.

Wine is the recommended way to use Vocup on Linux

1. Follow the instructions at [WineHQ Wiki](https://wiki.winehq.org/Download) to install Wine
2. Copy the _Microsoft Sans Serif_ font from a Windows computer (C:\Windows\Fonts\micross.ttf)
3. Download an installer of Vocup from the [releases page](https://github.com/daniel-lerch/vocup/releases)
4. Install Vocup with `wine Vocup_X.Y.Z_Mono.exe`

Tested with

- Vocup 1.8.6
- Wine 7.0.0
- Ubuntu 20.04 LTS

Using Mono will not work anymore because Vocup uses .NET 6.0 self contained deployment.
