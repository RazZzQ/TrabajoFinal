using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamarSonidos : MonoBehaviour
{
    public ControlerLinterna controlerLinterna;
    public AudioManager audioManager;

    private void OnEnable()
    {
        controlerLinterna.OnFailedToFindBattery += PlayFailedToFindBatterySound;
    }
    private void OnDisable()
    {

        controlerLinterna.OnFailedToFindBattery -= PlayFailedToFindBatterySound;
        
    }
    private void PlayFailedToFindBatterySound()
    {
        if (audioManager != null)
        {
            // Supongamos que el índice 0 representa el sonido que deseas reproducir
            int sfxIndex = 3;
            audioManager.PlaySFX(sfxIndex);
        }
    }
}
