  FROM mcr.microsoft.com/dotnet/core/sdk as build
  WORKDIR /aspnet
  COPY . .
  RUN dotnet build
  RUN dotnet publish --no-restore -c Release -o out

  FROM mcr.microsoft.com/dotnet/core/aspnet
  WORKDIR /dist
  EXPOSE 5000
  EXPOSE 5432
  # ENV ASPNETCORE_URLS=http://*:5000
  ENV ASPNETCORE_ENVIRONMENT=development
  COPY --from=build /aspnet/out .
  RUN ls -a
  CMD ["dotnet", "HighStakes.Client.dll"]