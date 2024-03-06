#!/bin/bash

# Set up the MySQL database using the initialization file
if [ ! -f /var/lib/mysql/initialized ]; then
    echo "Initializing MySQL database..."
    mysql_install_db --user=mysql --ldata=/var/lib/mysql
    touch /var/lib/mysql/initialized
    /usr/bin/mysqld --initialize-insecure --user=mysql --datadir=/var/lib/mysql
    /usr/bin/mysqld --user=mysql --bootstrap --datadir=/var/lib/mysql --verbose=0 < /create_database.sql
fi

# Start MySQL
exec "$@"
