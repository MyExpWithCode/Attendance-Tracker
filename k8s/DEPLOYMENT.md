# Kubernetes Deployment Flow

## Prerequisites

- kubectl configured with cluster access
- NGINX Ingress Controller installed
- Metrics Server installed (for HPA)
- Docker image pushed: `nnitheshreddy/attendance-tracker:v1`

## Deployment Order

```bash
# 1. Create namespace
kubectl apply -f k8s/namespace.yaml

# 2. Create StorageClass for persistent volumes
kubectl apply -f k8s/storageclass.yaml

# 3. Create Secret (DB credentials)
kubectl apply -f k8s/secret.yaml

# 4. Create ConfigMap (app + DB configuration)
kubectl apply -f k8s/configmap.yaml

# 5. Create headless service for DB (must exist before StatefulSet)
kubectl apply -f k8s/db-service.yaml

# 6. Deploy PostgreSQL as StatefulSet
kubectl apply -f k8s/db-statefulset.yaml

# 7. Wait for DB to be ready
kubectl wait --namespace attendance --for=condition=ready pod/attendance-db-0 --timeout=120s

# 8. Deploy application (4 replicas with rolling update)
kubectl apply -f k8s/app-deployment.yaml

# 9. Create ClusterIP service for the app
kubectl apply -f k8s/app-service.yaml

# 10. Create Ingress for external access
kubectl apply -f k8s/ingress.yaml

# 11. Enable Horizontal Pod Autoscaler
kubectl apply -f k8s/hpa.yaml
```

## Verify Deployment

```bash
# Check all resources
kubectl get all -n attendance

# Check pods are running
kubectl get pods -n attendance

# Check PVC is bound
kubectl get pvc -n attendance

# Check Ingress external IP
kubectl get ingress -n attendance

# Check HPA status
kubectl get hpa -n attendance
```

## Architecture Flow

```
Internet
    ↓
Ingress (NGINX) → External IP
    ↓
app-service (ClusterIP :80)
    ↓
attendance-service Deployment (4 pods :8080)
    ↓
attendance-db Service (Headless :5432)
    ↓
attendance-db StatefulSet (1 pod)
    ↓
PVC → PV (5Gi pd-standard)
```

## Teardown

```bash
kubectl delete namespace attendance
kubectl delete storageclass attendance-sc
```
