# Vocup Documentation

## Development

Florian Amstutz developed Vocup as student not knowing much about programming.
The software includes a rich set of features implemented in just a few classes, half in English, half in German.

Cleaning this code will not be finished soon.
All German names and comments have to be translated in English.
New code should make use of object orientated programming and identifier should be written in [CamelCase](https://msdn.microsoft.com/en-us/library/x2dbyw72%28v=vs.71%29.aspx?f=255&MSPPError=-2147217396).
Control names should begin upper case.

## File formats

See [File formats](fileformats.md)

## Internet services

Instead of designing an own infrastructure for releases and updates, Vocup will rely on the Microsoft Store and GitHub APIs.
The UWP version will not use any internet services at all. Other versions search for new releases using the GitHub API.

## Build procedure

### Artifacts
- `Vocup_1.7.2.appxbundle` UWP packaged version of Vocup (Windows 10 only)
- `Vocup_1.7.2.exe` Classic installer for Vocup (Windows 7+)
- `Vocup_1.7.2.zip` Portable version of Vocup (Windows 7+)
- `Vocup_1.7.2_Mono.exe` Classic installer for Vocup on Mono (Wine)
- `Vocup_1.7.2_Mono.tar.gz` Portable version of Vocup for Mono (Linux)

### Mono builds
Because of strange bugs of the Mono runtime the `Vocup.exe.config` file must be changed after .NET's build.
Like @AtjonTV pointed out in his fork, the following lines have to be removed:
```xml
<System.Windows.Forms.ApplicationConfigurationSection>
  <add key="DpiAwareness" value="PerMonitorV2" />
</System.Windows.Forms.ApplicationConfigurationSection>
```
In addition to that the whole blocks `configSections` and `userSettings` have to be removed like described [here](https://stackoverflow.com/a/37351698/7075733).
