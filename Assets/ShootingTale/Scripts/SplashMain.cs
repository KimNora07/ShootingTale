using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SplashMain : MonoBehaviour
{
    public RectTransform logoRect;
    public RectTransform handRect;

    public DOTweenAnimation logo;
    public DOTweenAnimation hand;
    public DOTweenAnimation fade;

    public bool isLaps = false;

    private void Start()
    {
        logo.DORestartById("0");
    }

    public void GotoMenu()
    {
        SceneManager.LoadSceneAsync("01_Menu");
    }
}
