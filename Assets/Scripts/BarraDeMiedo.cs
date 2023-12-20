using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarraDeMiedo : MonoBehaviour
{
    public Slider barraMiedoSlider;
    public float perdidaPorTiempo = 5f; // Puedes ajustar estos valores según tus necesidades
    public SO_Puntaje scoreData;

    private float tiempoAnterior;

    void Start()
    {
        tiempoAnterior = Time.time;
    }

    void Update()
    {
        // Actualiza el tiempo transcurrido en el puntaje
        scoreData.tiempoVivo += Time.deltaTime;

        // Actualiza la barra de miedo
        ActualizarBarraMiedo();

        // Pierdes si la barra de miedo llega a 100
        if (barraMiedoSlider.value >= 100f)
        {
            PerderJuego();
        }
    }

    void ActualizarBarraMiedo()
    {
        // Reduce la barra de miedo por tiempo
        float tiempoActual = Time.time;
        float tiempoTranscurrido = tiempoActual - tiempoAnterior;
        barraMiedoSlider.value -= perdidaPorTiempo * tiempoTranscurrido;

        // Actualiza el tiempo anterior
        tiempoAnterior = tiempoActual;

        // Asegúrate de que la barra de miedo esté en el rango [0, 100]
        barraMiedoSlider.value = Mathf.Clamp(barraMiedoSlider.value, 0f, 100f);

    }
    public void AumentarBarraMiedo(float cantidad)
    {
        barraMiedoSlider.value += cantidad;
        // Puedes agregar lógica adicional aquí, por ejemplo, mostrar efectos visuales o sonidos de susto
    }
    void PerderJuego()
    {
        // Implementa acciones cuando pierdes
        Debug.Log("¡Perdiste!");
    }
}
