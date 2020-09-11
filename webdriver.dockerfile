FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder

WORKDIR /app
COPY . .

RUN dotnet build -o ./build ./Sources/WebDriver.sln

COPY ./Sources/nuget.config ./Sources/SeleniumWebDriver/
RUN dotnet publish ./Sources/SeleniumWebDriver/SeleniumWebDriver.csproj -o /publish

ENTRYPOINT dotnet SeleniumWebDriver.dll
