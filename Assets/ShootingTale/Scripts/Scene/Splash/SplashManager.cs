// UnityEngine
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scene.Splash
{
    public class SplashManager : MonoBehaviour
    {
        [Header("Image")] [SerializeField] private Image sliderImage;

        [SerializeField] private Image fadeInImage;
        [SerializeField] private TMP_Text tipText;

        private void Awake()
        {
            sliderImage.fillAmount = 1;
        }


        private void Start()
        {
            AnimationUtility.SlideAnimation(this, sliderImage, 5f, 0.5f, 1, 0, 0, null,
                () =>
                {
                    AnimationUtility.FadeInAnimation(this, fadeInImage, 1.5f, 0, new Color(0, 0, 0), null,
                        () =>
                        {
                            AnimationUtility.FadeInAnimation(this, tipText, 1.5f, 0.5f, new Color(1, 1, 1), null,
                                () =>
                                {
                                    AnimationUtility.FadeOutAnimation(this, tipText, 1.5f, 0.5f, new Color(1, 1, 1), null,
                                        () => { SceneLoader.LoadSceneAsync(this, "01_Menu"); });
                                });
                        });
                });
        }
    }
}
