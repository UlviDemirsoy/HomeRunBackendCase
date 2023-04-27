# HomeRunBackendCase

 ![HomeRunk8sArchitecture](https://user-images.githubusercontent.com/31182762/235013128-1be367ac-33ae-4e47-bc96-8e66f0add8a5.png)

 Node ports added to external access to apis.
 Loadbalancers added to externel access to rabbitmq and sql server pods.
 
 
Possible Improvements

* A service layer can be added between the controller and the repository to have cleaner architecture.
 


How to Deploy
  
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

add acme.com to machine's host file to access with nginx.
