#!/bin/sh
dotnet new sln --name HighStakes
dotnet new mvc --name HighStakes.Client
dotnet new webapi --name HighStakes.Domain
dotnet new classlib --name HighStakes.Storing
dotnet new xunit --name HighStakes.Testing
dotnet add HighStakes.Client/HighStakes.Client.csproj reference HighStakes.Domain/HighStakes.Domain.csproj
dotnet add HighStakes.Domain/HighStakes.Domain.csproj reference HighStakes.Storing/HighStakes.Storing.csproj
dotnet add HighStakes.Client/HighStakes.Client.csproj reference HighStakes.Storing/HighStakes.Storing.csproj
dotnet add HighStakes.Testing/HighStakes.Testing.csproj package coverlet.msbuild
dotnet add HighStakes.Testing/HighStakes.Testing.csproj package moq
dotnet add HighStakes.Storing/HighStakes.Storing.csproj package microsoft.entityframeworkcore --version 2.2.6
dotnet add HighStakes.Storing/HighStakes.Storing.csproj package microsoft.entityframeworkcore.sqlserver --version 2.2.6
dotnet add HighStakes.Storing/HighStakes.Storing.csproj package microsoft.entityframeworkcore.design --version 2.2.6
dotnet add HighStakes.Storing/HighStakes.Storing.csproj package microsoft.entityframeworkcore.tools --version 2.2.6
mkdir HighStakes.Domain/Abstracts
mkdir HighStakes.Domain/Models
mkdir HighStakes.Domain/Interfaces
mkdir HighStakes.Storing/Repositories
mkdir HighStakes.Testing/Mocks
mkdir HighStakes.Testing/Specs
dotnet sln add **/*.csproj