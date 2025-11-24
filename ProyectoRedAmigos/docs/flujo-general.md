Flujo General del Proyecto – Red de Amigos

Este documento explica de forma simple cómo funciona el proyecto “Red de Amigos” utilizando una matriz de adyacencia.

Aquí se detalla cómo se agregan los usuarios, cómo se registran las amistades y cómo se imprime la red.

# Primera Parte (Creación básica de la red de amigos)

## 1. Agregar usuarios al grafo

En este proyecto, cada persona se representa como un nodo del grafo.
Los usuarios se almacenan en un arreglo:

Usuario[] usuarios


Para agregar un usuario se utiliza el método:

AgregarUsuario(Usuario u)


Flujo:

Se recibe un objeto Usuario.

Se almacena en el arreglo usuarios.

Se aumenta el contador de usuarios (contadorUsuarios).

Ejemplo en Program.cs:

red.AgregarUsuario(new Usuario("Juan"));
red.AgregarUsuario(new Usuario("María"));

Cada usuario ocupa un índice en el arreglo.
Ese índice se usará dentro de la matriz de adyacencia.

## 2. Crear amistades entre usuarios

Una amistad es una arista del grafo.

La matriz de adyacencia se define como:

int[,] matrizAdyacencia;


Si Juan y María son amigos, se marca un 1 en:

matrizAdyacencia[indiceJuan, indiceMaria]
matrizAdyacencia[indiceMaria, indiceJuan]  (porque es bidireccional)


Esto se hace en el método:

AgregarAmistad(Usuario a, Usuario b)


Flujo:

Buscar el índice de cada usuario en el arreglo usuarios.

Si ambos existen, poner 1 en la matriz en ambas posiciones.

Esto indica que existe una conexión entre los dos nodos.

Ejemplo:

red.AgregarAmistad(juan, maria);

## 3. Imprimir la matriz de adyacencia

Se usa el método:

MostrarRed()


Esta función:

Muestra una fila de encabezados con las iniciales de los usuarios.

Recorre la matriz de adyacencia.

Imprime 0 o 1 según exista amistad o no.

Ejemplo de salida:

    J M A P
J   0 1 0 1
M   1 0 1 0
A   0 1 0 0
P   1 0 0 0


Esto permite visualizar toda la red de amigos de manera clara.