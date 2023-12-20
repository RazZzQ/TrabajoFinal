using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPuntaje", menuName = "Puntaje/Configuracion")]

public class SO_Puntaje : ScriptableObject
{
    public int maxPuntajes = 10;
    public PuntajeInfo[] puntajes = new PuntajeInfo[0];
    public bool puntajesActualizados = false;

    public float tiempoVivo;

    // Estructura que representa la información de un puntaje
    public struct PuntajeInfo
    {
        public float puntaje;
        public float tiempoVivo;
    }

    // Método para agregar un nuevo puntaje y ordenar la lista 
    public void AgregarPuntaje(float puntaje, float tiempoVivo)
    {
        PuntajeInfo nuevoPuntaje = new PuntajeInfo
        {
            puntaje = puntaje,
            tiempoVivo = tiempoVivo
        };

        //verificar que hay suficiente espacio en el array
        if (puntajes.Length < maxPuntajes)
        {
            System.Array.Resize(ref puntajes, puntajes.Length + 1);
        }

        // Agrega el nuevo puntaje al final del array
        puntajes[puntajes.Length - 1] = nuevoPuntaje;

        // Ordena el array de puntajes
        OrdenarPuntajes();

        // Asegúrate de que la lista no tenga más puntajes de los permitidos
        if (puntajes.Length > maxPuntajes)
        {
            System.Array.Resize(ref puntajes, maxPuntajes);
        }

        puntajesActualizados = true;
    }//TiempoAsintotico: O(N*N)

    // Método para ordenar la lista de puntajes (SeleccionSort)
    private void OrdenarPuntajes()
    {
        //longitud del array de puntajes
        int n = puntajes.Length;

        // El bucle for itera sobre cada posición del array hasta la penúltima posición.
        for (int i = 0; i < n - 1; i++)
        {
            // Al comienzo de cada iteración del bucle externo, se establece el índice mínimo en la posición actual.
            int minIndex = i;

            // El bucle For(interno) comienza desde la posición siguiente al índice actual del bucle for(externo) y se extiende hasta la última posición del array.
            for (int j = i + 1; j < n; j++)
            {
                // Dentro del bucle interno, compara los puntajes en la posición actual (j) y el índice mínimo (minIndex). Si se encuentra un puntaje más grande, actualiza minIndex.
                if (puntajes[j].puntaje > puntajes[minIndex].puntaje)
                {
                    minIndex = j;
                }
            }
            // Después de completar el bucle interno, se intercambian los puntajes en la posición actual del bucle externo (i)
            // y el índice mínimo (minIndex). Esto coloca el puntaje más grande en la posición actual del bucle externo.
            PuntajeInfo temp = puntajes[minIndex];
            puntajes[minIndex] = puntajes[i];
            puntajes[i] = temp;
        }
    }//TiempoAsintotico: O(N*N)
}
