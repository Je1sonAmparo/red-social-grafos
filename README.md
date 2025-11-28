# 🧑‍🤝‍🧑 **Red Social con Grafos – Proyecto Académico**

Este proyecto modela una **red social de amigos** utilizando estructuras basadas en **grafos**, siguiendo la organización del *Capítulo 18 (Grafos)* del libro guía.
Cada persona se representa como un **nodo**, y cada amistad como una **arista**, permitiendo visualizar y analizar cómo funciona una red social real desde el punto de vista de la teoría de grafos.

---

## 📌 **Objetivo del Proyecto**

Demostrar cómo se aplican los conceptos fundamentales de grafos dentro de un sistema que simula una red social:

* Representación de usuarios (nodos).
* Conexiones de amistad (aristas).
* Uso de matriz de adyacencia.
* Recorridos y algoritmos clásicos como BFS, DFS, Dijkstra, Warshall, Floyd, Prim y Kruskal.
* Expansión progresiva del proyecto conforme avanzan los grupos.

Es un proyecto colaborativo diseñado para que **15 estudiantes, divididos en 5 grupos**, puedan trabajar de forma continua desde lo más básico hasta lo más avanzado.

---

## 🛠️ **Tecnologías usadas**

**Lenguaje:** C#

**Proyecto:** Consola (.NET)

**Control de versiones:** Git + GitHub

---

## 🧱 **Estructura del Proyecto**

```
ProyectoRedAmigos/
│
├── Program.cs
├── Usuario.cs
├── GrafoAmigos.cs
│
├── /algoritmos
│     (se llenará a medida que los grupos avancen)
│
└── /docs
      └── flujo-general.md
```

---

## 🚀 **¿Qué puedes encontrar aquí?**

### ✔ **Implementación básica del grafo**

* Arreglo de usuarios
* Matriz de adyacencia
* Métodos para agregar usuarios y amistades
* Impresión de la red

### ✔ **Base para añadir recorridos**

* Recorrido en anchura (BFS)
* Recorrido en profundidad (DFS)

### ✔ **Algoritmos avanzados**

* Warshall (cierre transitivo)
* Dijkstra (caminos mínimos desde un nodo)
* Floyd (todos los caminos mínimos)
* Prim y Kruskal (árbol de expansión mínima)

Cada grupo agregará una parte del capítulo utilizando la misma red social como ejemplo.

---

## 🎓 **Propósito académico**

Este repositorio se creó para un proyecto grupal con las siguientes características:

* 15 estudiantes distribuidos en 5 grupos
* Cada grupo desarrolla una parte del capítulo
* El resultado final será un video continuo explicando:

  * La teoría
  * La implementación
  * La aplicación usando la red social

---

## 🧑‍💻 **Cómo ejecutar el proyecto**

1. Clona el repositorio:

```
git clone https://github.com/Je1sonAmparo/red-social-grafos
```

2. Entra al directorio:

```
cd red-social-grafos
```

3. Ejecuta el proyecto:

```
dotnet run
```

---

## 📚 **Documentación del proyecto**

En la carpeta `/docs` encontrarás explicaciones que facilitan el entendimiento del proyecto:

* `flujo-general.md` – explicación del funcionamiento interno

---

## 🤝 **Contribuir**

Todo el equipo puede editar este repositorio.
Recomendación:

* Crear una rama por grupo
* Hacer commits claros
* Mantener las clases organizadas
* Seguir el estilo del libro para el código

---
