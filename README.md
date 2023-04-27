# HomeRunBackendCase

 ![HomeRunk8sArchitecture](https://user-images.githubusercontent.com/31182762/235013128-1be367ac-33ae-4e47-bc96-8e66f0add8a5.png)

 Node ports added for external access to apis.  
 Loadbalancers added for externel access to rabbitmq and sql server pods.  
 
 

# Service Descriptions

Ratings Service

Create-Read ServiceProviders
Create Ratings for ServiceProviders
Read ServiceProviders' ratings
Read ServiceProviders' average rating points.

Notification Service

When a new rating is added with the rating service, a message is sent to the notification service with the amqp protocol.   
Thee notification service records these ratings. Rating notification can only be seen once then flagged as seen by api.



Possible Improvements  

* A service layer can be added between the controller and the repository to have cleaner architecture.  
 
# How to Deploy  
  
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"  
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.7.0/deploy/static/provider/cloud/deploy.yaml  
kubectl apply -f local-pvc.yaml  
kubectl apply -f mssql-plat-depl.yaml  
kubectl apply -f ratings-depl.yaml  
kubectl apply -f ratings-np-srv.yaml  
kubectl apply -f rabbitmq-depl.yaml  
kubectl apply -f notifications-depl.yaml  
kubectl apply -f notifications-np-srv.yaml  
kubectl apply -f ingress-srv.yaml  
  
Add acme.com to machine's host file to access with nginx or else apis are accesible via nodeports.
