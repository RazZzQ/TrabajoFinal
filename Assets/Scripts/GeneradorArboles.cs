using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorArboles : MonoBehaviour
{
    public GameObject arbolPrefab;
    public int cantidadArboles = 10;
    public float radioMapa = 20f;
    public float separacionMinima = 2f;

    void Start()
    {
        GenerarArboles();
    }

    void GenerarArboles()
    {
        for (int i = 0; i < cantidadArboles; i++)
        {
            InstanciarArbol();
        }
    }

    void InstanciarArbol()
    {
        Vector3 posicionAleatoria = ObtenerPosicionAleatoria();
        Instantiate(arbolPrefab, posicionAleatoria, Quaternion.identity);
    }

    Vector3 ObtenerPosicionAleatoria()
    {
        Vector3 posicionAleatoria;

        do
        {
            float angulo = Random.Range(0f, Mathf.PI * 2f);
            float distancia = Random.Range(separacionMinima, radioMapa);
            float x = Mathf.Cos(angulo) * distancia;
            float z = Mathf.Sin(angulo) * distancia;

            posicionAleatoria = new Vector3(x, 0, z) + transform.position;

        } while (Physics.OverlapSphere(posicionAleatoria, separacionMinima).Length > 0);

        return posicionAleatoria;
    }
}
