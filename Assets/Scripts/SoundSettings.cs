using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField]
    private Slider soundSlider;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private AudioMixer soundMixer;

    private const string MusicParameterName = "Music";
    private const string SoundParameterName = "SFX";

    private DataService _dataService;

    private void Awake()
    {
        _dataService = DataService.Instance;
        soundSlider.value = _dataService.SoundEffectVolumeAmount;
        musicSlider.value = _dataService.MusicVolumeAmount;
    }

    public void ChangeSoundVolume()
    {
        soundMixer.SetFloat(SoundParameterName, soundSlider.value);
        _dataService.SaveSoundSettings(soundSlider.value);
    }

    public void ChangeMusicVolume()
    {
        soundMixer.SetFloat(MusicParameterName, musicSlider.value);
        _dataService.SaveMusicSettings(musicSlider.value);
    }
}
