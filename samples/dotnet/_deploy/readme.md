# Deployment instructions (bicep)

Each sample should have a bicep deployment template that can be used to deploy Azure resources and configuration, to follow the instructions below you will need:

- Azure CLI (v2.20.0+) with support for bicep
- Access to an Azure subscription in which you have subscription contributor rights

Firstly ensure you are correctly authenticated using the Azure CLI and that you are pointing at the correct subscription:

```azurecli
az account show
```

Once confirmed you can verify the deployment template by navigating to the `./deploy` directory and running the following command:

```azurecli
az deployment sub what-if -f main.bicep
```

You will need to specify the required deployment parameters after which a what-if run will occur returning a list of the resources that will be created.

If happy the deployment can be executed with:

```azurecli
az deployment sub create -f main.bicep
```
