param(
	[string]$sourceUrl = "http://geolite.maxmind.com/download/geoip/database/GeoLiteCity.dat.gz",
	[string]$compressedFile = "GeoLiteCity.dat.gz",
	[string[]]$updateTargets = @("\\server1\c$\path\GeoLiteCity.dat", "\\server2\c$\path\GeoLiteCity.dat")
)
$decompressedFile = $compressedFile -replace '.gz$'
$backupFile = "$decompressedFile.old"

. .\Get-WebFile.ps1

function UpdateFile {
    param( 
      [string] $source = $(throw "source is required"),
	  [string] $target = $(throw "target is required")
    )
	echo "Copying new database file '$source' to '$target'"
	& remove-item -path $target -force
	& copy-item -path .\$source $target -force
	if(!(test-path $target)){
		echo "ERROR: Failed to copy file '$source' to '$target'"
		RollBack
		exit
	}
}

function RollBack {
	foreach($processedTarget in $processedTargets){
		echo "Rolling back: $processedTarget"
		& copy-item -path .\$backupFile $processedTarget -force
	}
	echo "Removing backup file: $backupFile"
	& remove-item -path .\$backupFile
}

echo "Starting download of GeoIP database file from: '$sourceUrl'"
Get-WebFile $sourceUrl $compressedFile

echo "Decompressing '$compressedFile' to '$decompressedFile'"
& .\gzip.exe --decompress .\$compressedFile --force
if(!(test-path $decompressedFile)){
	echo "ERROR: Decompression failed. Nothing was updated."
	exit
}

echo "Backing up old database file (from first server) to allow for rollback."
$first = $updateTargets[0]
& copy-item -path $first .\$backupFile -force

[string[]]$processedTargets = @()
foreach($updateTarget in $updateTargets){
	$processedTargets += $updateTarget
	UpdateFile ".\$decompressedFile" $updateTarget
}

echo "Done updating."
echo "Cleaning up."
& remove-item -path .\$backupFile
& remove-item -path .\$decompressedFile

echo "Successfully updated database files on all servers."
