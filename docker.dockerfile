# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar y restaurar dependencias
COPY BrawlToonsAPI/*.csproj ./BrawlToonsAPI/
WORKDIR /app/BrawlToonsAPI
RUN dotnet restore

# Copiar el resto del código y compilar
COPY . ./
RUN dotnet publish -c Release -o /out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./

# Configurar la aplicación para escuchar en el puerto proporcionado por Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Comando para ejecutar la aplicación
CMD ["dotnet", "BrawlToonsAPI.dll"]
