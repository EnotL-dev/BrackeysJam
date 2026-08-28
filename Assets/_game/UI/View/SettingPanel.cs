using Assets._game.Player;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets._game.UI.View {
    public class SettingPanel : MonoBehaviour, IPlayerUI {

        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider sensitivitySlider;
        [SerializeField] private Slider fovSlider;

        private ISettingConfigService settingsService;

        public bool IsOpen => gameObject.activeSelf;

        [Inject]
        public void Construct( ISettingConfigService settingsService ) {
            this.settingsService = settingsService;
        }

        private void Start() {
            // Initialize sliders with existing service values
            if ( masterSlider != null ) {
                masterSlider.value = settingsService.MasterVolume;
                masterSlider.onValueChanged.AddListener(settingsService.SetMasterVolume);
            }

            if ( musicSlider != null ) {
                musicSlider.value = settingsService.MusicVolume;
                musicSlider.onValueChanged.AddListener(settingsService.SetMusicVolume);
            }

            if ( sfxSlider != null ) {
                sfxSlider.value = settingsService.SFXVolume;
                sfxSlider.onValueChanged.AddListener(settingsService.SetSFXVolume);
            }

            if ( sensitivitySlider != null ) {
                sensitivitySlider.value = settingsService.MouseSensitivity;
                sensitivitySlider.onValueChanged.AddListener(settingsService.SetMouseSensitivity);
            }

            if ( fovSlider != null ) {
                fovSlider.value = settingsService.FOV;
                fovSlider.onValueChanged.AddListener(settingsService.SetFOV);
            }

            Close();
        }

        private void OnDestroy() {
            if ( masterSlider != null ) masterSlider.onValueChanged.RemoveListener(settingsService.SetMasterVolume);
            if ( musicSlider != null ) musicSlider.onValueChanged.RemoveListener(settingsService.SetMusicVolume);
            if ( sfxSlider != null ) sfxSlider.onValueChanged.RemoveListener(settingsService.SetSFXVolume);
            if ( sensitivitySlider != null ) sensitivitySlider.onValueChanged.RemoveListener(settingsService.SetMouseSensitivity);
            if ( fovSlider != null ) fovSlider.onValueChanged.RemoveListener(settingsService.SetFOV);
        }

        public void Toggle() {
            if ( IsOpen ) Close();
            else Open();
        }

        public void Open() {
            gameObject.SetActive(true);
        }

        public void Close() {
            gameObject.SetActive(false);
        }
    }
}