# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar y restaurar dependencias
COPY BrawlToonsAPI/*.csproj BrawlToonsAPI/
WORKDIR /src/BrawlToonsAPI
RUN dotnet restore

# Copiar el resto del código y compilar
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Configurar la aplicación para escuchar en el puerto correcto
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Comando para ejecutar la aplicación
CMD ["dotnet", "BrawlToonsAPI.dll"]
