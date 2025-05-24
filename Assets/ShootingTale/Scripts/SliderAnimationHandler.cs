using UnityEngine;

public class SliderAnimationHandler : MonoBehaviour
{
    private Animator sliderAnimator;

    private void Awake()
    {
        sliderAnimator = GetComponent<Animator>();
    }

    public void StartSlideAnimation()
    {
        sliderAnimator.SetTrigger("StartSlider");
    }

    public void EndSliderAnimation()
    {
        sliderAnimator.Play("SliderImage2");
    }

    public void EndAnimation()
    {
        sliderAnimator.Play("StandardSlideImage");
    }
}