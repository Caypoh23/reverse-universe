using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataService : SingletonClass<DataService>
{
    #region Constants
    private const string SoundSettingsString = "Sound";
    private const string MusicSettingsString = "Music";

    private const string PlayerPositionString = "PlayerPosition";

    private const string PlayerHealthString = "CurrentHealth";

    #endregion

    #region Properties

    public float SoundEffectVolumeAmount { get; private set; }
    public float MusicVolumeAmount { get; private set; }

    public Vector3 PlayerPosition { get; private set; }

    public float CurrentPlayerHealthAmount { get; private set; }

    #endregion

    public override void Awake()
    {
        SoundEffectVolumeAmount = LoadSoundSettings();
        MusicVolumeAmount = LoadMusicSettings();
        CurrentPlayerHealthAmount = LoadPlayerHealthAmount();
        PlayerPosition = LoadPlayerPosition();
    }

    #region Save Functions

    public void SaveSoundSettings(float soundEffectVolumeAmount) =>
        ES3.Save(SoundSettingsString, SoundEffectVolumeAmount = soundEffectVolumeAmount);

    public void SaveMusicSettings(float musicSettingsIsActive) =>
        ES3.Save(MusicSettingsString, MusicVolumeAmount = musicSettingsIsActive);

    public void SaveCurrentHealthAmount(float currentHealthAmount) =>
        ES3.Save(PlayerHealthString, CurrentPlayerHealthAmount = currentHealthAmount);

    public void SavePlayerPosition(Vector3 playerPosition) =>
        ES3.Save(PlayerPositionString, PlayerPosition = playerPosition);

    #endregion

    #region Load Functions

    private float LoadSoundSettings() =>
        !ES3.KeyExists(SoundSettingsString) ? ES3.Load<float>(SoundSettingsString) : 0;

    private float LoadMusicSettings() =>
        !ES3.KeyExists(MusicSettingsString) ? ES3.Load<float>(MusicSettingsString) : 0;

    private float LoadPlayerHealthAmount() =>
        ES3.KeyExists(PlayerHealthString) ? ES3.Load<float>(PlayerHealthString) : 100;

    private Vector3 LoadPlayerPosition() =>
        ES3.KeyExists(PlayerPositionString) ? ES3.Load<Vector3>(PlayerPositionString) : Vector3.one;
    #endregion
}
