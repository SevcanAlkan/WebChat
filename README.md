# WebChat

WebChat is real time web based chatting app. .Net Core Web API, SignalR, EntityFramework, Angular 8, RxJS technologies are used on project.

### Needs to work

* NodeJS and NPM [Download](https://nodejs.org/en/download/
)
* Angular [Setup](https://angular.io/guide/setup-local
)
* .Net Core 2.2 [Download](https://dotnet.microsoft.com/download)
* MS SQL (14)

API is configured to work as standalone. Before the start API you must do belowing steps;

1. Check EF connection string in `NGA.API.appSettings.json`, if needs, edit it for your DB.
2. Open **Package Manager Console**, then select NGA.Data project and run **Update-Database** command.
3. You can start the API now.

> ~~You must add users to DB via sending POST Requests.~~ **DB Initializer** class only adds default Groups to DB!

4. DB dosen't have any user, you must create test users. You can use register option on Login page(**V0.2 or higher**). 
    *  ~~HTTP Request type: **POST**~~
    *  ~~Route: `http://localhost:5008/api/user/createtoken`~~
    *  ~~Model;~~
    ``` json
        {
            "UserName": "admin", 
            "PasswordHash":"Admin.123", 
            "DisplayName":"Admin", //Visible name for other users
            "Status":4, //No need to change
            "Email":"", //No need to change
            "IsAdmin":0, //No need to change
            "IsBanned":0, //No need to change
            "About":" - " //This fields for show on Profile page as description
        }

    ```
5. Open FrontEnd folder and run `ng serve` command for start Angular!





