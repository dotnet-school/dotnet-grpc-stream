Build docker images

```
cd GrpcServer
docker build -t grpc-server .

cd ../GrpcClientConsole/
docker build -t grpc-client .
```

Run server

```
docker run -p 5000:80 grpc-server
```

Run clien

```
docker run grpc-client 
```

