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
    $projectDirectory = Join-Path $PSScriptRoot "src\Vocup"
    $buildDirectory = Join-Path $projectDirectory "bin\Release\net6.0-windows10.0.19041.0\win-x86\publish"
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
        Invoke7zip -ArgumentList "a","`"$archive`"","`"$buildDirectory\*`"","-x!*.winmd"
    }
}

process {
    $ErrorActionPreference = "Stop"

    Start-Process -FilePath "dotnet" -ArgumentList "publish","-c","Release","-a","x86","--sc","-p:PublishSingleFile=true" -WorkingDirectory $projectDirectory -NoNewWindow -Wait
    if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

    if ($CreateSetup) {
        InvokeInnoSetup
        if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
    }
    if ($CreateArchives) {
        CreateZipArchive
        if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
    }
}
