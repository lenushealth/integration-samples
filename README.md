## Introduction

This repository contains dotnet samples for integration with the Lenus Health platform.

## Requirements

- .NET core 7.0 SDK
- Visual Studio 2022 or Visual Studio Code
- Access to an instance of the Lenus Health platform
- Access to register a new client application for use with this sample

## Samples

The following samples are available:
- [Clinician sample](https://github.com/lenushealth/integration-samples/tree/main/samples/dotnet/clinician) - Request agency for other users and display their data samples
- [Clinician organisation consent sample](https://github.com/lenushealth/integration-samples/tree/main/samples/dotnet/clinorg) - Demonstrates usage of the organisations and agency API to request agency on behalf on an organisation
- [Blood pressure sample](https://github.com/lenushealth/integration-samples/tree/main/samples/dotnet/mybp) - Retrieve and submit blood pressure samples
- [Client Credentials flow sample](https://github.com/lenushealth/integration-samples/tree/main/samples/dotnet/clientcreds) - Demonstrates usage of `client_credentials` to authenticate agency API calls without manually inputting credentials. It requests agency on behalf of an organisation enabled for the client. Organsation management is handled by Lenus support.

## Utils

Some utility scripts are provided to help build and publish each sample, please refer to the individual readme files for each sample for further instructions