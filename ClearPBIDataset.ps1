#Install-Module -Name PowerBIPS

$authToken = Get-PBIAuthToken


# To use username+password authentication you need to create an Azure AD Application and get it's id

#$authTokenWithUsername = Get-PBIAuthToken -ClientId "5df9cba3-45ed-4e5d-b7ad-e47951fe1eb1" -Credential (Get-Credential -username "<username>"

#$authTokenWithUsernameAndPassword = Get-PBIAuthToken -ClientId "C0E8435C-614D-49BF-A758-3EF858F8901B" -Credential (new-object System.Management.Automation.PSCredential("<username>",(ConvertTo-SecureString -String "<password>" -AsPlainText -Force)))

$group = Get-PBIGroup -authToken $authToken -name "Project Cars"

Set-PBIGroup -id $group.id


$dataSetsOfGroup = Get-PBIDataSet -authToken $authToken



Clear-PBITableRows -authToken $authToken -dataSetName "pcars-participants-stream" -tableName "pcars-participants" -Verbose

Clear-PBITableRows -authToken $authToken -dataSetName "pcars-telemetry-stream" -tableName "pcars-telemetry-stream" -Verbose