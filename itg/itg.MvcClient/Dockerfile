FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.5-alpine3.12 AS base
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.301-alpine3.12 AS builder
LABEL stage="intermediate"
WORKDIR /src

COPY . .
WORKDIR "./itg/itg.MvcClient"

FROM builder AS publish
RUN dotnet publish "itg.MvcClient.csproj" -c Debug  -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
LABEL project="mitp"

ENTRYPOINT ["dotnet", "itg.MvcClient.dll"]
