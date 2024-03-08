FROM mysql:latest

ENV MYSQL_ROOT_PASSWORD=root1234!
ENV MYSQL_DATABASE=PhoneAddressData

COPY ./MySQL/create_database.sql /create_database.sql
COPY ./MySQL/run_sql.sh /run_sql.sh
# COPY ./MySQL/custom_my.cnf /etc/mysql/my.cnf

EXPOSE 3306

CMD ["--init-file=/create_database.sql"]
