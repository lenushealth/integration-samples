# Clinician Agency API Sample

Sample implementation of the Lenus Health platform demonstrating use of the agency APIs to request access to a patient's health data.

## Requirements

- .NET core 3.1 SDK
- Visual Studio 2019 or Visual Studio Code
- Access to an instance of the Lenus Health platform
- Access to register a new client application for use with this sample

## Required Setup

### Creating a client application

Login to your developer portal account and create a new client application.  The client application will require the following configuration:

- RedirectUri: `https://localhost:5001/signin-oidc` (this may differ if you have altered the app launch settings)
- Grant Type: `hybrid`
- Basic Scopes: `openid profile email agency_api`

Set a known `client secret` value

## Updating the sample configuration files

Included in the sample is an `appsettings.json` file, within this file you will see a template of the configuration values required, these will need to be updated to successfully run the application, we would recommend that you override these settings via user-secrets configuration:

```json
  {
    "Lenus": {
      "OpenIdConnect": {
        "Authority": "https://<platform-identity-host>",
        "ClientId": "<your-configured-client-id>",
        "ClientSecret": "<configured-client-secret-value"
      },
      "Agency": {
        "BaseApiUri": "https://<platform-api-host>/agency/v1"
      }
    }
  }
```

## Building and Running

Open the solution either your editor or IDE of choice, or use the command line: