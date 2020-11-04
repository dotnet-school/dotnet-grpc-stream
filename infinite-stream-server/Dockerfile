FROM node:12

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
COPY package*.json ./

RUN npm install
# RUN npm ci --only=production

# Bundle app source
COPY . .

EXPOSE 50051
CMD [ "node", "server/index.js" ]