# Lesson 1.1:

### 0. Lesson goals and grading scope
The main goal of this lesson is to create basic miceoservice, build it with docker and deploy with docker compose.

### Task 1. Web API creation
Set up a basic microservice project using ASP.NET Core, following best practices for modularity, scalability, and maintainability.

1.0 Open a terminal or a Visual Studio or a Rider and create a new Web API project:
`dotnet new webapi -n {ServiceName}`

1.1 Implement basic set of classes that will represent service data
1.2 Create a Controller that will provide us with a basic mock functionality.
1.3 Ensure that `Swagger` is added to project and it can be acessed by building and running the service

### Task 2. Dockerize the service

2.0 Create a Dockerfile if it was not created automatically:

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["{ServiceFolder}/{ServiceProject}.csproj", "{ServiceFolder}/"]
RUN dotnet restore "{ServiceFolder}/{ServiceProject}.csproj"
COPY . .
WORKDIR "/src/{ServiceFolder}"
RUN dotnet build "{ServiceProject}.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "{ServiceProject}.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "{ServiceProject}.dll"]

2.1 Create a docker-compose.yml file

services:
{servicename}:
build: ./{ServiceFolder}
ports:
- "5001:8080"


2.2 Run application with Docker Compose
`docker-compose up --build`

2.3 Test Application API.




