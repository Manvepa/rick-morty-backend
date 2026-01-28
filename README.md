# rick-morty-backend
# Rick & Morty Backend - ASP.NET Core + Docker

Backend desarrollado en ASP.NET Core que funciona como capa intermedia entre un frontend y la API pública de Rick & Morty.

## Arquitectura

Flujo de datos:

Frontend  
→ Backend ASP.NET Core  
→ Rick & Morty API (externa)  
→ Backend  
→ Frontend

## Tecnologías

- ASP.NET Core (.NET 10 LTS)
- Docker
- Docker Compose
- GitHub
- Rick & Morty Public API

## Requisitos

- Docker instalado
- Git (opcional, para clonar el repositorio)

## Ejecución del proyecto

Clonar el repositorio:

```bash
git clone https://github.com/TU_USUARIO/rick-morty-backend-dotnet.git
cd rick-morty-backend-dotnet
docker-compose down -v
docker-compose up --build

# Rick & Morty Backend API

Backend desarrollado en **ASP .NET Core** que expone una API REST para consultar personajes de Rick & Morty.  
La aplicación utiliza **MySQL** para persistencia de datos y se ejecuta completamente en **Docker** para garantizar consistencia de entorno.

---

##  Arquitectura General

- **ASP .NET Core Web API**
- **Entity Framework Core**
- **MySQL 8**
- **Docker & Docker Compose**
- **Seed automático de datos**
- **Arquitectura en capas simple**



Autor

Manuel Parra
Prueba técnica Backend - ASP.NET Core