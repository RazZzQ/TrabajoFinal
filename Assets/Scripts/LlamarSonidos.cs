using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamarSonidos : MonoBehaviour
{
    public ControlerLinterna controlerLinterna;
    public PlayerMovement Player;
    public AudioManager audioManager;

    private void OnEnable()
    {
        controlerLinterna.OnFailedToFindBattery += PlayFailedToFindBatterySound;
        controlerLinterna.OnLinternOff += linternaOff;
        controlerLinterna.OnLinternOn += linternaOn;
        controlerLinterna.OnFlashLigthOut += vidaLinternaCero;
        controlerLinterna.OnLifeHalfLintern += VidaMitad;
        Player.GrabObject += GrabObject;
    }
    private void OnDisable()
    {
        controlerLinterna.OnFailedToFindBattery -= PlayFailedToFindBatterySound;
        controlerLinterna.OnLinternOff -= linternaOff;
        controlerLinterna.OnLinternOn -= linternaOn;
        controlerLinterna.OnFlashLigthOut -= vidaLinternaCero;
        controlerLinterna.OnLifeHalfLintern -= VidaMitad;
        Player.GrabObject -= GrabObject;
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
    private void linternaOn()
    {
        if (audioManager != null)
        {
            int sfxIndex = 2;
            audioManager.PlaySFX(sfxIndex);
        }
    }
    private void linternaOff()
    {
        if (audioManager != null)
        {
            int sfxIndex = 2;
            audioManager.PlaySFX(sfxIndex);
        }
    }
    private void vidaLinternaCero()
    {
        if (audioManager != null)
        {
            int sfxIndex = 1;
            audioManager.PlaySFX(sfxIndex);
        }
    }
    private void VidaMitad()
    {
        if (audioManager != null)
        {
            int sfxIndex = 8;
            audioManager.PlaySFX(sfxIndex);
        }
    }
    private void GrabObject()
    {
        if (audioManager != null)
        {
            int sfxIndex = 6;
            audioManager.PlaySFX(sfxIndex);
        }
    }
}
