# K8S

1. `docker build -t konradkaluzny96/platformservice .`
1. `docker push konradkaluzny96/platformservice`
1. `docker build -t konradkaluzny96/commandservice .`
1. `docker push konradkaluzny96/commandservice`
1. `kubectl apply -f platforms-depl.yaml`
1. `kubectl apply -f commands-depl.yaml`
1. `kubectl apply -f platforms-np-srv.yaml`
1. `kubectl get deployments`
1. `kubectl get pods`
1. `kubectl get services`
1. `kubectl rollout restart deployment platforms-depl`
1. `kubectl rollout restart deployment commands-depl`
1. `kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/aws/deploy.yaml`
1. `kubectl get namespace`
1. `kubectl get pods -n ingress-nginx`
1. `kubectl get services -n ingress-nginx`
1. `kubectl apply -f ingress-srv.yaml`
1. `kubectl get storageclass`
1. `kubectl create secret generic mssql --from-literal=SA_PASSWORD="Password123"`
1. `kubectl get secrets`
1. `kubectl apply -f local-pvc.yaml`
1. `kubectl apply -f mssql-plat-depl.yaml`
1. `kubectl apply -f rabbitmq-depl.yaml`
1. `dotnet add package RabbitMQ.Client`
1. `dotnet add package Grpc.AspNetCore`
1. `dotnet add package Grpc.Net.Client`
1. `dotnet add package Grpc.Tools`
1. `dotnet add package Google.Protobuf`
