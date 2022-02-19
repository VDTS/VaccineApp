# Secret Files
> This folder must include all secrets files.
- Place your __Firebase rtdb api key and BaseAddress__ within the file `AppSecrets.json`

> __Note:__ The project won't execute without these files.

## AppSecrets.json template

``` 
{
    "AppSecrets:FirebaseBaseAddress": "your_firebase_api_endpoint",
    "AppSecrets:FirebaseApiKey": "your_firebase_api_key",
  
    "AppSecrets:FirebaseBaseAddress_Offline": "your_firebase_api_endpoint_for_firebase_emulator"
}
```  
