using System;
using System.Collections;
using UnityEngine;

namespace Assets._game.UI {
    public interface ISettingConfigService {
        float MasterVolume { get; }
        float MusicVolume { get; }
        float SFXVolume { get; }
        float MouseSensitivity { get; }
        float FOV { get; }

        // Setters
        void SetMasterVolume( float value );
        void SetMusicVolume( float value );
        void SetSFXVolume( float value );
        void SetMouseSensitivity( float value );
        void SetFOV( float value );

        // Events to subscribe to
        event Action<float> OnMasterVolumeChanged;
        event Action<float> OnMusicVolumeChanged;
        event Action<float> OnSFXVolumeChanged;
        event Action<float> OnSensitivityChanged;
        event Action<float> OnFOVChanged;
    }
}