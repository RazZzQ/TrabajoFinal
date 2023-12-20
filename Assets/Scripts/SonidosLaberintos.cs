using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosLaberintos : MonoBehaviour
{
    public ScreamersGenerador screamersGenerador;
    public PlayerTemporal tmp;
    public LinternaTemporal linterna;
    public AudioManager audioManager;

    private void OnEnable()
    {
        screamersGenerador.OnScreamerGenerated += SpawnScreamer;
        tmp.OnGrabCoin += GrabCoin;
        linterna.OnLinternOn += linternaOn;
        linterna.OnLinternOff += linternaOff;
    }
    private void OnDisable()
    {
        screamersGenerador.OnScreamerGenerated -= SpawnScreamer;
        tmp.OnGrabCoin -= GrabCoin;
        linterna.OnLinternOn -= linternaOn;
        linterna.OnLinternOff -= linternaOff;
    }
    private void SpawnScreamer()
    {
        if (audioManager != null)
        {
            int sfxIndex = 5;
            audioManager.PlaySFX(sfxIndex);
        }
    }
    private void GrabCoin()
    {
        if(audioManager != null)
        {
            int sfxIndex = 7;
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
}
