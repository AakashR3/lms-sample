# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=''
ENV AZ_CONFIG_CS=''
ENV AZ_KEY_VAULT_SEC=''
ENV AZ_AI_INS_KEY=''

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./Tata.IGetIT.Learner.sln ./
COPY ./Tata.IGetIT.Learner.Repository/*.csproj ./Tata.IGetIT.Learner.Repository/
COPY ./Tata.IGetIT.Learner.Repository.Tests/*.csproj ./Tata.IGetIT.Learner.Repository.Tests/
COPY ./Tata.IGetIT.Learner.Service/*.csproj ./Tata.IGetIT.Learner.Service/
COPY ./Tata.IGetIT.Learner.Service.Tests/*.csproj ./Tata.IGetIT.Learner.Service.Tests/
COPY ./Tata.IGetIT.Learner.Web/*.csproj ./Tata.IGetIT.Learner.Web/
COPY ./Tata.IGetIT.Learner.Web.Tests/*.csproj ./Tata.IGetIT.Learner.Web.Tests/


RUN dotnet restore
    
COPY . .
WORKDIR /src/Tata.IGetIT.Learner.Repository
RUN dotnet build -c Release -o /app

WORKDIR /src/Tata.IGetIT.Learner.Repository.Tests
RUN dotnet build -c Release -o /app

WORKDIR /src/Tata.IGetIT.Learner.Service
RUN dotnet build -c Release -o /app

WORKDIR /src/Tata.IGetIT.Learner.Service.Tests
RUN dotnet build -c Release -o /app

WORKDIR /src/Tata.IGetIT.Learner.Web
RUN dotnet build -c Release -o /app

WORKDIR /src/Tata.IGetIT.Learner.Web.Tests
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app
    
FROM base AS final
WORKDIR /app

ADD ./Tata.IGetIT.Learner.Web/Templates /app/Templates
ADD ./Tata.IGetIT.Learner.Web/Downloads/Invoice /app/Downloads/Invoice
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Tata.IGetIT.Learner.Web.dll"]
