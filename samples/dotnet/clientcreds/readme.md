# Clinician Agency Flow via client_credentials Sample

Sample implementation of the Lenus Health platform demonstrating use client_credentials grant type flow to support requesting agency over a patient by a clinician within an organisation.

## Running locally

Open solution in IDE of choice and run the Lenus.Samples.ClientCredentialsFlow project. If using Visual Studio you will be prompted to install a self-signed certificate on your machine for https redirection. Running via CLI or another IDE (VS code) will not do this step automatically so it will be necessary to install the certificates yourself. This can be done by following these commands:

```
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

Run `dotnet dev-certs https --trust` on macOS and Windows or alternatively trust the dev certificate via your browser

## Required Setup

You will need to set up a client via identity admin configured to use the client_credentials grant type. This can be done via a deployed environment such as test or in a locally running instance.

For this solution only agency_api scope is required.

Once you have set up your client you will need to enable an organisation for the parent product and client you created. The id of this organisation is required when completing the form in this solution to send an agency invite.

After creating your client replace the values in appsettings:

```
  "Lenus": {
    "OpenIdConnect": {
      "TokenUrl": "<REPLACE URL>/connect/token",
      "ClientId": "<REPLACE CLIENT ID>",
      "Scope": "agency_api"
    },
    "Agency": {
      "BaseApiUri": "<REPLACE URL>/api/agency"
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

Once the project is running a simple UI will be launched providing inputs for an email, phone number, organisation id and requested scopes. Submitting this form will make a request to send agency invite via identity platform.



