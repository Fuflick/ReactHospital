FROM node:21 as build

WORKDIR /usr/bin/app

COPY package*.json ./

RUN npm i

COPY . .

RUN npm run build

FROM nginx:stable-alpine

COPY --from=build /usr/bin/app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]