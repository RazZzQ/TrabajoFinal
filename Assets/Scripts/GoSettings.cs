using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSettings : MonoBehaviour
{
    void Update()
    {
        // Verificar si la tecla Escape fue presionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "Opciones")
            {
                // Si no estamos en la escena de opciones, cambiar a ella
                SceneManager.LoadScene("Opciones");
            }
        }
    }
}
