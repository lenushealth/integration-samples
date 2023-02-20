# dotnet-mybp-sample
Sample implementation of the Lenus Health platform to retrieve and submit blood pressure samples for a logged in user

# Requirements

- .NET core 7.0 SDK
- Visual Studio 2019 or Visual Studio Code
- Access to an instance of the Lenus Health platform
- Access to register a new client application for use with this sample

# Creating a client application

Register and/or login to your account and create a new client application.  The client application will be required to use the following configuration and scopes:

- RedirectUri: `https://localhost:5001/signin-oidc`
- Grant Type: `hybrid`
- Basic Scopes: `openid profile email`
- Correlation Scopes: `read.blood_pressure write.blood_pressure`
- Vitals Quantity Scopes: `read.blood_pressure.blood_pressure_diastolic read.blood_pressure.blood_pressure_systolic write.blood_pressure.blood_pressure_diastolic write.blood_pressure.blood_pressure_systolic`

Blood pressure scopes are provided within the developer portal via the "Blood Pressure" read and write checkboxes, under the hood this will include the scopes as described above.

![image](https://user-images.githubusercontent.com/432217/132019991-0eabf9d9-f364-4e8c-aa23-d5fd1d52a8aa.png)


Set a known `client secret` value

# Updating the sample configuration files

Included in the sample is an `appsettings.development.json` file, within this file you will need to update the following configuration with values obtained with creating your client application:

```json
  "OpenIdConnect" : 
  {
     "Authority" :  "<uri-to-identity-service>",
     "ClientId" :  "<client-id>",
     "ClientSecret" : "<client-secret>"
  } 
```

The `Authority` uri will be that of the security token service for the Lenus health platform environment.

Then update the following section of the same configuration file:

```json
  "HealthDataClient" : 
  {
    "BaseUri" :  "<uri-to-data-service>"
  }
```

Where the `BaseUri` for the Lenus platform API.

# Building and Running

Open the solution either in Visual Studio 2017 or using Visual Studio Code

## Deploy Sample to Azure

Refer to [these general instructions](../deploy/readme.md) for a guide to running the resource deployment.
