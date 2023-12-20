using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class BarraDeMiedo : MonoBehaviour
{
    public Slider barraMiedoSlider;
    public float perdidaPorTiempo = 5f;
    public SO_Puntaje scoreData;
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera skyCamera;
    public Light skyCameraSpotlight;

    private bool usandoCamaraCielo = false;
    private float tiempoAnterior;

    void Start()
    {
        tiempoAnterior = Time.time;
    }

    void Update()
    {
        // Actualiza el tiempo transcurrido en el puntaje
        scoreData.tiempoVivo += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            usandoCamaraCielo = !usandoCamaraCielo;
            CambiarCamara();
        }

        if (usandoCamaraCielo)
        {
            AumentarBarraMiedoPorTiempo();
            ActivarSpotlight(true);
        }
        else
        {
            ActualizarBarraMiedo();
            ActivarSpotlight(false);
        }

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
    void AumentarBarraMiedoPorTiempo()
    {
        float tiempoActual = Time.time;
        float tiempoTranscurrido = tiempoActual - tiempoAnterior;
        barraMiedoSlider.value += perdidaPorTiempo * tiempoTranscurrido;

        tiempoAnterior = tiempoActual;

        barraMiedoSlider.value = Mathf.Clamp(barraMiedoSlider.value, 0f, 100f);
    }
    public void AumentarBarraMiedo(float cantidad)
    {
        barraMiedoSlider.value += cantidad;
    }
    void PerderJuego()
    {
        scoreData.AgregarPuntaje(barraMiedoSlider.value, scoreData.tiempoVivo);

        scoreData.tiempoVivo = 0f;

        SceneManager.LoadScene("Perder");
    }
    void CambiarCamara()
    {
        // Ajusta las prioridades de las cámaras para activar/desactivar
        if (usandoCamaraCielo)
        {
            playerCamera.Priority = 0;
            skyCamera.Priority = 1;
        }
        else
        {
            playerCamera.Priority = 1;
            skyCamera.Priority = 0;
        }
    }

    void ActivarSpotlight(bool activar)
    {
        // Activa o desactiva el Spotlight de la cámara del cielo
        if (skyCameraSpotlight != null)
        {
            skyCameraSpotlight.enabled = activar;
        }
    }
}
