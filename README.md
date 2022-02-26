# VaccineApp
**VaccineApp** is a digital immunization software that helps in distributing and tracking vaccinations with a **MobileApp**, **Desktop** and **WebApp**. It will help the field vaccinators to register children for tracking their vaccinations. The team offers **digital** solutions.

## Features
* Reliable child vaccination process.
* Communicaion channel between parents and vaccine employees for better improve of services
* Localized app which supports three different languages: English, Pashto and Dari
* accessibility features like: changing fonts, font sizes dark and light modes
* Feedback feature for improving the performance of the app
* cross platform (support **Windows 11** and **Android**)

## Documentation
* Documentation is [Here](https://github.com/NaveedAhmadHematmal/VaccineApp/blob/main/docs/documentation.md)
# Preparing Envirnoment and Running the project locally

## Configuring API keys before cloning the repository
### Placement of Secret keys
Some services need secret keys for functioning:
1. Github bot- You need to set Github private key in the app.
2. Firebase Admin- You need to set firebase admin private in order to work with firebase admin.
3. Firebase rest API- Set firebase project API key and base URL for working with firebase storage and firebase real time database  
<br /> 

**Note:** For __Desktop__ put these files in a folder named [SecretFiles](https://github.com/NaveedAhmadHematmal/VaccineApp/tree/main/src/VaccineApp/SecretFiles/readme.md) in VaccineApp project and for __Web__ put them in [SecretFiles](https://github.com/NaveedAhmadHematmal/VaccineApp/blob/main/src/Web/VaccineApp.BlazorPWA/VaccineApp.BlazorPWA/wwwroot/SecretFiles/readme.md)

### Application Development and production Environments
There exist two options to work with firebase; online (for production), offline (for development). For switching between offline and online mode, change the value of [AppConfigs](https://github.com/NaveedAhmadHematmal/VaccineApp/blob/main/src/VaccineApp/AppConfigs/AppSettings.json) to online or offline for __Desktop__ and for __Web__ go to this [Link](https://github.com/NaveedAhmadHematmal/VaccineApp/blob/main/src/Web/VaccineApp.BlazorPWA/VaccineApp.BlazorPWA/wwwroot/AppConfigs/AppSettings.json).  

## LOC

[Click here](https://api.codetabs.com/v1/loc/?github=VDTS/VaccineApp) to see counts of __Lines of Codes__

## Stats
![Alt](https://repobeats.axiom.co/api/embed/401483c7d3e66e08967e37e81b4adff52c750f58.svg "Repobeats analytics image")
