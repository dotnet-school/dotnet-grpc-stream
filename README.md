https://github.com/dotnetgik/gRPCDocker/tree/master/Properties



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

