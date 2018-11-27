# Vocup internet services

## Use cases
* Provide a direct download option for classical Win32 installations
* Show update notifications for Linux platforms
* General information messages

## Reference

Endpoint: https://vectordata.de/vocup/v1/

Example:

````json
{
    "Messages": [
        {
			"Id": "327f4678-a1a4-4cc1-8bbe-d183fafcd1e7",
			"Text": ""
		},
		{
			"Id": "ab8e6412-4be2-4bd2-8376-a364e98e86a4",
			"Text": ""
		}
	]
	"Releases": [
		{
			"Platform": "UWP",
			"Version": "1.6.0",
			"MessageId": "327f4678-a1a4-4cc1-8bbe-d183fafcd1e7"
		},
		{
			"Platform": "Win32",
			"Version": "1.6.0",
			"MessageId": "327f4678-a1a4-4cc1-8bbe-d183fafcd1e7",
			"Mirror": "https://github.com/daniel-lerch/vocup/releases/download/v1.6.0/Vocup_1.6.0.exe"
		},
		{
			"Platform": "Mono",
			"Version": "1.6.1",
			"MessageId": "ab8e6412-4be2-4bd2-8376-a364e98e86a4"
		}
	]
}
```