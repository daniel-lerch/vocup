# Vocup technical architecture

## UI Framework

Vocup was originally built with Windows Forms which does not meet today's requirements anymore.
After a major modernization of the WinForms codebase in 2018 I did not find a suitable replacement for the UI framework.

Now in 2024 many frameworks have made significant progress.
Avalonia and Uno Platform are the most promising frameworks with their unique strengths and weaknesses.

In the end I decided to go with **Avalonia** because of its simpler architecture
and more widespread adoption, namely  Git Credential Manager, Icons8 Lunacy, Plastic SCM, and dotTrace.

### Considered frameworks

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
