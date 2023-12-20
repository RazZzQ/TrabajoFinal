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

    // Estructura que representa la informaci�n de un puntaje
    public struct PuntajeInfo
    {
        public float puntaje;
        public float tiempoVivo;
    }

    // M�todo para agregar un nuevo puntaje y ordenar la lista 
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

        // Aseg�rate de que la lista no tenga m�s puntajes de los permitidos
        if (puntajes.Length > maxPuntajes)
        {
            System.Array.Resize(ref puntajes, maxPuntajes);
        }

        puntajesActualizados = true;
    }//TiempoAsintotico: O(N*N)

    // M�todo para ordenar la lista de puntajes (SeleccionSort)
    private void OrdenarPuntajes()
    {
        //longitud del array de puntajes
        int n = puntajes.Length;

        // El bucle for itera sobre cada posici�n del array hasta la pen�ltima posici�n.
        for (int i = 0; i < n - 1; i++)
        {
            // Al comienzo de cada iteraci�n del bucle externo, se establece el �ndice m�nimo en la posici�n actual.
            int minIndex = i;

            // El bucle For(interno) comienza desde la posici�n siguiente al �ndice actual del bucle for(externo) y se extiende hasta la �ltima posici�n del array.
            for (int j = i + 1; j < n; j++)
            {
                // Dentro del bucle interno, compara los puntajes en la posici�n actual (j) y el �ndice m�nimo (minIndex). Si se encuentra un puntaje m�s grande, actualiza minIndex.
                if (puntajes[j].puntaje > puntajes[minIndex].puntaje)
                {
                    minIndex = j;
                }
            }
            // Despu�s de completar el bucle interno, se intercambian los puntajes en la posici�n actual del bucle externo (i)
            // y el �ndice m�nimo (minIndex). Esto coloca el puntaje m�s grande en la posici�n actual del bucle externo.
            PuntajeInfo temp = puntajes[minIndex];
            puntajes[minIndex] = puntajes[i];
            puntajes[i] = temp;
        }
    }//TiempoAsintotico: O(N*N)
}
