# Cost Optimization & FinOps

> What was observed in the running cluster and the resource/autoscaling changes
> recommended to reduce GKE cost without sacrificing availability.
>
> **Note:** Resource requests/limits, HPA scaling behavior, and the lowered
> replica baseline (4 → 2) have been right-sized from observed metrics and
> **applied** to the manifests.

---

## Table of Contents
1. [Observed Metrics](#1-observed-metrics)
2. [Optimizations Recommended](#2-optimizations-recommended)
3. [Current vs. Recommended](#3-current-vs-recommended)
4. [Estimated Impact](#4-estimated-impact)

---

## 1. Observed Metrics

Captured with `kubectl top pods -n attendance --containers` and `kubectl top nodes`:

| Tier | Observed CPU | Observed Memory |
|------|--------------|-----------------|
| API (per pod) | ~1–2m | ~66–71Mi working set |
| Database | ~4m | ~21Mi |
| Nodes (3) | 102–129m (10–13%) | 1189–1372Mi (42–49%) |

**Key insight:** nodes sit at **42–49% memory** but only **10–13% CPU**, so
**memory is the binding resource**. Reducing memory requests is what actually
allows tighter bin-packing or dropping a node — trimming CPU alone would not.

The pods were running far below their original requests, meaning the requests
were over-provisioning the cluster.

---

## 2. Optimizations Recommended

1. **Lower the HPA replica floor (4 → 2).** The API runs 24/7, so the idle
   baseline dominates the bill. Halving `minReplicas` cuts steady-state compute
   roughly in half during off-peak hours, while the HPA still bursts to 8 pods
   under load.

2. **Right-size requests to observed usage.** Requests drive scheduling and node
   count, not actual usage. Measured usage was far below the configured requests,
   so CPU/memory requests could be trimmed to sit just above idle, letting more
   pods pack onto each node so fewer/smaller nodes are needed.

3. **Add HPA scale-down behavior to prevent flapping.** Without a stabilization
   window, replicas oscillate and extra pods (and their cost) linger after a
   spike. A 300s scale-down window with a 1-pod/60s step would ensure capacity is
   actually reclaimed once load subsides, while a fast 30s scale-up keeps
   performance responsive.

4. **Cap burst limits sensibly.** CPU limits allow short bursts (compressible)
   before the HPA reacts, while memory limits stay close to the request to avoid
   reserving memory that is never used — improving node memory density.

---

## 3. Current vs. Recommended

### API tier — [app-deployment.yaml](../k8s/app-deployment.yaml)

| Setting | Original | Applied | Recommended |
|---------|----------|---------|-------------|
| CPU request | 100m | **25m** | 25m |
| Memory request | 128Mi | **96Mi** | 96Mi |
| CPU limit | 300m | **250m** | 250m |
| Memory limit | 256Mi | **192Mi** | 192Mi |
| Deployment replicas (baseline) | 4 | **2** | 2 |

### Database tier — [db-statefulset.yaml](../k8s/db-statefulset.yaml)

| Setting | Original | Applied | Recommended |
|---------|----------|---------|-------------|
| CPU request | 200m | **50m** | 50m |
| Memory request | 256Mi | **128Mi** | 128Mi |
| CPU limit | 500m | **250m** | 250m |
| Memory limit | 512Mi | **256Mi** | 256Mi |

### Autoscaling — [hpa.yaml](../k8s/hpa.yaml)

| Setting | Original | Applied | Recommended |
|---------|----------|---------|-------------|
| Min replicas | 4 | **2** | 2 |
| Max replicas | 8 | 8 | 8 |
| Scale-down behavior | (default) | **300s window, 1 pod / 60s** | same |
| Scale-up behavior | (default) | **30s window, 2 pods / 30s** | same |

---

## 4. Estimated Impact

- **Idle compute roughly halved** for the API tier with the baseline lowered
  from 4 → 2 replicas — the dominant cost for a 24/7 service.
- **Aggregate CPU requests** for the API baseline would drop from
  `4 × 100m = 400m` to `2 × 25m = 50m` (~88% lower reserved CPU at idle).
- **Aggregate memory requests** for the API baseline would drop from
  `4 × 128Mi = 512Mi` to `2 × 96Mi = 192Mi` (~63% lower reserved memory at idle).
- Tighter requests increase schedulable density, creating headroom to **reduce
  the node pool** via Cluster Autoscaler.
