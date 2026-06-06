# Build Angular
FROM node:22 AS build

WORKDIR /app

COPY . .

RUN npm install

RUN npm run build

# Nginx
FROM nginx:alpine

COPY --from=build /app/dist/*/browser /usr/share/nginx/html

EXPOSE 80