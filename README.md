## Description

Education platform based on subscriptions plans written in .NET that uses [Asaas](https://www.asaas.com), a payment plataform widely used in Brazil.

## Badges

[![build and deploy to app service](https://github.com/backjoao88/education-by-subscription-api/actions/workflows/build.yml/badge.svg)](https://github.com/backjoao88/education-by-subscription-api/actions/workflows/build.yml)

## Stack and concepts applied

- .NET 8;
- Entity Framework Core 9.0;
- SQL Server 16.0.1000;
- Clean Architecture;
- Outbox Pattern;
- Serilog for logs;
- Asaas for charging;
- Quartz for recurrent jobs;

## Use cases and requirements

- [x] The admin users must be able to insert, list and update subscriptions plans;
- [x] The admin user must be able to insert, list and update courses and lessons;
- [x] The user must be able to sign himself into the system and update his informatioin;
- [x] The user must not have access to a course that is not in his allowed courses;
- [x] The app must be able to accept a HTTP post request from the payment server (webhook) and confirm it internally;

## Running

### Pre-requisites

- An Azure account with an active subscription. 
- https://dotnet.microsoft.com/en-us/download/dotnet/8.0 (.NET Core 8.0 SDK - ensure it's in PATh)

### Testing the app locally

Before start, ensure you have with you:
- A valid storage account name, account key and container name ([Creating a storage account](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal))
- A valid vault URI ([Creating a vault](https://learn.microsoft.com/en-us/azure/key-vault/general/quick-create-portal));
- A valid [Asaas](https://sandbox.asaas.com/login/auth) sandbox API key; 
- A valid SQL Server instance connection string;

#### Clone the GitHub repository

Download or clone the repository from GitHub. Use the following command to clone:

```bash
git clone https://github.com/backjoao88/education-by-subscription-api
```

#### Setting up the Azure Key Vault

In the EducationBySubscription\Api folder, init the .NET secret manager.

```bash
dotnet user-secrets init
```

Set your Azure Key Vault Uri.

```bash
dotnet user-secrets set "Vault:VaultUri" "<vault-uri>"
```

Ensure it was created with the following command.

```bash
dotnet user-secrets list
```
#### Setting up the Azure Vault credentials

Navigate to your vault in [Azure](https://portal.azure.com/signin/index/) and ensure you have the following **secrets** set.

- **EduSubscriptionStorageAccountName**: For referring to azure account name.
- **EduSubscriptionStorageAccountKey**: For referring to azure account key.
- **EduSubscriptionStorageContainerName**: For referring to azure blob container.
- **AsaasApiKey**: For referring your Asaas sandbox account.
- **JwtIssuer**: For referring to your Jwt token issuer.
- **JwtAudience**: For referring to your Jwt audience.
- **JwtPrivateKey**: For referring to your JWT private key.
- **EduSubscriptionDbConnectionString**: For referring to your SQL Server instance connection string.

For running it with local credentials you must edit the `EducationBySubscription\Infrastructure\DepdendencyInjection.cs` file,
and change all vault options setup to local options setup. Example (JWT service):

From

```csharp
public static IServiceCollection AddJwt(this IServiceCollection services)
{
    services
        .ConfigureOptions<JwtOptionsVaultSetup>()
        .ConfigureOptions<JwtBearerOptionsSetup>()
        .AddScoped<IJwtProvider, JwtProvider>();
    return services;
}
```

To

```csharp
public static IServiceCollection AddJwt(this IServiceCollection services)
{
    services
        .ConfigureOptions<JwtOptionLocalSetup>()
        .ConfigureOptions<JwtBearerOptionsSetup>()
        .AddScoped<IJwtProvider, JwtProvider>();
    return services;
}
```

Then you can navigate to `appsettings.json` and set the corresponding sections (Storage example).

```json
"Storage": {
    "AccountName": "<your-account-name>",
    "AccountKey": "<your-account-key>",
    "ContainerName": "<your-container-name>"
},
```

#### Run

Restore the project.

```bash
dotnet restore
```

Run the API.

```bash
dotnet watch
```

You can check the available requests at `EducationBySubscription\Api\Requests` folder.

## Contributions

You can contribute to this project, send a pull request and i'll be happily evaluated. 




