# Step 1: Use .NET SDK to build the solution
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file
COPY MicroserviceApplication.sln ./

# Copy the project file and restore dependencies
COPY MicroserviceApplication/MicroserviceApplication.csproj MicroserviceApplication/
RUN dotnet restore MicroserviceApplication/MicroserviceApplication.csproj

# Copy the entire project and build it
COPY . ./
RUN dotnet publish MicroserviceApplication/MicroserviceApplication.csproj -c Release -o /out

# Step 2: Use runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set working directory
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /out .

# Expose the API port
EXPOSE 8080
EXPOSE 8081

# Run the application
CMD ["dotnet", "MicroserviceApplication.dll"]
