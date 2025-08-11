CqrsUser


dotnet add package MediatR
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package AutoMapper
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package FluentValidation
dotnet add package FluentValidation.DependencyInjectionExtensions


run cmd
dotnet restore
dotnet build
dotnet run

http://localhost:5283/swagger/index.html <- swagger page
http://localhost:5283/  <- index page
http://localhost:5283/UsersMvc/Details/{id}   <- details page
http://localhost:5283/UsersMvc/ByNumber/userno