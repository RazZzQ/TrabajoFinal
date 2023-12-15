using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaberintosCambiantes : MonoBehaviour
{
    public GameObject[] configuracionesLaberintos; // Prefabs de los laberintos
    private ListaCircular<GameObject> listaCircular = new ListaCircular<GameObject>();
    private float tiempoUltimoCambio;

    private void Start()
    {
        // Inicializar la lista circular con los prefabs de laberintos
        listaCircular.InicializarListaCircular(configuracionesLaberintos);

        // Instanciar el laberinto actual
        InstanciarLaberintoActual();
    }

    private void Update()
    {
        // Verificar si ha pasado el tiempo suficiente para cambiar el laberinto
        if (Time.time - tiempoUltimoCambio > ObtenerTiempoDeCambioActual())
        {
            CambiarLaberinto();
        }
    }

    private void InstanciarLaberintoActual()
    {
        // Instanciar el prefab del laberinto actual en la escena
        GameObject laberintoActualPrefab = listaCircular.ObtenerElementoActual();
        Instantiate(laberintoActualPrefab, transform.position, Quaternion.identity);


        tiempoUltimoCambio = Time.time;
    }

    private void CambiarLaberinto()
    {
        // Avanzar al siguiente laberinto en la lista circular
        listaCircular.Avanzar();

        // Destruir el laberinto actual
        Destroy(GameObject.FindGameObjectWithTag("Laberinto"));

        // Instanciar el nuevo laberinto
        InstanciarLaberintoActual();
    }

    private float ObtenerTiempoDeCambioActual()
    {
        // Puedes personalizar la lógica para obtener el tiempo de cambio actual según tus necesidades
        return 5.0f; // Ejemplo: cada 5 segundos
    }
}
