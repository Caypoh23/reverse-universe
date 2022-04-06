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
    private AudioMixerGroup vfxMixer;
    [SerializeField]
    private AudioMixerGroup musicMixer;

    private const string MusicParameterName = "Music";
    private const string SoundParameterName = "SFX";

    private DataService _dataService;

    private void Awake()
    {
        _dataService = DataService.Instance;
    }
}
