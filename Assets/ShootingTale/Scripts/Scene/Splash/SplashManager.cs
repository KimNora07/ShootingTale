// UnityEngine
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scene.Splash
{
    public class SplashManager : MonoBehaviour
    {
        [SerializeField] private Image fadeInImage;
        [SerializeField] private TMP_Text tipText;

        [SerializeField] private Image targetImage;
        [SerializeField] private float startPixelSize = 50f;
        [SerializeField] private float endPixelSize = 1f;
        [SerializeField] private float duration = 2f;

        private Material mosaicMaterial;

        void Start()
        {
            // 셰이더가 적용된 Material을 복제해서 사용
            mosaicMaterial = Instantiate(targetImage.material);
            targetImage.material = mosaicMaterial;

            StartCoroutine(AnimateMosaic());
        }

        private IEnumerator AnimateMosaic()
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                float pixelSize = Mathf.Lerp(startPixelSize, endPixelSize, t);
                mosaicMaterial.SetFloat("_PixelSize", pixelSize);
                elapsed += Time.deltaTime;
                yield return null;
            }

            mosaicMaterial.SetFloat("_PixelSize", endPixelSize);
        }

    }
}
