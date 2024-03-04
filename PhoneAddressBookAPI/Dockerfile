# Start with the base .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0.406

# Set the working directory in the container
WORKDIR /app

# Copy all the project files to the container
COPY . .

# Expose the port your app runs on
EXPOSE 5175

WORKDIR ./PhoneAddressBookAPI

CMD ["dotnet", "run"]