using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreamersGenerador : MonoBehaviour
{
    public event Action OnScreamerGenerated;

    public BarraDeMiedo miedo;
    public float tiempoEntreScreamersMin = 1f;
    public float tiempoEntreScreamersMax = 10f;
    public float radioGeneracion = 5f;
    public float tiempoDeVidaScreamer = 2f;

    public Transform jugadorTransform;
    public GameObject[] screamers;

    void Start()
    {
        if (jugadorTransform == null || screamers.Length == 0)
        {
            Debug.LogError("El Transform del jugador no está asignado o no hay screamers disponibles.");
            return;
        }
            Invoke("GenerarScreamer", UnityEngine.Random.Range(tiempoEntreScreamersMin, tiempoEntreScreamersMax));
    }
    void GenerarScreamer()
    {
        if (jugadorTransform == null || screamers.Length == 0)
        {
            Debug.LogError("El Transform del jugador no está asignado o no hay screamers disponibles.");
            return;
        }

        int indexScreamer = UnityEngine.Random.Range(0, screamers.Length);
        GameObject screamerPrefab = screamers[indexScreamer];

        float anguloAleatorio = UnityEngine.Random.Range(0f, 360f);
        Vector3 direccion = Quaternion.Euler(0f, anguloAleatorio, 0f) * Vector3.forward;
        Vector3 spawnPosition = jugadorTransform.position + direccion * radioGeneracion;

        GameObject screamerInstance = Instantiate(screamerPrefab, spawnPosition, Quaternion.identity);
        ScreamerBase screamerComponent = screamerInstance.GetComponent<ScreamerBase>();

        if (screamerComponent != null)
        {
            screamerComponent.AfectarBarraMiedo(miedo);
        }

        Destroy(screamerInstance, tiempoDeVidaScreamer);

        OnScreamerGenerated?.Invoke();
        Invoke("GenerarScreamer", UnityEngine.Random.Range(tiempoEntreScreamersMin, tiempoEntreScreamersMax));
    }
}
