# Install Vocup on Linux

## 1. Using Wine

Wine is the recommended way to use Vocup on Linux

1. Follow the instructions at [WineHQ Wiki](https://wiki.winehq.org/Download) to install Wine
2. Download the latest verison of Wine-Mono from [Wine Download Server](https://dl.winehq.org/wine/wine-mono/)
3. Install Wine-Mono with `wine msiexec /i wine-mono-X.Y.Z-x86.msi`
4. Open registry editor with `wine regedit`
5. Add a key `HKEY_CURRENT_USER\Environment\MONO_TLS_PROVIDER` with `btls` as value
6. Download a Mono optimized installer of Vocup from the [releases page](https://github.com/daniel-lerch/vocup/releases)
7. Install Vocup with `wine Vocup_X.Y.Z_Mono.exe`

Tested with

- Vocup 1.7.2
- Wine 5.0.1
- Wine-Mono 5.1.0
- Ubuntu 20.04 LTS

## 2. Using Mono

If you don't want to use Wine to run Vocup on Linux you can directly run it with Mono:

1. Follow the instructions at [mono-project.com](https://www.mono-project.com/download/stable/#download-lin-ubuntu) to configure packet sources
2. Install dependencies with  
`sudo apt install mono-runtime libmono-system-windows-forms4.0-cil libmono-system-io-compression4.0-cil libmono-system-io-compression-filesystem4.0-cil libcanberra-gtk-module`
3. Download Mono optimized executables of Vocup from the [releases page](https://github.com/daniel-lerch/vocup/releases)
4. Start Vocup from command prompt with `mono Vocup.exe`

Known issues

- Layout bugs
- Missing icons
- Update notification not working

Tested with

- Vocup 1.7.2
- Mono 6.8.0
- Ubuntu 20.04 LTS

These steps might also work for other distributions. Thanks @AtjonTV for the first Mono optimized release!
