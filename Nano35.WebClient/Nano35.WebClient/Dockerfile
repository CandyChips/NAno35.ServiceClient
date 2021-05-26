FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o output -p:PublishTrimmed=false
FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=build /app/output/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf