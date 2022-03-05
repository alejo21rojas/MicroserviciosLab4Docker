#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Unillanos.ArquitecturaMS.Usuarios/Unillanos.ArquitecturaMS.Usuarios.csproj", "Unillanos.ArquitecturaMS.Usuarios/"]
RUN dotnet restore "Unillanos.ArquitecturaMS.Usuarios/Unillanos.ArquitecturaMS.Usuarios.csproj"
COPY . .
WORKDIR "/src/Unillanos.ArquitecturaMS.Usuarios"
RUN dotnet build "Unillanos.ArquitecturaMS.Usuarios.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Unillanos.ArquitecturaMS.Usuarios.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unillanos.ArquitecturaMS.Usuarios.dll"]