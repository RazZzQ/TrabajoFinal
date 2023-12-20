using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlerLinterna : MonoBehaviour
{
    //Referencias
    public Light LinternaPlayer;
    ParticleSystem particulas;
    public GameObject PilaPrefab;
    public MonsterControler monstruo;
    public Slider SliderLife;

    //Variables para la vida
    public float LifeLintern = 100;
    public float currentLife = 0;
    public float distanciaMonstruo = 5f;
    public float velocidadPerdidaVida = 2f;
    public float TiempoParaEncontrarBateriaNueva = 5;

    //eventos
    public event Action OnLifeHalfLintern;
    public event Action OnFlashLigthOut;
    public event Action OnFailedToFindBattery;
    public event Action OnLinternOff;
    public event Action OnLinternOn;

    //booleanos
    private bool canToggleFlashlight = true;
    private bool bateriaInstanciada = false;
    private bool hasFailedToFindBattery = false;
    public bool lifeReachedZeroEventTriggered = false;
    public bool lifeReachedHalfEventTriggered = false;


    private void Start()
    {
        particulas = GetComponent<ParticleSystem>();
        currentLife = LifeLintern;
        SliderLife.maxValue = LifeLintern;
        SliderLife.value = currentLife;
    }
    private void Update()
    {
        flashlightflashing();

        if (monstruo != null && Vector3.Distance(monstruo.transform.position, transform.position) < distanciaMonstruo)
        {
            // Disminuir la vida de la linterna con una velocidad determinada
            currentLife -= velocidadPerdidaVida * Time.deltaTime;

            SliderLife.value = currentLife;
        }
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
            if (Input.GetButtonDown("LinternOnOff") && canToggleFlashlight)
            {
                if (LinternaPlayer.enabled == true)
                {
                    particulas.Stop();
                    LinternaPlayer.enabled = false;
                    SliderLife.handleRect.gameObject.SetActive(false);


                    OnLinternOff?.Invoke();
                }
                else if (LinternaPlayer.enabled == false)
                {
                    particulas.Play();
                    LinternaPlayer.enabled = true;
                    SliderLife.handleRect.gameObject.SetActive(true);


                    OnLinternOn?.Invoke();
                }
            }
        }
    }
    public void flashlightflashing()
    {
        if (!lifeReachedZeroEventTriggered && currentLife <= 0)
        {
            LinternaPlayer.enabled = false;
            SliderLife.fillRect.gameObject.SetActive(false);
            SliderLife.handleRect.gameObject.SetActive(false);
            OnFlashLigthOut?.Invoke();
            lifeReachedZeroEventTriggered = true;

            return;
        }

        // Verifica si la vida de la linterna está a la mitad y ejecuta el evento correspondiente
        if (!lifeReachedHalfEventTriggered && currentLife <= 50)
        {
            if (LinternaPlayer.enabled && canToggleFlashlight)
            {
                StartCoroutine(FlashlightToggle());
            }
            OnLifeHalfLintern?.Invoke();
            lifeReachedHalfEventTriggered = true;
        }
    }
    private IEnumerator FlashlightToggle()
    {
        // Desactiva la capacidad de cambiar el estado de la linterna temporalmente
        canToggleFlashlight = false;

        // Apaga la linterna
        LinternaPlayer.enabled = false;
        SliderLife.handleRect.gameObject.SetActive(false);
        particulas.Stop();

        // Espera un tiempo aleatorio para simular el parpadeo
        float timeToWait = UnityEngine.Random.Range(0.1f, 2f);
        yield return new WaitForSeconds(timeToWait);

        // Enciende la linterna
        LinternaPlayer.enabled = true;
        SliderLife.handleRect.gameObject.SetActive(true);
        particulas.Play();

        // Espera un tiempo aleatorio antes de permitir cambiar el estado nuevamente
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 2f));

        // Habilita la capacidad de cambiar el estado de la linterna
        canToggleFlashlight = true;
    }
    private void ShowBattery()
    {
        // Mostrar la batería en una posición aleatoria alrededor del jugador
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), -6.85f, UnityEngine.Random.Range(-5f, 5f));
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
        if (!hasFailedToFindBattery)
        {
            // Ejecutar el evento
            OnFailedToFindBattery?.Invoke();

            // Cambiar el booleano para indicar que el evento ya se ejecutó
            hasFailedToFindBattery = true;
        }
    }
}
