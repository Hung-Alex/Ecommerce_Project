# Organic-Store

## Introduction
This project is a full-stack web application built using modern web technologies. It features a responsive frontend and a robust backend to handle various functionalities.

<p align="center">
<h1 align="center">Organic Store</h1>
<h3 align="center"><strong>Powerful .NET 8 Organic Store API</strong></h3>
<p align="center">Create your own custom front-end or explore a complete Organic Store API.</p>

Live demo : Is Updating

This project was developed by a dedicated team of web developers:

- **[Phạm Ngọc Hưng]** - Backend Developer  
  [GitHub Profile](https://github.com/Hung-Alex)  
- **[Hồ Trương Huệ Nhật]** - Frontend Developer  
  [GitHub Profile](https://github.com/hotruonghuenhat)  
## Team Size
- **Team Size:** 2 Developer
## Contact

For any questions or inquiries, please contact . 

## Features
  - **CRUD for all models** to manage all data through the API.
- **User Registration, Login, and Logout**, including login with Google.
- **JWT Authentication** with support for access and refresh tokens.
- **Dynamic Role and Permission Management**:
  - Create, assign, modify, and update roles and permissions dynamically via the API.
  - Example: Create a new role "mini-moderator" with specific permissions like CanEdit own Posts, etc.
- **VNPay Sandbox Payment Integration** for secure and seamless payment processing.
- **Search** across all resources with customizable filters.
- **Post and Comment Creation** allowing users to create posts and comment on them.
- **Management** of users, orders, categories, slides, banners, and products.
- **File Upload with Cloudinary** for efficient and scalable media handling.
- **User Account Locking** to prevent access when necessary.
## Technologies
 - [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
 - [EntityFramework](https://learn.microsoft.com/en-us/ef/)
 - [Microsoft Dependency Injection (DI)](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
 - [FluentValidation](https://github.com/FluentValidation/FluentValidation)
 - [Swashbuckle (Swagger)](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio)
 - [AutoMapper](https://automapper.org/)
 - [MediaR](https://github.com/jbogard/MediatR)
 - [Google.Apis.Auth](https://github.com/googleapis/google-api-dotnet-client)
 - [Identity](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore)
 - [VnPay](https://sandbox.vnpayment.vn/apis/docs/thanh-toan-pay/pay.html) => Vnpay Sanbox
 - [Cloudinary](https://cloudinary.com/documentation/dotnet_integration#landingpage)
## Patterns
 - Repository
 - Generic Repository
 - Unit Of Work
 - Specification
 - CQRS (Command and Query Responsibility Segregation)
## Architechture
  -clean architecture
  
## Getting Started

### Prerequisites

- Install [Microsoft SQL Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads) 
- [Visual Studio](https://visualstudio.microsoft.com/fr/)

### Installation

- Create ``appsettings.json`` file inside **OrganicAPI** project below and edit or add the database settings :
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Debug",
      "Microsoft.AspNetCore.Authentication": "Trace",
      "Microsoft.Hosting.Lifetime": "Trace"
      //"Microsoft.AspNetCore.Authentication": "Information",
      //"Default": "Information",
      //"Microsoft.AspNetCore": "Warning"
    }
  } 
}
```
For **Microsoft SQL Server** :

```json
"ConnectionStrings": {
 "EcommerceDB": "Server=.;Database=EcommerceDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
```

For **Other ** :

```json
"CloudDinarySettings": {
  "CloudName": "your cloud name",
  "ApiKey": "your api key",
  "ApiSecret": "your secret"
},
"JwtSetting": {
  "SecretKey": "your SecretKey",
  "Issuer": "your Issuer",
  "Audience": "your Audience",
  "ExpiredToken": 6, //minutes
  "ExpiredRefreshToken": 7 //day
},
"Google": {
  "ClientId": "your ClientId",
  "ClientSecret": "your ClientSecret"
},
"vnPay": {
  "vnp_TmnCode": "your vnp_TmnCode",
  "vnp_HashSecret": "your vnp_HashSecret",
  "vnp_Url": "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
  "vnp_Returnurl": "your vnp_Returnurl"
}
```

Then in visual studio :

1. Set **Organic Store API** as project to run
1. Open the Package Manager Console (Tools -> Nuget Package Manager -> Package Manager Console).
2. In the package Manager Console, select **Infrastructure.Persistence** as Default project
3. Run the following commands:

- **Microsoft SQL Server**
```
Add-Migration Init
Update-Database
```
5. Now press F5 and run the application.
6. Refresh the page. The API was busy filling the default data in the database. Now it can respond.
