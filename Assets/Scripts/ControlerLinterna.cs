using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlerLinterna : MonoBehaviour
{
    public Light LinternaPlayer;
    public float LifeLintern = 100;
    public float currentLife = 0;
    public float TiempoParaEncontrarBateriaNueva = 5;


    public event Action OnLifeHalfLintern;
    public event Action OnFlashLigthOut;

    private bool canToggleFlashlight = true;

    private void Start()
    {
        currentLife = LifeLintern;
    }
    private void Update()
    {
        flashlightflashing();
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Input.GetMouseButtonDown(0) && canToggleFlashlight)
            {
                if (LinternaPlayer.enabled == true)
                {
                    LinternaPlayer.enabled = false;
                }
                else if (LinternaPlayer.enabled == false)
                {
                    LinternaPlayer.enabled = true;
                }
            }
        }
    }
    public void flashlightflashing()
    {
        if (currentLife <= 0)
        {
            // Linterna agotada, ejecuta el evento correspondiente y sal del método
            OnFlashLigthOut?.Invoke();
            return;
        }

        // Verifica si la vida de la linterna está a la mitad y ejecuta el evento correspondiente
        if (currentLife <= 50)
        {
            if (LinternaPlayer.enabled && canToggleFlashlight)
            {
                StartCoroutine(FlashlightToggle());
            }
            OnLifeHalfLintern?.Invoke();
        }
    }
    private IEnumerator FlashlightToggle()
    {
        // Desactiva la capacidad de cambiar el estado de la linterna temporalmente
        canToggleFlashlight = false;

        // Apaga la linterna
        LinternaPlayer.enabled = false;

        // Espera un tiempo aleatorio para simular el parpadeo
        float timeToWait = UnityEngine.Random.Range(0.1f, 2f);
        yield return new WaitForSeconds(timeToWait);

        // Enciende la linterna
        LinternaPlayer.enabled = true;

        // Espera un tiempo aleatorio antes de permitir cambiar el estado nuevamente
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 2f));

        // Habilita la capacidad de cambiar el estado de la linterna
        canToggleFlashlight = true;
    }
}
