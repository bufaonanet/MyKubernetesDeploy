#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/PizzaFrontEnd/PizzaFrontEnd.csproj", "src/PizzaFrontEnd/"]
RUN dotnet restore "src/PizzaFrontEnd/PizzaFrontEnd.csproj"
COPY . .
WORKDIR "/src/src/PizzaFrontEnd"
RUN dotnet build "PizzaFrontEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PizzaFrontEnd.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaFrontEnd.dll"]