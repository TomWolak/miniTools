Add-Type -AssemblyName PresentationFramework

$server = "V0161"
$port = 8451
$wasDown = $true

while ($true) {

    $isUp = Test-NetConnection $server -Port $port -InformationLevel Quiet

    if ($isUp) {
        Write-Host "$(Get-Date -Format 'HH:mm:ss') - $server OK" -ForegroundColor Green

        # show popup only if server was previously unavailable
        if ($wasDown) {

            [System.Windows.MessageBox]::Show(
                "$server is responding again on port $port",
                "VM Available",
                "OK",
                "Information"
            )

            $wasDown = $false
        }

    } else {
        Write-Host "$(Get-Date -Format 'HH:mm:ss') - $server FAIL" -ForegroundColor Red
        $wasDown = $true
    }

    Start-Sleep 10
}