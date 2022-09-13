# Clinician Agency Flow via client_credentials Sample

Sample implementation of the Lenus Health platform demonstrating use client_credentials grant type flow to support requesting agency over a patient by a clinician within an organisation.

## Running locally

Open solution in IDE of choice and run the Lenus.Samples.ClientCredentialsFlow project. 

## Required Setup

You will need to set up a client via the developer portal configured to use the client_credentials grant type. This can be done via a sandbox using your developer account.

For this solution only agency_api scope is required.

Once you have set up your client you will need contact Lenus support and ask us to enable an organisation for the client you created. The id of this organisation is required when sending an agency invite.

After creating your client replace the values in appsettings:

```
  "Lenus": {
    "OpenIdConnect": {
      "TokenUrl": "<REPLACE URL>/connect/token",
      "ClientId": "<REPLACE CLIENT ID>",
      "Scope": "agency_api"
    },
    "Agency": {
      "BaseApiUri": "<REPLACE URL>/api/agency",
      "OrganisationId": "<REPLACE ORGANISATIONID>"
    }
  }
```

Additionally in your secrets file you will need to add your client secret:
```
  "Lenus": {
    "OpenIdConnect": {
      "ClientSecret": "<REPLACE SECRET>",
    }
  }
```

## Testing the project

Once the project is running a simple UI will be launched providing inputs for an email, phone number, and requested scopes. Submitting this form will make a request to send agency invite via the sandbox identity platform, you will see emails come through via sandbox mailtrap.



