# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar os arquivos .csproj e restaurar as dependências
COPY DesafioPitang.Service/*.csproj DesafioPitang.Business/
COPY DesafioPitang.IService/*.csproj DesafioPitang.IBusiness/
COPY DesafioPitang.Entity/*.csproj DesafioPitang.Entities/
COPY DesafioPitang.Repository/*.csproj DesafioPitang.Repository/
COPY DesafioPitang.IRepository/*.csproj DesafioPitang.IRepository/
COPY DesafioPitang.IRepository/*.csproj DesafioPitang.UnitTests/
COPY DesafioPitang.Utils/*.csproj DesafioPitang.Utils/
COPY DesafioPitang.IRepository/*.csproj DesafioPitang.Validators/
COPY DesafioPitang.WebApi/*.csproj DesafioPitang.WebApi/

RUN dotnet restore DesafioPitang.WebApi/DesafioPitang.WebApi.csproj

# Copiar o restante dos arquivos e construir a aplicação
COPY . .
RUN dotnet publish DesafioPitang.WebApi/DesafioPitang.WebApi.csproj -c Release -o /out

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Configurar a variável de ambiente
ENV ASPNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "DesafioPitang.WebApi.dll"]
