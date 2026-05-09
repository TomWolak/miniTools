# TcpWatcher

Simple PowerShell utility for monitoring TCP availability of a VM or application server.

## Features

- Periodic TCP connectivity checks
- Console status output
- Popup notification when the server becomes available again
- Useful during VM restarts, deployments, or application startup monitoring

## Configuration

Edit:

```powershell
$server = "V0161"
$port = 8451
$checkIntervalSeconds = 10
```

## Run

```powershell
.\TcpWatcher.ps1
```