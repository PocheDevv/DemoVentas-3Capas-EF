# DemoVentas — Sistema de Ventas en 3 Capas

Aplicación web de gestión de ventas desarrollada con **ASP.NET Web Forms** y **Entity Framework 6**, siguiendo una arquitectura clásica de **3 capas**. Proyecto realizado en el curso de Desarrollo de Aplicaciones (ISIL).

---

## Arquitectura

El proyecto separa las responsabilidades en cuatro proyectos dentro de una misma solución:

```
┌─────────────────────────────────────────┐
│   SitioWEB_VentasGUI  (Presentación)     │  Páginas .aspx, Login, Dashboard
└───────────────────┬─────────────────────┘
                    │
┌───────────────────▼─────────────────────┐
│   ProyVentas_BL   (Lógica de Negocio)    │  Reglas y validaciones
└───────────────────┬─────────────────────┘
                    │
┌───────────────────▼─────────────────────┐
│   ProyVentas_ADO  (Acceso a Datos)       │  Entity Framework (VentasLeon.edmx)
└───────────────────┬─────────────────────┘
                    │
        ┌───────────▼───────────┐
        │   SQL Server: VentasLeon │
        └───────────────────────┘

   ProyVentas_BE (Entidades)  →  usado por todas las capas
```

| Proyecto | Rol | Contenido | asdfasdf
|---|---|---|
| **ProyVentas_BE** | Business Entities | Clases POCO: `ClienteBE`, `ProductoBE`, `FacturaBE`, `OrdenBE`, `UsuarioBE`, `DashboardBE` |
| **ProyVentas_ADO** | Acceso a datos | Modelo Entity Framework `VentasLeon.edmx` (database-first) y clases `*ADO` |
| **ProyVentas_BL** | Lógica de negocio | Clases `*BL` que intermedian entre la GUI y el acceso a datos |
| **SitioWEB_VentasGUI** | Presentación | Sitio web ASP.NET Web Forms (`.aspx`) |

---

## Funcionalidades

- **Autenticación** con Forms Authentication (Login / Logout, protección de páginas).
- **Dashboard** con indicadores calculados por LINQ (total de ventas, total de deuda, clientes activos).
- **Facturación de cliente** — consulta de facturas por cliente con paginación.
- **Registro de órdenes de compra** a proveedores.
- **Exportación a Excel** de listados usando EPPlus.
- **Gráficos** con System.Web.DataVisualization (Charting).

---

## Tecnologías

- .NET Framework 4.8
- ASP.NET Web Forms
- Entity Framework 6.5.2 (database-first, `.edmx`)
- SQL Server
- EPPlus 8.6.1 (exportación a Excel)
- AjaxControlToolkit 20.1.0

---

## Base de datos

El modelo es **database-first**. El script completo de la base de datos (estructura + datos + procedimientos almacenados) está en:

```
Database/VentasLeon.sql
```

Sus tablas principales: `Tb_Cliente`, `Tb_Producto`, `Tb_Proveedor`, `Tb_Factura`, `Tb_Detalle_Factura`, `Tb_Orden_Compra`, `Tb_Detalle_Compra`, `Tb_Abastecimiento`, `Tb_Categoria`, `Tb_Usuario`, `Tb_Vendedor`, `Tb_Ubigeo`, `Tb_UnidadMedida`, `Tb_RepositorioDocumentos`.

**Para crear la base:** abre `Database/VentasLeon.sql` en SQL Server Management Studio (SSMS) y ejecútalo (F5).

> **Importante:** el script crea los archivos de la base en la ruta `C:\BD_VentasLeon_Aplicaciones\`. Si esa carpeta no existe, créala antes de ejecutar, o edita las líneas `FILENAME = N'...'` del `CREATE DATABASE` para apuntar a tu ruta preferida (o bórralas para usar la ubicación por defecto de SQL Server).

**Usuarios de prueba** (para iniciar sesión en la aplicación):

| Usuario | Contraseña | Nivel |
|---|---|---|
| `jleon` | `jleon` | Administrador |
| `cmontoya` | `12345` | Usuario |

---

## Cómo ejecutar

1. Clona el repositorio:
   ```bash
      git clone https://github.com/PocheDevv/DemoVentas-3Capas-EF.git
   ```
2. Abre `DemoVentas_3CapasEF.slnx` en **Visual Studio 2022**.
3. Restaura los paquetes NuGet (clic derecho en la solución → *Restaurar paquetes NuGet*).
4. Crea la base de datos `VentasLeon` en tu SQL Server.
5. Ajusta la cadena de conexión en `SitioWEB_VentasGUI/Web.config` y `ProyVentas_ADO/App.Config` a tu servidor:
   ```xml
   provider connection string="data source=TU_SERVIDOR;initial catalog=VentasLeon;user id=TU_USUARIO;password=TU_PASSWORD;..."
   ```
6. Establece **SitioWEB_VentasGUI** como proyecto de inicio y ejecuta (F5).

---

## Autor

**José Gabriel Rosas del Águila (Poche)** — [@PocheDevv](https://github.com/PocheDevv)

Proyecto académico — ISIL, Desarrollo de Software.
