# Auctioneer Platform

This is an open source example of how to use signalr to create a modern bidding system with scaling in mind.

## Starting Redis

Simply run:

```bash
docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 -e "REDIS_ARGS=--requirepass Auctioneer12345" redis/redis-stack:latest
```

You can then use local host creds with no username or password to access redis. This also comes with RedisInsights located here:

[http://localhost:8001](http://localhost:8001)

## Starting SqlServer

Simply run:

```bash
docker run -d --name sql-stack -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Auctioneer12345" -p 1433:1433 -v ./scripts/create-database.sql:/docker-entrypoint-initdb.d mcr.microsoft.com/mssql/server:2022-latest
```