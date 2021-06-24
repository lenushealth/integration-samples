# dotnet-mybp-sample
Sample implementation of the Lenus Health platform to retrieve and submit blood pressure samples for a logged in user

# Requirements

- .NET core 3.1 SDK
- Visual Studio 2019 or Visual Studio Code
- Access to an instance of the Lenus Health platform
- Access to register a new client application for use with this sample

# Creating a client application

Register and/or login to your account and create a new client application.  The client application will be required to use the following configuration:

- RedirectUri: `https://localhost:5001/signin-oidc`
- Grant Type: `hybrid`
- Basic Scopes: `openid profile email`
- Correlation Scopes: `read.blood_pressure write.blood_pressure`
- Vitals Quantity Scopes: `read.blood_pressure.blood_pressure_diastolic read.blood_pressure.blood_pressure_systolic write.blood_pressure.blood_pressure_diastolic write.blood_pressure.blood_pressure_systolic`

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

# Deploy To Azure

You can also deploy an instance of this sample directly to azure using:

[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)
