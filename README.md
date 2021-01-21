# Search Demo App

This is a demo application that is intended to be an illustration of how to use the Azure Search Service and is not intended for production use. This illustration is for a very specific document schema, index schema and search scenario.
Use at your own risk.

## High Level Setup Steps
1. Create stoage account or use and existing account container and copy the sample data into a container.
2. Create a Azure Cognitive Search service.
3. Follow instruction on [How to index JSON blobs using a Blob indexer in Azure Cognitive Search](https://docs.microsoft.com/en-us/azure/search/search-howto-index-json-blobs#:~:text=Parsing%20modes%20%20%20%20parsingMode%20%20,mode%20if%20your%20blobs%20consist%20o%20..., "") to index JSON in the SampleData folder.
    1. For Step 3 in instructions, select Parsing mode as JSON.
    2. For step 5 in instructions, set index attributes as specified in the sample index deifnition and as shown below:
    add image
4. Create a Web App Service and deploy search demo app using [Kudu App Serivce option](https://docs.microsoft.com/en-us/azure/app-service/deploy-continuous-deployment#option-1-kudu-app-service)
5. Enable Authnetication on the App Service. For simplicity use [Express Settings](https://docs.microsoft.com/en-us/azure/app-service/configure-authentication-provider-aad#-configure-with-express-settings)
