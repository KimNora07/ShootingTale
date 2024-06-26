using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SplashMain : MonoBehaviour
{
    public DOTweenAnimation fade;

    public void FadeIn()
    {
        fade.DORestartById("0");
    }

    public void GotoMenu()
    {
        SceneManager.LoadSceneAsync("01_Menu");
    }
}
