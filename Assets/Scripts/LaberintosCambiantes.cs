using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaberintosCambiantes : MonoBehaviour
{
    private CircularList circularList;
    private bool playerEncontroSalida = false;
    public float tiempoEntreCambios = 5f; // Tiempo en segundos entre cambios de laberinto
    public float tiempoTranscurrido = 0f;

    public GameObject lab1;
    public GameObject lab2;
    public GameObject lab3;
    public GameObject lab4;
    public GameObject lab5;
    public GameObject teleportPoint1;
    public GameObject teleportPoint2;
    public GameObject teleportPoint3;
    public GameObject teleportPoint4;
    public GameObject teleportPoint5;

    public GameObject jugador;

    void Start()
    {
        circularList = new CircularList();

        // Agrega algunos GameObjects iniciales a la lista circular
        circularList.AddNode(lab1, teleportPoint1);
        circularList.AddNode(lab2, teleportPoint2);
        circularList.AddNode(lab3, teleportPoint3);
        circularList.AddNode(lab4, teleportPoint4);
        circularList.AddNode(lab5, teleportPoint5);

        // Instancia el primer laberinto
        InstantiateCurrentLaberinto();

    }

    void Update()
    {
        // Actualiza el tiempo transcurrido
        tiempoTranscurrido += Time.deltaTime;

        // Cambia el laberinto automáticamente si el jugador aún no ha encontrado la salida y ha pasado el tiempo especificado
        if (!playerEncontroSalida && tiempoTranscurrido >= tiempoEntreCambios)
        {
            CambiarLaberinto();
            tiempoTranscurrido = 0f; // Reinicia el contador de tiempo
        }
    }

    void InstantiateCurrentLaberinto()
    {
        GameObject currentLaberintoPrefab = circularList.GetCurrentLaberintoPrefab();
        GameObject teleportPoint = circularList.GetCurrentNode().TeleportPoint;

        if (currentLaberintoPrefab != null && teleportPoint != null)
        {
            jugador.transform.position = teleportPoint.transform.position;
            // Destruye la instancia anterior (si existe)
            if (circularList.GetCurrentNode().CurrentLaberintoInstance != null)
            {
                Destroy(circularList.GetCurrentNode().CurrentLaberintoInstance);
            }

            // Instancia el nuevo laberinto
            circularList.GetCurrentNode().CurrentLaberintoInstance = Instantiate(currentLaberintoPrefab, currentLaberintoPrefab.transform.position, Quaternion.identity);
        }
    }

    void CambiarLaberinto()
    {
        // Implementa la lógica para cambiar el laberinto
        Debug.Log("Cambiando laberinto");

        // Rota la lista para obtener el próximo laberinto en el siguiente ciclo
        circularList.RotateList();

        // Instancia el nuevo laberinto
        InstantiateCurrentLaberinto();
    }

    // Esta función podría ser llamada cuando el jugador encuentra la salida
    public void JugadorEncontroSalida()
    {
        playerEncontroSalida = true;
        // Puedes realizar acciones adicionales cuando el jugador encuentra la salida
    }
}
