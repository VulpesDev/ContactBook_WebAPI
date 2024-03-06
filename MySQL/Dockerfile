# Use the official MySQL image from Docker Hub
FROM mysql:latest

# Set environment variables for MySQL configuration
ENV MYSQL_ROOT_PASSWORD=root1234!
ENV MYSQL_DATABASE=PhoneAddressData

# Copy the SQL script to initialize the database
COPY ./MySQL/create_database.sql /create_database.sql
COPY ./MySQL/run_sql.sh /run_sql.sh

# Expose port 3306 to the outside world
EXPOSE 3306


