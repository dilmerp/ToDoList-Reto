# ToDoList - Enterprise Architecture

Proyecto de gestión de tareas desarrollado como reto técnico, aplicando estándares de arquitectura de software empresarial.

## 🚀 Arquitectura y Patrones
Este proyecto ha sido diseñado bajo un enfoque de **Clean Architecture** (Arquitectura N-Capas), asegurando la separación de responsabilidades y la mantenibilidad del sistema.

* **Patrones Implementados:**
    * **Patrón Repositorio:** Desacoplamiento total entre la lógica de negocio y el acceso a datos.
    * **Patrón Cache-Aside:** Estrategia híbrida de persistencia utilizando `MemoryCache` (Caché de sesión) para lecturas de alto rendimiento y `SQL Server` (Entity Framework) para la persistencia definitiva.
    * **Inversión de Dependencias (SOLID):** Uso de interfaces para asegurar que la capa web no dependa de implementaciones concretas.

## 🛠 Tecnologías
* **Backend:** C#, .NET Framework 4.8, MVC 5.
* **ORM:** Entity Framework 6 (Code-First).
* **Frontend:** Razor Views, Bootstrap 4, jQuery.
* **Seguridad:** Tokens anti-falsificación (Anti-CSRF) alineados a OWASP.

## 📋 Características Principales
1. **CRUD Completo:** Crear, Editar, Eliminar y marcar tareas como completadas.
2. **Sistema de Filtros:** Visualización dinámica (Todas, Pendientes, Completadas) mediante LINQ.
3. **Persistencia Robusta:** Los datos persisten en SQL Server, optimizados con caché en memoria.
4. **Diseño Responsivo:** Interfaz adaptativa para móviles y escritorio mediante el sistema de cuadrículas de Bootstrap.

## 🚀 Instrucciones de Ejecución
1. Clonar el repositorio: `git clone https://github.com/dilmerp/ToDoList.git`
2. Configurar la cadena de conexión en `Web.config` (Proyecto `ToDoList.Web`).
3. Compilar la solución en Visual Studio (`Ctrl + Shift + B`).
4. Ejecutar la aplicación (`F5`).
