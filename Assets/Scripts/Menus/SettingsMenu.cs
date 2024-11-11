using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;
    
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", volume);
    }
    
    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", volume);
    }
    
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("master", volume);
    }
    
    public void GoBack()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
