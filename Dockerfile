# tag for x86 3.1-alpine
FROM mcr.microsoft.com/dotnet/sdk:3.1-buster-arm64v8
EXPOSE 50001
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /
COPY . .
RUN dotnet build -c Release -o /src/build

WORKDIR /src/UserAccess.API
# RUN dotnet pack -o /src/publish
RUN dotnet publish "UserAccess.API.csproj" -o /app/publish -c Debug

WORKDIR /app/publish

ENTRYPOINT [ "dotnet", "UserAccess.API.dll" ]
