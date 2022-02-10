# VaccineApp
**VaccineApp** is a digital immunization software that helps in distributing and tracking vaccinations with a **MobileApp** and **WebPortal**. It will help the field vaccinators to register children for tracking their vaccinations. The team offers **digital** solutions.

## App General Info
* Language: C# 10, Javascript, HTML, CSS, XAML
* Framework:.NET 6
* UI Framework: .NET MAUI
* Database: Firebase rtdb
* Authentication Provider: Firebase

## Features
* Reliable child vaccination process.
* Communicaion channel between parents and vaccine employees for better improve of services
* Localized app which supports three different languages: English, Pashto and Dari
* accessibility features like: changing fonts, font sizes dark and light modes
* Feedback feature for improving the performance of the app
* cross platform (support **Windows 11** and **Android**)

# Preparing Envirnoment and Running the project locally

## Configuring API keys before cloning the repository
### Placement of Secret keys
Some services need secret keys for functioning:
1. Github bot- You need to set Github private key in the app.
2. Firebase Admin- You need to set firebase admin private in order to work with firebase admin.
3. Firebase rest API- Set firebase project API key and base URL for working with firebase storage and firebase real time database

<br />**Note:** put these files in a folder named [SecretFiles](https://github.com/NaveedAhmadHematmal/VaccineApp/tree/main/src/VaccineApp/SecretFiles) in VaccineApp project.

### Application Development and production Environments
There exist two options to work with firebase; online (for production, offline (for development). For switching between offline and online mode, change the value of [AppConfigs](https://github.com/NaveedAhmadHematmal/VaccineApp/blob/main/src/VaccineApp/AppConfigs/AppSettings.json) to online or offline.
<br /> **Note:** You need to download firebase emulator suite

