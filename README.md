# ASP.NET Course (072025) — Repository Overview

This document summarizes the repository branches and provides short explanations of the subjects covered throughout the course.

## Branches

- master
  - Baseline branch intended as the starting point for the course. Typically contains the minimal project setup to introduce concepts incrementally.
- aspire
  - Focused on .NET Aspire patterns for building distributed applications. Typically includes an AppHost and one or more service projects to demonstrate service discovery, shared configuration/telemetry, and local orchestration.
- use-compose
  - Demonstrates running the solution with Docker Compose. Emphasizes containerized development, service orchestration, and environment configuration via compose files.

## Subjects

- Docker
  - Docker packages your application and its dependencies into portable images so you can run the same container consistently across environments. It helps isolate services, enables reproducible builds, and simplifies local/dev/test parity.
  
- Dockerfile
  - A Dockerfile defines the steps to build your image (base image, restore, build, publish, and final runtime). Multi‑stage builds keep images small and secure by separating build and runtime layers.
  
- Docker Compose
  - Docker Compose describes and runs multi‑container applications using a single YAML file. You define services, networks, and volumes, and can override settings per environment for easy orchestration.
  
- ASP.NET Aspire (.NET Aspire)
  - .NET Aspire provides a curated set of components and tooling for building cloud‑native, distributed .NET applications. It streamlines local orchestration, service discovery, configuration, and telemetry with sensible defaults and dashboards.
  
- Middleware
  - Middleware are components in the ASP.NET Core request/response pipeline that handle cross‑cutting concerns (logging, auth, CORS, compression, etc.). Order matters—each middleware can short‑circuit or pass control to the next component.
  
- Endpoint Exporter
  - Endpoint exporters expose observability data such as metrics, traces, or logs to external systems (e.g., Prometheus via /metrics, OTLP for OpenTelemetry backends). In ASP.NET Core (and often via Aspire defaults), exporters make it easy to integrate with monitoring stacks.
  
- Configuration and IOptions
  - ASP.NET Core configuration binds settings from multiple sources (appsettings.json, environment variables, user secrets, etc.) into strongly‑typed objects using the Options pattern. This keeps settings centralized, testable, and decoupled from business logic.
  
- IOptionsMonitor
  - IOptionsMonitor provides change‑tracking and notifications when configuration is reloaded, allowing services to react to updates without restarts. It differs from IOptions (static snapshot at startup) by offering live, thread‑safe access to current values.
  
- Health Checks
  - Health checks provide liveness/readiness endpoints that report service and dependency status. They integrate with orchestrators (Docker/Kubernetes) and monitoring systems to enable safe rollouts and automated recovery.
  
- OpenAPI/Swagger
  - OpenAPI documents your HTTP APIs so tools like Swagger UI can render interactive documentation. It improves discoverability, client generation, and validation across environments.
  
- Minimal APIs
  - Minimal APIs offer a lightweight way to define HTTP endpoints with minimal ceremony, ideal for microservices and small HTTP services. They embrace top‑level statements and dependency injection while remaining production‑ready.
  
- Dependency Injection
  - ASP.NET Core has built‑in DI to manage service lifetimes and dependencies (Singleton/Scoped/Transient). Proper DI leads to more testable, modular, and maintainable code.
  
- Entity Framework Core (EF Core)
  - Entity Framework Core is an Object-Relational Mapping (ORM) framework that enables .NET developers to work with databases using .NET objects. It supports multiple database providers, migrations for schema changes, LINQ queries, change tracking, and both Code First and Database First approaches for data modeling.
  
- Logging and OpenTelemetry
  - Structured logging and OpenTelemetry tracing/metrics give end‑to‑end visibility into your services. With exporters and collectors, you can stream observability data to platforms like Grafana, Prometheus, or Azure Monitor.