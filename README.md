# AccioData29 - Hexagonal Architecture Template

Template de .NET 8 para crear APIs REST siguiendo el patrón de **Arquitectura Hexagonal (Ports & Adapters)**. Incluye una estructura de proyecto completa con capas bien definidas, manejo de excepciones, Entity Framework Core con PostgreSQL, AutoMapper, Swagger y pruebas unitarias.

---

## Instalación

### Desde GitHub Packages

El paquete se publica en GitHub Packages. Necesitás un token de GitHub con permiso `read:packages` ([generarlo aquí](https://github.com/settings/tokens)).

Instalá el template especificando la fuente directamente:

```bash
dotnet new install AccioData29 \
  --nuget-source "https://TU_USUARIO_GITHUB:TU_GITHUB_TOKEN@nuget.pkg.github.com/AccioData29/index.json"
```

O bien, agregá la fuente una sola vez y luego instalá normalmente:

```bash
# Paso 1 — agregar la fuente (una sola vez)
dotnet nuget add source \
  --username TU_USUARIO_GITHUB \
  --password TU_GITHUB_TOKEN \
  --store-password-in-clear-text \
  --name github \
  "https://nuget.pkg.github.com/AccioData29/index.json"

# Paso 2 — instalar el template
dotnet new install AccioData29 --nuget-source github
```

### Desde un archivo `.nupkg` local

```bash
dotnet new install ./AccioData29.1.0.0.nupkg
```

---

## Uso

Una vez instalado el template, crea un nuevo proyecto reemplazando `Company` y `ProjectName` con los valores de tu solución:

```bash
dotnet new hexagonal-arch --name Company.ProjectName
```

Ejemplo:

```bash
dotnet new hexagonal-arch --name Acme.Inventory
```

Esto generará una solución completa con todos los proyectos renombrados automáticamente.

---

## Estructura del proyecto generado

```
Tu.Proyecto/
├── src/
│   ├── Tu.Proyecto.API/              # Capa de entrada HTTP (Controllers, Middlewares)
│   ├── Tu.Proyecto.Application/      # Lógica de negocio, interfaces, DTOs, excepciones
│   ├── Tu.Proyecto.Domain/           # Entidades y enums de dominio (sin dependencias externas)
│   ├── Tu.Proyecto.Host/             # Configuración e inyección de dependencias
│   ├── Tu.Proyecto.Infraestructure/  # Repositorios, DbContext, EF Core
│   ├── Tu.Proyecto.Shared/           # Respuestas genéricas y utilidades transversales
│   └── Tu.Proyecto.Tests/            # Pruebas unitarias con xUnit + Moq + FluentAssertions
└── Tu.Proyecto.sln
```

---

## Arquitectura

El template implementa **Arquitectura Hexagonal** con separación estricta entre el núcleo de negocio y los adaptadores externos.

```
┌──────────────────────────────────────────────┐
│                   Adapters                   │
│  ┌─────────┐  ┌────────────┐  ┌──────────┐  │
│  │   API   │  │   Tests    │  │  Host    │  │
│  └────┬────┘  └─────┬──────┘  └────┬─────┘  │
│       │              │              │         │
│  ┌────▼──────────────▼──────────────▼─────┐  │
│  │              Application               │  │
│  │    (Interfaces / Services / DTOs)      │  │
│  ├────────────────────────────────────────┤  │
│  │                 Domain                 │  │
│  │        (Entities / Enums / Rules)      │  │
│  └────────────────────────────────────────┘  │
│       │              │              │         │
│  ┌────▼────┐  ┌───────▼──────┐             │
│  │  Shared │  │Infrastructure │             │
│  └─────────┘  └───────────────┘             │
└──────────────────────────────────────────────┘
```

### Flujo de datos

```
HTTP Request
  → Controller (API)
  → IService (Application)
  → IUnitOfWork / IRepository (Ports)
  → Repository + DbContext (Infrastructure)
  → PostgreSQL

Response
  → Entity → AutoMapper → DTO → ApiResponse<T> → HTTP Response
```

---

## Configuración

### Cadena de conexión

Edita `src/Tu.Proyecto.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=tu_base_de_datos;Username=postgres;Password=tu_password"
  }
}
```

### Migraciones

Las migraciones se aplican **automáticamente al iniciar la aplicación**. Para crear nuevas migraciones:

```bash
dotnet ef migrations add NombreMigracion --project src/Tu.Proyecto.Infraestructure --startup-project src/Tu.Proyecto.API
```

---

## Ejecutar el proyecto

```bash
cd src/Tu.Proyecto.API
dotnet run
```

La API estará disponible en `https://localhost:{puerto}` y Swagger en `https://localhost:{puerto}/swagger` (solo en entorno Development).

---

## Ejecutar las pruebas

```bash
dotnet test
```

El proyecto de pruebas usa una base de datos **en memoria** (EF Core InMemory), por lo que no requiere una instancia de PostgreSQL.

---

## Endpoints de ejemplo (Product)

El template incluye un CRUD de `Product` como ejemplo de implementación:

| Método | Ruta                  | Descripción              |
|--------|-----------------------|--------------------------|
| GET    | `api/v1/products`     | Listar todos los productos |
| GET    | `api/v1/products/{id}`| Obtener producto por ID  |
| POST   | `api/v1/products`     | Crear producto           |
| PUT    | `api/v1/products/{id}`| Actualizar producto      |
| DELETE | `api/v1/products/{id}`| Eliminar producto        |

### Respuesta estándar

Todas las respuestas siguen el formato `ApiResponse<T>`:

```json
{
  "statusCode": 200,
  "message": "Operación exitosa",
  "result": { ... }
}
```

Los errores incluyen el campo `errors` con la lista de validaciones o el mensaje de excepción.

---

## Excepciones de dominio

El template provee excepciones tipadas que el middleware convierte automáticamente en respuestas HTTP:

| Excepción               | Código HTTP |
|-------------------------|-------------|
| `NotFoundException`     | 404         |
| `DuplicateException`    | 409         |
| `UniqueConstraintException` | 409    |
| `ValidationException`   | 422         |

---

## Tecnologías incluidas

| Tecnología                         | Versión  | Propósito                        |
|------------------------------------|----------|----------------------------------|
| .NET                               | 8.0      | Framework base                   |
| Entity Framework Core              | 8.0.11   | ORM                              |
| Npgsql.EntityFrameworkCore.PostgreSQL | 8.0.11 | Proveedor PostgreSQL            |
| AutoMapper                         | 13.0.1   | Mapeo de objetos                 |
| Swashbuckle.AspNetCore             | 6.8.1    | Documentación Swagger/OpenAPI    |
| xUnit                              | 2.9.2    | Framework de pruebas             |
| Moq                                | 4.20.72  | Mocking en pruebas               |
| FluentAssertions                   | 8.8.0    | Aserciones en pruebas            |

---

## Publicar una nueva versión

El paquete se publica automáticamente en GitHub Packages al crear un tag con el prefijo `v`:

```bash
git tag v1.0.0
git push origin v1.0.0
```

El workflow `.github/workflows/publish.yml` ejecuta `dotnet pack` y sube el `.nupkg` generado al registro de GitHub Packages de la organización.

---

## Desinstalar el template

```bash
dotnet new uninstall AccioData29
```

---

## Autor

**AccioData29**
