using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarHijos : MonoBehaviour
{
    public ControlerLinterna Linterna;
    void Start()
    {
        // Llama a la función para desactivar todos los hijos del objeto actual
        //DesactivarTodosLosHijos(transform);
    }
    private void OnEnable()
    {
        Linterna.OnLinternOff += DesactivarHijosHandler;
        Linterna.OnLinternOn += ActivarHijosHandler;
    }

    private void OnDisable()
    {
        Linterna.OnLinternOff -= DesactivarHijosHandler;
        Linterna.OnLinternOn -= ActivarHijosHandler;
    }
    void DesactivarHijosHandler()
    {
        // Llama a DesactivarTodosLosHijos con el transform actual
        DesactivarTodosLosHijos(transform);
    }
    void ActivarHijosHandler()
    {
        // Llama a ActivarTodosLosHijos con el transform actual
        ActivarTodosLosHijos(transform);
    }

    void DesactivarTodosLosHijos(Transform objetoPadre)
    {
        // Itera a través de todos los hijos del objeto padre
        for (int i = 0; i < objetoPadre.childCount; i++)
        {
            // Desactiva cada hijo en lugar de destruirlo
            objetoPadre.GetChild(i).gameObject.SetActive(false);
        }
    }
    void ActivarTodosLosHijos(Transform objetoPadre)
    {
        // Itera a través de todos los hijos del objeto padre
        for (int i = 0; i < objetoPadre.childCount; i++)
        {
            // Activa cada hijo
            objetoPadre.GetChild(i).gameObject.SetActive(true);
        }
    }
}
