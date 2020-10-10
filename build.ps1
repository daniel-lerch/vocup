<#
.SYNOPSIS
    Build and packaging tool for Vocup
.PARAMETER CreateSetup
    Run Inno Setup compiler after build to create an installation wizard
.PARAMETER CreateArchives
    Pack previously built binaries into archives for portable usage
#>

[CmdletBinding()]
param (
    [switch]$CreateSetup,
    [switch]$CreateArchives
)

begin {
    $setupDirectory = Join-Path $PSScriptRoot "setup"
    $outputDirectory = Join-Path $setupDirectory "bin"
    $buildDirectory = Join-Path $PSScriptRoot "src\Vocup\bin\Release"
    $executable = Join-Path $buildDirectory "Vocup.exe"

    function GetAppVersion () {
        $version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($executable)
        return "$($version.FileMajorPart).$($version.FileMinorPart).$($version.FileBuildPart)"
    }

    function InvokeInnoSetup ([switch]$Mono) {
        $is = Join-Path ${env:ProgramFiles(x86)} "Inno Setup 6"
        $iscc = Join-Path $is "ISCC.exe"
        $iss = Join-Path $setupDirectory "vocup.iss"
        if ($Mono) {
            $filename = "Vocup_$(GetAppVersion)_Mono"
        }else {
            $filename = "Vocup_$(GetAppVersion)"
        }
        if (Test-Path -Path $iscc -PathType Leaf) {
            Start-Process -FilePath $iscc -ArgumentList "/F$filename","`"$iss`"" -WorkingDirectory $setupDirectory -NoNewWindow -Wait
        } else {
            Write-Error "Inno Setup was not found at $is"
            exit 1
        }
    }

    function Invoke7zip ([string[]]$ArgumentList) {
        $7zip = Join-Path $env:ProgramFiles "7-Zip"
        $7z = Join-Path $7zip "7z.exe"
        if (Test-Path -Path $7z -PathType Leaf) {
            Start-Process -FilePath $7z -ArgumentList $ArgumentList -NoNewWindow -Wait
        } else {
            Write-Error "7-Zip was not found at $7zip"
            exit 1
        }
    }

    function CreateZipArchive () {
        $archive = Join-Path $outputDirectory "Vocup_$(GetAppVersion).zip"
        if (Test-Path -Path $archive -PathType Leaf) {
            Remove-Item -Path $archive
        }
        Invoke7zip -ArgumentList "a","`"$archive`"","`"$buildDirectory\*`"","-x!*.xml"
    }

    function CreateTarGzArchive () {
        $temp = Join-Path $outputDirectory "Vocup_$(GetAppVersion)_Mono.tar"
        Invoke7zip -ArgumentList "a","-ttar","`"$temp`"","`"$buildDirectory\*`"","-x!*.xml"
        $archive = Join-Path $outputDirectory "Vocup_$(GetAppVersion)_Mono.tar.gz"
        if (Test-Path -Path $archive -PathType Leaf) {
            Remove-Item -Path $archive
        }
        Invoke7zip -ArgumentList "a","`"$archive`"","`"$temp`""
        Remove-Item -Path $temp
    }

    function RemoveConfigSection () {
        [string]$configuration = Get-Content -Path "$executable.config" -Raw

        [int]$start1 = $configuration.IndexOf("  <configSections>")
        $search1 = "</userSettings>`r`n"
        [int]$end1 = $configuration.IndexOf($search1) + $search1.Length
        Write-Host "Start: $start1 End: $end1"
        $configuration = $configuration.Remove($start1, $end1 - $start1)

        [int]$start2 = $configuration.IndexOf("  <System.Windows.Forms.ApplicationConfigurationSection>")
        $search2 = "</System.Windows.Forms.ApplicationConfigurationSection>`r`n"
        [int]$end2 = $configuration.IndexOf($search2) + $search2.Length
        Write-Host "Start: $start2 End: $end2"
        $configuration = $configuration.Remove($start2, $end2 - $start2)

        Set-Content -Path "$executable.config" -Value $configuration -NoNewline
    }
}

process {
    $ErrorActionPreference = "Stop"
    Write-Host "MSBuild integration is not implemented yet. Please build Vocup using Visual Studio."
    Write-Host "Press any key to continue..."
    [System.Console]::ReadKey($true) | Out-Null
    Write-Host
    if ($CreateSetup) {
        InvokeInnoSetup
    }
    if ($CreateArchives) {
        CreateZipArchive
    }

    RemoveConfigSection

    if ($CreateSetup) {
        InvokeInnoSetup -Mono
    }
    if ($CreateArchives) {
        CreateTarGzArchive
    }
}
