using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPuntaje", menuName = "Puntaje/Configuracion")]

public class SO_Puntaje : ScriptableObject
{
    public int maxPuntajes = 10;
    public PuntajeInfo[] puntajes = new PuntajeInfo[0];
    public bool puntajesActualizados = false;

    public float tiempoVivo;  // Agregamos el campo tiempoVivo aquí

    public struct PuntajeInfo
    {
        public float puntaje;
        public float tiempoVivo;
    }

    public void AgregarPuntaje(float puntaje, float tiempoVivo)
    {
        PuntajeInfo nuevoPuntaje = new PuntajeInfo
        {
            puntaje = puntaje,
            tiempoVivo = tiempoVivo
        };

        // Asegúrate de que hay suficiente espacio en el array
        if (puntajes.Length < maxPuntajes)
        {
            System.Array.Resize(ref puntajes, puntajes.Length + 1);
        }

        puntajes[puntajes.Length - 1] = nuevoPuntaje;

        // Ordena el array de puntajes
        OrdenarPuntajes();

        // Asegúrate de que la lista no tenga más puntajes de los permitidos
        if (puntajes.Length > maxPuntajes)
        {
            System.Array.Resize(ref puntajes, maxPuntajes);
        }

        puntajesActualizados = true;
    }

    private void OrdenarPuntajes()
    {
        int n = puntajes.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (puntajes[j].puntaje > puntajes[minIndex].puntaje)
                {
                    minIndex = j;
                }
            }
            PuntajeInfo temp = puntajes[minIndex];
            puntajes[minIndex] = puntajes[i];
            puntajes[i] = temp;
        }
    }
}
