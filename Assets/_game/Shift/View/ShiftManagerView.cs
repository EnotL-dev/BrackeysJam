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

            float posx = RenderSettings.sun.transform.eulerAngles.x;
            float addedPosx = posx + 180;
            ChangeSkyBoxAndLighting(0, 1, 0.5f, 0.25f, posx, addedPosx, 0.9f, 0);
        }

        public void StopTimer()
        {
            playerInterfaceManagerView.StopTimer();
            enabledTimer = false;

            float posx = RenderSettings.sun.transform.eulerAngles.x;
            float addedPosx = posx + 180;
            ChangeSkyBoxAndLighting(1, 0, 0.25f, 0.5f, posx, addedPosx, 0, 0.9f);
        }

        private Tween transitionTween;
        private Tween intensityTween;
        private Tween rotationTween;
        private Tween lightIntensityTween;

        public void ChangeSkyBoxAndLighting(float from, float to, float fromIntensity, float toIntensity, float fromRotation, float toRotation, float fromLightIntensity, float toLightIntensity, float duration = 8f, System.Action onComplete = null)
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
                Quaternion startRot = Quaternion.Euler(fromRotation, sun.transform.eulerAngles.y, sun.transform.eulerAngles.z);
                Quaternion endRot = Quaternion.Euler(toRotation, sun.transform.eulerAngles.y, sun.transform.eulerAngles.z);

                rotationTween = DOTween.To(() => 0f, t => {
                    sun.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
                }, 1f, duration).SetEase(Ease.InOutCubic);

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
            float posx = RenderSettings.sun.transform.eulerAngles.x;
            float addedPosx = posx + 180;
            ChangeSkyBoxAndLighting(1, 0, 0.25f, 0.5f, posx, addedPosx, 0, 0.9f);
        }
    }
}