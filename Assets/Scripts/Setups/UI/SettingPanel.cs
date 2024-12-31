using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.UpdateBgVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.UpdateSFXVolume);
    }

    private void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveListener(SoundManager.Instance.UpdateBgVolume);
        sfxSlider.onValueChanged.RemoveListener(SoundManager.Instance.UpdateSFXVolume);
    }
    
    public void OpenPanel()
    {
        gameObject.SetActive(true);
        bgmSlider.value = SoundManager.Instance.BGMVolume;
        sfxSlider.value = SoundManager.Instance.SFXVolume;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}