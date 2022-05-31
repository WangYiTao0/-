using DG.Tweening;
using UnityEngine;

namespace FarmGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemFader : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// 恢复颜色
        /// </summary>
        public void FadeIn()
        {
            Color targetColor = new Color(1, 1, 1, 1);
            _spriteRenderer.DOColor(targetColor, Settings.FadeDuration);
        }

        public void FadeOut()
        {
            Color targetColor = new Color(1, 1, 1, Settings.TargetAlpha);
            _spriteRenderer.DOColor(targetColor, Settings.FadeDuration);
        }

    }
}
