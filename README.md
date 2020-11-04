Create docker images

```bash
cd infinite-stream-server
docker build -t stream-server .

cd ../GrpcClientConsole
docker build -t stream-client .
```



Check stream server

```
docker run   \
	-p 50051:50051 \
	stream-server
	
	docker run stream-client
```



Run stream server

```
docker run  -i 
	-p 50051:50051 \
	--network=demo-net \
	stream-server

docker run \
  --network=demo-net\
  stream-client

```



