using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashMain : MonoBehaviour
{
    [SerializeField] private Image sliderImage;
    [SerializeField] private Image fadeInImage;

    private void Awake()
    {
        sliderImage.fillAmount = 1;
    }

    private void Start()
    {
        StartCoroutine(StartSlide());
    }

    private IEnumerator StartSlide()
    {
        float slideValue = 1;
        while (slideValue > 0)
        {
            slideValue -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            sliderImage.fillAmount = slideValue;
        }
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float alphaValue = 0;
        while (alphaValue < 1.0f)
        {
            alphaValue += 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadeInImage.color = new Color(0, 0, 0, alphaValue);
        }
        GotoMenu();
    }

    private static void GotoMenu()
    {
        SceneManager.LoadSceneAsync("01_Menu");
    }
}
