# PlayStation Game Catalog

Proyecto desarrollado en ASP.NET Core MVC utilizando arquitectura por capas.

El sistema funciona como un catálogo interactivo de videojuegos de PlayStation 5, permitiendo administrar juegos, usuarios y sesiones mediante una interfaz moderna inspirada en plataformas como PlayStation Store y Steam.

---

# Tecnologías utilizadas

- ASP.NET Core MVC
- C#
- Arquitectura por capas
- JSON como persistencia de datos
- Bootstrap 5
- CSS personalizado
- Session Authentication
- Git y GitHub

---

# Arquitectura del proyecto

El proyecto se encuentra dividido en múltiples capas:

## Catalogo.Domain
Contiene:
- Modelos
- Interfaces
- Entidades principales

## Catalogo.Application
Contiene:
- Servicios
- Lógica de negocio

## Catalogo.Infrastructure
Contiene:
- Repositorios
- Persistencia JSON

## Catalogo.Presentation
Contiene:
- Controladores MVC
- Views Razor
- CSS
- JavaScript
- Assets
- Navegación

---

# Funcionalidades actuales

## Catálogo de videojuegos
- Visualización de juegos
- Cards premium estilo PlayStation
- Vista detalle avanzada
- Hover interactivo
- Filtros por género
- Búsqueda en tiempo real

## Sistema de usuarios
- Registro de usuarios
- Inicio de sesión
- Persistencia en users.json
- Manejo de sesiones
- Navbar dinámica
- Logout

## Protección de rutas
- Restricción para agregar juegos
- Restricción para eliminar juegos
- Validación por sesión

## Persistencia de datos
- Juegos almacenados en items.json
- Usuarios almacenados en users.json

## Experiencia visual
- Diseño gamer oscuro
- Glow effects
- Animaciones hover
- Responsive design
- Estilo inspirado en PS Store

---

# Estructura de datos

## Juegos
Los videojuegos se almacenan en:

```text	
Catalogo.Presentation/Data/items.json

## Autor

Proyecto académico desarrollado para la materia de Arquitectura de Software.

Desarrollado por:
David Morales 

Tecnológico del Software