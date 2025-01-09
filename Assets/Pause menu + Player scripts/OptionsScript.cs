using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{

    //reference na audioMixer
    public AudioMixer audio;

    //reference na dropdown
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;

    public int setQualit;
    public int setResH;
    public int setResV;

    //nastavování audia
    public void SetVolume (float volume)
    {
        audio.SetFloat("Volume", volume);
    }



    Resolution[] res;
    //pøidávání možností na rozlišení pro uživatele
    private void Start()
    {
        res = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List <string> resOptions = new();
        int currentResIndex = 0;
        for (int i = 0; i < res.Length; i++)
        {
            string options = res[i].width + "x" + res[i].height;
            resOptions.Add(options);

            if (res[i].width == Screen.currentResolution.width && res[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

   /* private void Update () 
    {
        
    }*/

    public void SetRes(int ResIndex)
    {
        Resolution resolution = res[ResIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //setResH = resolution.height;
        //setResV = resolution.width;
    }

    //nastavení kvality
    public void Quality (int qualitIndex)
    {
        QualitySettings.SetQualityLevel(qualitIndex);
        //setQualit = qualitIndex;
    }
    
    //nastavení fullscreenu
    public void FullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

}

