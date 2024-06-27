# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8082
EXPOSE 8443

# Use the official ASP.NET Core SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyIdentityServer.csproj", "./"]
RUN dotnet restore "MyIdentityServer.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MyIdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyIdentityServer.csproj" -c Release -o /app/publish

# Use the runtime image again to create the final container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["https-aspnetcore.pfx", "./"]
ENTRYPOINT ["dotnet", "MyIdentityServer.dll"]
