# Search Demo App

This is a demo application that is intended to be an illustration of how to index the output of the S2T data generated by the [solution accelerator](https://github.com/Azure-Samples/cognitive-services-speech-sdk/tree/master/samples/batch/transcription-enabled-storage). The search application will display top 100 search results and sample filters for demoing the search of the S2T transcription.

![Result Display](https://github.com/fsaleemm/searchdemoapp/blob/master/images/result.JPG)

## High Level Setup Steps
1. [Create storage account](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal#create-a-storage-account-1) and [container](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-portal#create-a-container), then copy the [sample data](https://github.com/fsaleemm/searchdemoapp/tree/master/searchqueryapp/SampleData) into the container.
2. Create a [Azure Cognitive Search service](https://docs.microsoft.com/en-us/azure/search/search-create-service-portal).
3. Follow instruction on [How to index JSON blobs using a Blob indexer in Azure Cognitive Search](https://docs.microsoft.com/en-us/azure/search/search-howto-index-json-blobs#:~:text=Parsing%20modes%20%20%20%20parsingMode%20%20,mode%20if%20your%20blobs%20consist%20o%20..., "") to index JSON in the SampleData folder.
    1. For Step 3 in instructions, select Parsing mode as JSON, and the storage container created earlier.
    2. For step 5 in instructions, set index attributes as specified in the screenshot shown below:
    ![Index Definition](https://github.com/fsaleemm/searchdemoapp/blob/master/images/indexdefinition.JPG)
4. Create a Web App Service and deploy search demo app using [Kudu App Service option](https://docs.microsoft.com/en-us/azure/app-service/deploy-continuous-deployment#option-1-kudu-app-service)
5. Enable Authentication on the App Service. For simplicity use [Express Settings](https://docs.microsoft.com/en-us/azure/app-service/configure-authentication-provider-aad#-configure-with-express-settings)
6. Add the following [configurations to the App Service.](https://docs.microsoft.com/en-us/azure/app-service/configure-common#edit-in-bulk) Fill in the Index Name, Search Service Name and Query API from your Search Service.
```json
[
  {
    "name": "SearchIndexName",
    "value": "<My Index Name>",
    "slotSetting": false
  },
  {
    "name": "SearchServiceEndpoint",
    "value": "https://<My Search Service Name>.search.windows.net",
    "slotSetting": false
  },
  {
    "name": "SearchServiceQueryApiKey",
    "value": "<My Query API Key>",
    "slotSetting": false
  }
]
```
#### Disclaimer
This is a sample application with rudimentary error/exception handling and no unit testing. It is intended as an illustration/demo and not for production use.
