# mydock/hello-sql

Hello World container that you can use as initial image for Cloud Run / Container Apps.

It listens for HTTP requests on port `env.PORT` (or 8080).

Pass in environment variable `CONNECTION_STRING` to test database connectivity.

It will return the full exception as response instead of "Hello world" on connection errors.

You can also pass in `SQL_QUERY_1` to override the default SQL query (`SELECT 1`)

```sh
docker run -itp 8080:8080 ghcr.io/mydock/hello-sql:v1
```
