# Assumes
#   1. The az command line utils are installed
#   2. az is logged in to the azure subscription where the AKS cluster lives
#   3. Project has been published that creates the base content going to AKS
#   4. azcopy is installed and available on the path

$storageAccountName=$(az storage account list --query '[0].{Name:name}' -o tsv)
$storageAccountKey=$(az storage account keys list -n $storageAccountName --query '[0].{value:value}' -o tsv)
$shareName='pod-storage'

$sasToken=$(az storage share generate-sas `
   --account-name $storageAccountName `
   --account-key $storageAccountKey `
   --permissions dlrw `
   --name $shareName `
   --expiry $(get-date).AddHours(2).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")
)

Write-Output $sasToken

azcopy copy .\obj\Debug\Freds.dll "https://$storageAccountName.file.core.windows.net/$shareName/bin/?$sasToken" 

