// UnityEngine
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [SerializeField] private Animator logoAnime;
    [SerializeField] private Image fadeImage;

    private bool isLogoAnimationPlaying;

    private void Start()
    {
        isLogoAnimationPlaying = true;
    }

    private void Update()
    {
        if(isLogoAnimationPlaying && logoAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            isLogoAnimationPlaying = false;
            FadeIn();
        }
    }

    private void FadeIn()
    {
        Utils.Animation.AnimationUtility.FadeAnimation(this, fadeImage, new Color(0, 0, 0), 0, 1, 3, 0, null, ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(SceneName.MainScene);
    } 
}
