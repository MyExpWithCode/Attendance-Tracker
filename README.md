# Attendance-Tracker

## Local Setup using Docker Compose

### Prerequisites

- Docker Desktop installed and running

### Run

```bash
cd c:\nithesh\at
docker-compose up --build
```

This starts:
- **PostgreSQL** on `localhost:5433`
- **Attendance Service API** on `http://localhost:5000`

### Stop

```bash
docker-compose down
```

To also remove the database volume:

```bash
docker-compose down -v
```