
param serverFarmName string
param webAppName string
param location string = resourceGroup().location
param appSettings object = {}

resource serverFarmResource 'Microsoft.Web/serverfarms@2021-01-15' = {
  name: serverFarmName
  location: location
  sku: {
    name: 'F1'
  }
}

resource webAppResource 'Microsoft.Web/sites@2021-01-15' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: serverFarmResource.id
    httpsOnly: true
    siteConfig: {
      ftpsState: 'Disabled'
      http20Enabled: true
      minTlsVersion: '1.2'
      scmMinTlsVersion: '1.2'
      use32BitWorkerProcess: true
      websiteTimeZone: 'GMT Standard Time'
    }
  }
}

resource webAppConfigResource 'Microsoft.Web/sites/config@2021-01-15' = {
  parent: webAppResource
  name: 'appsettings'
  properties: appSettings
}

output siteUrl string = 'https://${webAppResource.properties.hostNames[0]}'
