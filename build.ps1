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

    function GetAppVersion () {
        $binary = Join-Path $buildDirectory "Vocup.exe"
        $version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($binary)
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
        Invoke7zip -ArgumentList "a","`"$archive`"","`"$buildDirectory\*`""
    }

    function CreateTarGzArchive () {
        $temp = Join-Path $outputDirectory "Vocup_$(GetAppVersion)_Mono.tar"
        Invoke7zip -ArgumentList "a","-ttar","`"$temp`"","`"$buildDirectory\*`""
        $archive = Join-Path $outputDirectory "Vocup_$(GetAppVersion)_Mono.tar.gz"
        Invoke7zip -ArgumentList "a","`"$archive`"","`"$temp`""
        Remove-Item -Path $temp
    }
}

process {
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
    Write-Host
    Write-Host "Modification for Mono are not automated yet. Please apply necessary changes manually."
    Write-Host "Press any key to continue..."
    [System.Console]::ReadKey($true) | Out-Null
    Write-Host
    if ($CreateSetup) {
        InvokeInnoSetup -Mono
    }
    if ($CreateArchives) {
        CreateTarGzArchive
    }
}
