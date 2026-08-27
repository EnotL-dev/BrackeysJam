using Assets._game.Player.View;
using Assets._game.Shift.Controller;
using DG.Tweening;
using UnityEngine;
using Zenject;
using static UnityEngine.Rendering.DebugUI;

namespace Assets._game.Shift.View
{
    public class ShiftManagerView : MonoBehaviour
    {
        [Inject] IShiftService shiftService;
        [Inject] PlayerInterfaceManagerView playerInterfaceManagerView;

        [SerializeField] private Transform light;
        [Space(5)]
        [SerializeField] private int ShiftCycleTime = 300; // time of shift in seconds

        private bool enabledTimer = false;
        private float timer = 0;
        private int seconds = 0;
        public int GetSeconds() => seconds;

        void Update()
        {
            if (!enabledTimer) return;

            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer = 0f;
                seconds--;
                playerInterfaceManagerView.UpdateTimer(seconds);

                if (seconds <= 0)
                    shiftService.StartDayShift();
            }
        }

        public void StartTimer()
        {
            enabledTimer = true;
            timer = 1;
            seconds = ShiftCycleTime;

            ChangeSkyBoxAndLighting(0, 1, 0.5f, 0.25f, 40, 0, 0.9f, 0);
        }

        public void StopTimer()
        {
            playerInterfaceManagerView.StopTimer();
            enabledTimer = false;

            ChangeSkyBoxAndLighting(1, 0, 0.25f, 0.5f, 0, 40, 0, 0.9f);
        }

        public void SetShiftCount(int count)
        {
            playerInterfaceManagerView.SetShiftCount(count);
        }

        private Tween transitionTween;
        private Tween intensityTween;
        private Tween rotationTween;
        private Tween lightIntensityTween;

        public void ChangeSkyBoxAndLighting(float from, float to, float fromIntensity, float toIntensity, float fromRotation, float toRotation, float fromLightIntensity, float toLightIntensity, float duration = 5f, System.Action onComplete = null)
        {
            transitionTween?.Kill();
            intensityTween?.Kill();
            rotationTween?.Kill();
            lightIntensityTween?.Kill();

            RenderSettings.skybox.SetFloat("_CubemapTransition", from);
            transitionTween = DOTween.To(() => RenderSettings.skybox.GetFloat("_CubemapTransition"),
                x => RenderSettings.skybox.SetFloat("_CubemapTransition", x),
                to, duration).SetEase(Ease.InOutCubic);

            RenderSettings.reflectionIntensity = fromIntensity;
            intensityTween = DOTween.To(() => RenderSettings.reflectionIntensity,
                x => RenderSettings.reflectionIntensity = x,
                toIntensity, duration).SetEase(Ease.InOutCubic);

            Light sun = RenderSettings.sun;
            if (sun != null)
            {
                Vector3 startRot = sun.transform.eulerAngles;
                startRot.x = fromRotation;
                sun.transform.eulerAngles = startRot;
                rotationTween = DOTween.To(() => sun.transform.eulerAngles.x,
                    x => {
                        Vector3 rot = sun.transform.eulerAngles;
                        rot.x = x;
                        sun.transform.eulerAngles = rot;
                    },
                    toRotation, duration).SetEase(Ease.InOutCubic);

                sun.intensity = fromLightIntensity;
                lightIntensityTween = DOTween.To(() => sun.intensity,
                    x => sun.intensity = x,
                    toLightIntensity, duration).SetEase(Ease.InOutCubic);
            }

            if (onComplete != null)
            {
                transitionTween.OnComplete(() => onComplete?.Invoke());
            }
        }

        private void Start()
        {
            ChangeSkyBoxAndLighting(1, 0, 0.25f, 0.5f, 0, 40, 0, 0.9f);
        }
    }
}