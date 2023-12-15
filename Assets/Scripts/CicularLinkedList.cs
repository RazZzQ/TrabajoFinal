using System.Collections.Generic;
using UnityEngine;

public class ListaCircular<T>
{
    private Nodo<T> cabeza;
    private Nodo<T> nodoActual;

    public void InicializarListaCircular(T[] elementos)
    {
        if (elementos == null || elementos.Length == 0)
        {
            Debug.LogError("La lista circular debe tener al menos un elemento.");
            return;
        }

        cabeza = new Nodo<T>(elementos[0]);
        Nodo<T> nodoActual = cabeza;

        for (int i = 1; i < elementos.Length; i++)
        {
            nodoActual.Siguiente = new Nodo<T>(elementos[i]);
            nodoActual = nodoActual.Siguiente;
        }

        nodoActual.Siguiente = cabeza; // Hacer que el último nodo apunte al primer nodo
    }

    public T ObtenerElementoActual()
    {
        return nodoActual.Dato;
    }

    public void Avanzar()
    {
        if (cabeza == null)
        {
            Debug.LogError("La lista circular está vacía o no se ha inicializado.");
            return;
        }

        nodoActual = nodoActual.Siguiente;
    }

    private class Nodo<T>
    {
        public T Dato;
        public Nodo<T> Siguiente;

        public Nodo(T dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}