targetScope = 'subscription'

param resourcePrefix string = 'lenussamples'

param sampleName string

@maxLength(6)
param nameSuffix string

param location string = 'uksouth'

param appSettings object = {}

var rgName = '${resourcePrefix}-rg'
var serverFarmName = '${resourcePrefix}-asp'
var webAppName = '${resourcePrefix}-${sampleName}-${nameSuffix}'

resource rgResource 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: rgName
  location: location
}

module deploySampleModule 'webApp.module.bicep' = {
  scope: rgResource
  name: webAppName
  params: {
    serverFarmName: serverFarmName
    webAppName: webAppName
    appSettings: appSettings
  } 
}

output sampleSiteUrl string = deploySampleModule.outputs.siteUrl
