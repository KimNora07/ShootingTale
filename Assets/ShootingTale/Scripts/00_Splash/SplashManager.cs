// UnityEngine
using UnityEngine;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image sliderImage;
    [SerializeField] private Image fadeInImage;

    private void Awake()
    {
        FillSlider();
    }

    private void Start()
    {
        AnimationUtility.SlideAnimation(this, sliderImage, 0, 0, 1, 0,0, null, () =>
        {
            AnimationUtility.FadeInAnimation(this, fadeInImage, 0.5f, 0, new Color(0, 0, 0), null, () =>
            {
                SceneLoader.LoadSceneAsync(this,"01_Menu");
            });
        });
    }
    
    private void FillSlider()
    {
        sliderImage.fillAmount = 1;
    }
}
