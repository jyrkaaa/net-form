Runs with sqlite, Session based save and update functionality with basic DI principles.
~~~sh 
dotnet ef migrations add --project App.DAL --startup-project WebApp --context AppDbContext InitialCreate

dotnet ef migrations   --project App.DAL --startup-project WebApp remove

dotnet ef database   --project App.DAL --startup-project WebApp update
dotnet ef database   --project App.DAL --startup-project WebApp drop

~~~
MVC Controllers
~~~sh
cd WebApp

dotnet aspnet-codegenerator controller -name UserController        -actions -m  App.Domain.User        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserSectorController        -actions -m  App.Domain.UserSector        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SectorController        -actions -m  App.Domain.Sector        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

~~~