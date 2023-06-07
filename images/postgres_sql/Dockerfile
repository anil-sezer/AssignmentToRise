FROM postgres
EXPOSE 5432

#COPY ./images/postgres_sql/config.sql /docker-entrypoint-initdb.d/
#COPY ./config.sql /docker-entrypoint-initdb.d/
COPY ["images/postgres_sql", "/docker-entrypoint-initdb.d/"]
