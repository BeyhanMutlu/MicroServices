# Microservice architecture project
In this project, a fully functional demo application has been developed from scratch using microservice architecture principles. The project demonstrates the creation, deployment, and operation of microservices to build a cohesive software system.

## Installation

- If you have Docker Desktop, you can download Docker images. Start Docker Desktop and ensure that the Docker daemon is running. Open a terminal or command prompt.

![Docker Image Size (tag)](https://img.shields.io/docker/image-size/beyhanm/productionservice/latest)
```bash
docker pull beyhanm/productionservice:latest
```
![Docker Image Size (tag)](https://img.shields.io/docker/image-size/beyhanm/orderservice/latest)
```bash
docker pull beyhanm/orderservice:latest
```
- For installing NGINX Ingress Controller.
```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.0/deploy/static/provider/cloud/deploy.yaml
```
- Locate your hosts file and make the necessary changes to use the NGINX virtual server on our example requests.
```bash
127.0.0.1 localh.com
```
- Creating the password to access the SQL Server.
```bash
kubectl create secret generic mssql --from-literal=SA_PASSWORD="mssql2022!"
```
- For K8S download yaml files and run the scripts.
```bash
kubectl apply -f ingress-srv.yaml
kubectl apply -f local-pvc.yaml
kubectl apply -f mssql-plt-depl.yaml
kubectl apply -f order-depl.yaml
kubectl apply -f production-depl.yaml
kubectl apply -f production-np-srv.yaml
kubectl apply -f rabbitmq-depl.yaml
```
## Examples

### Production Service 
| Method   | URL                                      | Description                              |
| -------- | ---------------------------------------- | ---------------------------------------- |
| `GET`    | `http://localh.com/api/products`         | Retrieve all products.                   |
| `GET`    | `http://localh.com/api/products/1`       | Retrieve id spesific product.            |
| `POST`   | `http://localh.com/api/products`         | Create a new product.                    |

### Order Service 
| Method   | URL                                              | Description                              |
| -------- | ----------------------------------------         | ---------------------------------------- |
| `GET`    | `http://localh.com/api/Ordsrv/product`           | Retrieve all products.                   |
| `GET`    | `http://localh.com/api/Ordsrv/products/1/order`  | Retrieve id spesific product's order.    |
| `POST`   | `http://localh.com/api/Ordsrv/products/1/order`  | Create an order for id spesific product. |




























