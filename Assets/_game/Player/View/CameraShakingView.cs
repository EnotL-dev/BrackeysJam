using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets._game.Player.View
{
    public class CameraShakingView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public void ShakeCanon()
        {
            _camera.transform.DOKill();

            _camera.transform.DOShakePosition(0.6f, strength: 0.4f, vibrato: 25, randomness: 90);
            _camera.transform.DOShakeRotation(0.6f, strength: new Vector3(8, 8, 0), vibrato: 25);
        }

        public void ShakePunch()
        {
            _camera.transform.DOKill();

            Sequence punchSeq = DOTween.Sequence();
            punchSeq.Append(_camera.transform.DOScale(1.12f, 0.08f).SetEase(Ease.OutQuad));
            punchSeq.Append(_camera.transform.DOScale(1f, 0.12f).SetEase(Ease.InQuad));
            punchSeq.Join(_camera.transform.DOShakePosition(0.15f, strength: 0.05f, vibrato: 8));
            punchSeq.Play();
        }
    }
}