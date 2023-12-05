using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlerLinterna : MonoBehaviour
{
    public Light LinternaPlayer;
    ParticleSystem particulas;
    public float LifeLintern = 100;
    public float currentLife = 0;
    public float TiempoParaEncontrarBateriaNueva = 5;

    //componentesAparte
    public GameObject PilaPrefab;
    public MonsterControler monstruo;
    public float tiempoStunLinterna = 5f;

    //eventos
    public event Action OnLifeHalfLintern;
    public event Action OnFlashLigthOut;
    public event Action OnFailedToFindBattery;
    public event Action OnLinternOff;
    public event Action OnLinternOn;

    //booleanos
    private bool canToggleFlashlight = true;
    private bool bateriaInstanciada = false;
    private bool OnEnableOnce = false;

    private void Start()
    {
        particulas = GetComponent<ParticleSystem>();
        currentLife = LifeLintern;
    }
    private void Update()
    {
        flashlightflashing();
    }

    private void OnEnable()
    {
        OnFlashLigthOut += ShowBattery;
    }

    private void OnDisable()
    {
        OnFlashLigthOut -= ShowBattery;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Input.GetMouseButtonDown(0) && canToggleFlashlight)
            {
                if (LinternaPlayer.enabled == true)
                {                    
                    particulas.Stop();
                    LinternaPlayer.enabled = false;

                    OnLinternOff?.Invoke();
                }
                else if (LinternaPlayer.enabled == false)
                {
                    particulas.Play();
                    LinternaPlayer.enabled = true;

                    OnLinternOn?.Invoke();
                }
            }
        }
    }
    public void flashlightflashing()
    {
        if (currentLife <= 0)
        {
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
        particulas.Stop();

        // Espera un tiempo aleatorio para simular el parpadeo
        float timeToWait = UnityEngine.Random.Range(0.1f, 2f);
        yield return new WaitForSeconds(timeToWait);

        // Enciende la linterna
        LinternaPlayer.enabled = true;
        particulas.Play();

        // Espera un tiempo aleatorio antes de permitir cambiar el estado nuevamente
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 2f));

        // Habilita la capacidad de cambiar el estado de la linterna
        canToggleFlashlight = true;
    }
    private void ShowBattery()
    {
        // Mostrar la batería en una posición aleatoria alrededor del jugador
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), -6.81f, UnityEngine.Random.Range(-5f, 5f));
        InstantiateBattery(randomPosition);
        // Iniciar temporizador para encontrar la batería
        StartCoroutine(FindBatteryTimer());
    }


    private void InstantiateBattery(Vector3 position)
    {
        if (!bateriaInstanciada)
        {
            Instantiate(PilaPrefab, position, Quaternion.identity);
            bateriaInstanciada = true;
        }
    }

    private IEnumerator FindBatteryTimer()
    {
        yield return new WaitForSeconds(TiempoParaEncontrarBateriaNueva);
        Debug.Log("Perdiste");
        // Si el jugador no encuentra la batería a tiempo, ejecuta el evento correspondiente
        OnFailedToFindBattery?.Invoke();
    }
    public void DamagePlayer(int damage)
    {
        LifeLintern -= damage;
    }
}
