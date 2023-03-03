FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

ARG APP_PORT=""
ENV ASPNETCORE_URLS=http://+:${APP_PORT}

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ARG SRC_DIR="src"
ARG APP_NAME=""
ARG APP_PATH=""

COPY config/ /config/
COPY ${SRC_DIR}/ /src/
WORKDIR /src

RUN dotnet restore "${APP_PATH}${APP_NAME}/${APP_NAME}.csproj"
RUN dotnet publish "/src/${APP_PATH}${APP_NAME}/${APP_NAME}.csproj" -c Release -o /app/publish --no-restore
RUN echo "#!/bin/bash\nset -e\nruncmd=\"dotnet ${APP_NAME}.dll\"\nexec \$runcmd" > /app/publish/entrypoint.sh && chmod +x /app/publish/entrypoint.sh

FROM base AS final

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80/tcp
ENTRYPOINT ["./entrypoint.sh"]