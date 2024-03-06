#!/bin/bash

echo "Waiting for MySQL to startup!"
sleep 10

echo "Creating sample database and tables."
# Execute the SQL script using the MySQL command-line client
mysql -p3306 < /create_database.sql
