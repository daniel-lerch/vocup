# Vocup Privacy Statement

## Data that Vocup collects

Vocup collects the following information for analytics:

- Anonymized IP address
- Random Session ID
- Usage statistics about specific features

This information is used to find out how our software is being used and to improve the application.
We process your data on an own server and will not share it with any other company.

Your vocabulary books are only processed locally on your computer and never shared with us.

### Technical details

We use a self hosted instance of the excellent [Matomo](https://matomo.org/) analytics software.
It is configured to remove the last byte of all IP addresses in order to anonymize them but still allow basic Geo IP locations.

The Session ID is computed from a persistent random device identifier and the current date using an SHA256 hash truncated to the first 128bits.
When users decides to have their data deleted they must submit their device identifier so that we can reconstruct all Session IDs and delete the respective data.

Events that are tracked for usage statistics:

- Application start and close
- Practice start and end
- Vocabulary book creation, merge or print
- CSV import and export
- Special char keyboard open

## Your rights

You have the right to be informed of Personal Data processed by Vocup, a right to rectification/correction, erasure and restriction of processing.
You also have the right to ask from us a structured, common and machine-readable format of Personal Data you provided to us.

Vocup is legally represented by:

Daniel Lerch  
Rebschulweg 6  
64646 Heppenheim

Requests can be sent via post or via email. You will the address in the about window of Vocup.

## Third party services

### Microsoft Store

Vocup is distributed via the Microsoft Store which can collect crash reports and usage statistics to share them with the developers.
You configure this behaviour in the Windows settings. Vocup is not responsible for data that Microsoft collects.

### GitHub Releases

Vocup is also available as a .exe installer or portable .zip archive. These variants check for updates on every application start.
Your IP address and the version number of your Vocup app are transmitted to GitHub Inc. for this purpose.
