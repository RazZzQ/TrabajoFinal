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
        Linterna.OnLinternOff += DesactivarTodosLosHijos;
    }

    private void OnDisable()
    {
        Linterna.OnLinternOff -= DesactivarTodosLosHijos;
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
}
