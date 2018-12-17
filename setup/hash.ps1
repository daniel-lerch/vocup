function Get-WebHash {
	param(
		[Parameter(Mandatory=$true)]
		[string]$Type,

		[Parameter(Mandatory=$true)]
		[string]$FileName
	)

	$fs = New-Object System.IO.FileStream($FileName, [System.IO.FileMode]::Open)
	$csp = [System.Security.Cryptography.HashAlgorithm]::Create($type)
	$value = $csp.ComputeHash($fs)
	$csp.Dispose()
	$fs.Dispose()
	return $Type.ToLower() + "-" + [System.Convert]::ToBase64String($value)
}