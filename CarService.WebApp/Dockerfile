#STAGE 1
FROM node:latest as build
WORKDIR /usr/src/app
COPY CarService.WebApp/package*.json ./
RUN npm install
RUN npm install -g @angular/cli@latest
COPY /CarService.WebApp .
RUN npm run build

#STAGE 2
FROM nginx:latest
COPY /CarService.WebApp/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/car-service/browser /usr/share/nginx/html
