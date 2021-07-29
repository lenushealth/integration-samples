targetScope = 'subscription'

@description('Provide a unique suffix to apply to the deployed sample to avoid conflicts with existing sample deployments, e.g. company short name like \'lh\'')
@maxLength(6)
param uniqueSampleSuffix string

@description('The HTTPS endpoint of the Lenus Health identity service for the environment you are connecting to, this will have been supplied during onboarding')
param identityHost string

@description('The clientId created for this sample')
param clientId string

@description('The clientSecret set for the clientId supplied')
@secure()
param clientSecret string

@description('The HTTPS endpoint of the Lenus Health API gateway for the environment you are connecting to, this will have been supplied during onboarding')
param apiHost string

module clinicianOrgSampleModule '../../_deploy/modules/sample.sub.module.bicep' = {
  name: 'org-sample'
  scope: subscription()
  params: {
    sampleName: 'clinorg'
    nameSuffix: uniqueSampleSuffix
    appSettings: {
      'Lenus:OpenIdConnect:Authority': identityHost
      'Lenus:OpenIdConnect:ClientId': clientId
      'Lenus:OpenIdConnect:ClientSecret': clientSecret
      'Lenus:Agency:BaseApiUri': '${apiHost}/agency/v1'
      'Lenus:Organisations:BaseApiUri': '${apiHost}/organizations/v1'
    }
  }
}
