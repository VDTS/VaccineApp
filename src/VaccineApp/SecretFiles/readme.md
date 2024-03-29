﻿# Secret Files
> This folder includes all private keys and other secret configuration files.
- Set your __Firebase project private key__ as `firebase_private_key.json`
- Set your __Github app private key__ as `github_app_private_key.pem`
- Place your __Firebase rtdb api key and BaseAddress__ within the file `AppSecrets.json`

> __Note:__ Change the build action of these files into `Embedded resource` in each file properties.  

> __Note:__ The project won't execute without these files.


```
{
  "AppSecrets:FirebaseBaseAddress": "your_firebase_api_endpoint",
  "AppSecrets:FirebaseApiKey": "your_firebase_api_key",
  "AppSecrets:FirebaseStorageAddress": "your_firebase_storage_api_endpoint",
  "AppSecrets:FirebaseAuthAddress": "your_firebase_auth_api_endpoint",
  "AppSecrets:GithubAppId": your_github_app_id,

  "AppSecrets:FirebaseBaseAddress_Offline": "your_firebase_api_endpoint_for_firebase_emulator",
  "AppSecrets:FirebaseAuthAddress_Offline": "your_firebase_auth_endpoint_for_firebase_emulator"
}

```
