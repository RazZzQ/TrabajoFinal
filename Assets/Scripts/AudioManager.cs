using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public SO_Audio audioSettings;
    public AudioSource audioSource;

    private const string musicVolumeParameter = "Music";
    private const string sfxVolumeParameter = "SFX";
    private bool isMusicMuted = false;
    private bool isSFXMuted = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey(musicVolumeParameter))
        {
            LoadVolume();
        }
        else
        {
            SetInitialSliderValues();
        }
        if (PlayerPrefs.HasKey(sfxVolumeParameter))
        {
            LoadSFXvolumen();
        }
        else
        {
            SetInitialSliderValues();
        }
    }
    private void SetInitialSliderValues()
    {
        if (audioMixer != null && audioSettings != null)
        {

            float musicVolume = audioSettings.musicVolume;
            float sfxVolume = audioSettings.sfxVolume;
            
            SetMusicVolume(musicVolume);
            SetSFXVolume(sfxVolume);
        }
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(musicVolumeParameter, Mathf.Log10(volume) * 20);
        audioSettings.musicVolume = volume;
        musicSlider.value = volume;

        PlayerPrefs.SetFloat(musicVolumeParameter, volume);
    }
    private void LoadVolume()
    {
        float tmp = musicSlider.value;

        tmp = PlayerPrefs.GetFloat(musicVolumeParameter);
        SetMusicVolume(tmp);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(sfxVolumeParameter, Mathf.Log10(volume) * 20);
        audioSettings.sfxVolume = volume;
        sfxSlider.value = volume;

        PlayerPrefs.SetFloat (sfxVolumeParameter, volume);
    }
    private void LoadSFXvolumen()
    {
        float tmp = sfxSlider.value;

        tmp = PlayerPrefs.GetFloat(sfxVolumeParameter);
        SetMusicVolume(tmp);
    }
    public void ToggleMusicMute()
    {
        isMusicMuted = !isMusicMuted;

        if (isMusicMuted)
        {
            audioMixer.SetFloat(musicVolumeParameter, -80f); // Set a low value for muting
        }
        else
        {
            SetMusicVolume(audioSettings.musicVolume);
        }
    }
    public void PlaySFX(int sfxIndex)
    {
        if (audioSource != null && audioSettings != null && sfxIndex >= 0 && sfxIndex < audioSettings.sfxClips.Length)
        {
            AudioClip sfxClip = audioSettings.sfxClips[sfxIndex];
            audioSource.PlayOneShot(sfxClip, audioSettings.sfxVolume);
        }
        else
        {
            Debug.LogWarning("Error al reproducir el sonido SFX. Asegúrate de tener un AudioSource y que el índice sea válido.");
        }
    }
}
