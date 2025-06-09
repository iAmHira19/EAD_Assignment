# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Make sure to copy from the correct relative path
COPY ["ExamManagementSystem/ExamManagementSystem.csproj", "ExamManagementSystem/"]

RUN dotnet restore "ExamManagementSystem/ExamManagementSystem.csproj"
COPY . .
WORKDIR "/src/ExamManagementSystem"
RUN dotnet build "ExamManagementSystem.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ExamManagementSystem.csproj" -c Release -o /app/publish

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "ExamManagementSystem.dll"]
