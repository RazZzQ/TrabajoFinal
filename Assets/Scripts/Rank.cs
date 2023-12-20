using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rank : MonoBehaviour
{
    public SO_Puntaje scoreData;
    public TextMeshProUGUI puntajesText;

    void Start()
    {
        MostrarPuntajes();
    }

    void MostrarPuntajes()
    {
        // Verifica si los puntajes han sido actualizados desde la última vez
        if (scoreData.puntajesActualizados)
        {
            // Actualiza el texto con los puntajes
            puntajesText.text = ObtenerTextoPuntajes(scoreData.puntajes);
            // Reinicia la bandera de puntajesActualizados
            scoreData.puntajesActualizados = false;
        }
    }

    string ObtenerTextoPuntajes(SO_Puntaje.PuntajeInfo[] puntajes)
    {
        string textoPuntajes = "";
        for (int i = 0; i < puntajes.Length; i++)
        {
            textoPuntajes += (i + 1) + ". Miedo: " + puntajes[i].puntaje + ", Tiempo Vivo: " + puntajes[i].tiempoVivo + "\n";
        }
        return textoPuntajes;
    }
}
