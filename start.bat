@echo off
podman stop -a
podman rm -a
podman run -d --name OrderDb -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=OrderDb -p 5434:5432 -v orderdb_data:/var/lib/postgresql/data postgres
podman run -d --name CatalogDb -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=CatalogDb -p 5432:5432 -v catalogdb_data:/var/lib/postgresql/data postgres
pause