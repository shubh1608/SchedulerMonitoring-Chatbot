$Computer = "DESKTOP-39N2S13"
$Service = "SchedulerService"
if(Test-Connection -ComputerName $Computer -Count 1 -ea 0) {
	$IsOnline = $true
	try {
		$ServiceObj = Get-Service -Name "RabbitMQ" -ComputerName $Computer -ErrorAction Stop
		Restart-Service -InputObj $ServiceObj -erroraction stop
        Set-Content -Path .\restart_service_output.txt -Value "Service restarted successfully."
				
	} catch {
        $msg = "Failed to restart $Service on $Computer. Error: $_"
		Write-Verbose $msg
        Set-Content -Path .\restart_service_output.txt -Value $msg
	}
}
else {
    $msg = "$Computer is not reachable"
	Write-Verbose $msg
    Set-Content -Path .\restart_service_output.txt -Value $msg
}

Get-Content -Path .\restart_service_output.txt

