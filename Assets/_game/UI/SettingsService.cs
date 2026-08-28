using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets._game.UI {
    public class SettingsService : ISettingConfigService, IInitializable, IDisposable {

        private const string MasterKey = "MasterVolume";
        private const string MusicKey = "MusicVolume";
        private const string SFXKey = "SFXVolume";
        private const string SensKey = "MouseSensitivity";
        private const string FOVKey = "FOV";

        private Bus masterBus;
        private Bus musicBus;
        private Bus sfxBus;

        public float MasterVolume { get; private set; }
        public float MusicVolume { get; private set; }
        public float SFXVolume { get; private set; }
        public float MouseSensitivity { get; private set; }
        public float FOV { get; private set; }

        public event Action<float> OnMasterVolumeChanged;
        public event Action<float> OnMusicVolumeChanged;
        public event Action<float> OnSFXVolumeChanged;
        public event Action<float> OnSensitivityChanged;
        public event Action<float> OnFOVChanged;

        public void Initialize() {
            // Cache FMOD buses
            masterBus = RuntimeManager.GetBus("bus:/");
            musicBus = RuntimeManager.GetBus("bus:/Music");
            sfxBus = RuntimeManager.GetBus("bus:/SFX");

            // Load saved values or fallbacks
            MasterVolume = PlayerPrefs.GetFloat(MasterKey, 1.0f);
            MusicVolume = PlayerPrefs.GetFloat(MusicKey, 1.0f);
            SFXVolume = PlayerPrefs.GetFloat(SFXKey, 1.0f);
            MouseSensitivity = PlayerPrefs.GetFloat(SensKey, 1.0f);
            FOV = PlayerPrefs.GetFloat(FOVKey, 75.0f);

            // Apply initial audio levels to FMOD
            ApplyBusVolume(masterBus, MasterVolume);
            ApplyBusVolume(musicBus, MusicVolume);
            ApplyBusVolume(sfxBus, SFXVolume);
        }

        public void SetMasterVolume( float value ) {
            MasterVolume = value;
            ApplyBusVolume(masterBus, value);
            PlayerPrefs.SetFloat(MasterKey, value);
            OnMasterVolumeChanged?.Invoke(value);
        }

        public void SetMusicVolume( float value ) {
            MusicVolume = value;
            ApplyBusVolume(musicBus, value);
            PlayerPrefs.SetFloat(MusicKey, value);
            OnMusicVolumeChanged?.Invoke(value);
        }

        public void SetSFXVolume( float value ) {
            SFXVolume = value;
            ApplyBusVolume(sfxBus, value);
            PlayerPrefs.SetFloat(SFXKey, value);
            OnSFXVolumeChanged?.Invoke(value);
        }

        public void SetMouseSensitivity( float value ) {
            MouseSensitivity = value;
            PlayerPrefs.SetFloat(SensKey, value);
            OnSensitivityChanged?.Invoke(value);
        }

        public void SetFOV( float value ) {
            FOV = value;
            PlayerPrefs.SetFloat(FOVKey, value);
            OnFOVChanged?.Invoke(value);
        }

        private void ApplyBusVolume( Bus bus, float volume ) {
            if ( bus.isValid() ) {
                bus.setVolume(volume);
            }
        }

        public void Dispose() {
            PlayerPrefs.Save();
        }
    }
}