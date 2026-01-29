# Vocup technical architecture

## UI Framework

Vocup was originally built with Windows Forms which does not meet today's requirements anymore.
After a major modernization of the WinForms codebase in 2018 I did not find a suitable replacement for the UI framework.

Until 2024 many frameworks made significant progress.
Avalonia and Uno Platform were the most promising frameworks with their unique strengths and weaknesses.
I decided to go with **Avalonia** because of its simpler architecture and more widespread adoption, namely  Git Credential Manager, Icons8 Lunacy, Plastic SCM, and dotTrace.

<details>
<summary>2024 Framework comparison</summary>

See this [amazing article](https://github.com/robloo/PublicDocs/blob/master/XAMLFrameworkComparison.md)
by robloo for an in-depth comparison of Avalonia, Uno Platform, and .NET MAUI.

#### Avalonia
➕ Cross platform  
➕ Partial modernization  
➕ Adapted by at least some major apps  
➖ Only a UI framework  
➖ No native webview  

#### Uno Platform
➕ Cross platform  
➕ Windows Community Toolkit compatibility  
➕ Native rendering of primitive controls  
➖ No significant adopters  
➖ Complex architecture

#### .NET MAUI
➕ Native look and feel  
➖ No Linux support  
➖ No browser support  
➖ No significant adopters  

#### Blazor
➕ Cross platform  
➕ Partial modernization  
➖ Complex interop with component libraries  
➖ Data binding is buggy  

### Not considered
- WinUI 3
- WPF
- Windows Forms
</details>
<br>

During the development of the first MVP in summer 2025, I noticed a lot of bugs and missing features for Android.
Frustrated about Avalonia, I started an experiment with **Flutter** which I had not considered before.

After initial exicitement, I found out that Flutter was missing a lot of features for desktop apps.
In late 2025 it was not even possible to change the text of window from Dart code, not to speak about window positioning, etc.
There were also no prominent adopters of Flutter on desktop like there were for Avalonia.
Convinced that there is no better framework, I continue to develop Vocup with Avalonia.
