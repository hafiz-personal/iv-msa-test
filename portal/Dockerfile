FROM node:16.8.0

ENV APP_ROOT /src

RUN mkdir ${APP_ROOT}
WORKDIR ${APP_ROOT}
ADD . ${APP_ROOT}

RUN npm install
RUN npm run build-dev
ENV NODE_ENV dev
ENV ENV_FILE dev

ENV NUXT_PORT=3000
ENV HOST 0.0.0.0

EXPOSE 3000
CMD [ "npm", "run", "start-dev" ]