# FreeMoney

I built this application to learn how to deploy an application using Azure.

I made two deployment variants for my solution, using Azure Kubernetes Service and App Service:

<table>
    <tr>
        <th>Azure Kubernetes Service</th>
        <th>App Service</th>
    </tr>
    <tr align="center">
        <td colspan="2">Azure SQL Server</td>
    </tr>
    <tr align="center">
        <td colspan="2">Azure Functions</td>
    </tr>
</table>

## SLA

I also did the calculation of the SLA for my whole solution using the data provided by Microsoft:

| Azure Resource           | SLA    |
| :----------------------- | :----- |
| Azure Kubernetes Service | 99.9%  |
| App Service              | 99.95% |
| Azure SQL Server         | 99.9%  |
| Azure Functions          | 99.95% |

The total SLA for each solution variant is:
| Azure Resource | SLA |
| :----------------------- | :--------------------------- |
| Azure Kubernetes Service | (.999)(.9995)(.999) = **99.75%** |
|App Service | (.999)(.9995)(.9995) = **99.8%** |

## How to run

Provide environment variables for `docker-compose` in a `.env` file.
You can use `.env.example` as a guide.
This is not needed to run the project, but not all features will be available.

```shell
cp .env.example .env
```

A sample valid `.env` file might look like this:

```
FREE_MONEY_CONNECTION_STRING=Server=tcp:example-server,1433;Initial Catalog=free-money;User ID=ExampleUser;Password=ExamplePass1;
AZURE_FUNCTION_REGISTER_USER_RECORD=https://example-functions.azurewebsites.net/api/RegisterUserRecord
```

To run project.

```shell
docker-compose up
```
