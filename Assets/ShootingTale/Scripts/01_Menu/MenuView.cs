// System
using System;
using System.Collections.Generic;

// UnityEngine
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[System.Serializable]
public class Point
{
    public List<RectTransform> points;
}

public class MenuView : MonoBehaviour, IMenuView
{
    [SerializeField] private RectTransform interactButton;
    [SerializeField] private Image interactButtonSlider;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private List<Point> points;
    
    [SerializeField] private RectTransform icon;
    
    [SerializeField] private RectTransform title;
    [SerializeField] private RectTransform startPanel;
    [SerializeField] private RectTransform exitPanel;
    [SerializeField] private RectTransform mainSelectBar;
    [SerializeField] private RectTransform settingSelectBar;
    [SerializeField] private RectTransform otherSelectBar;
    [SerializeField] private Image fadeImage;
    
    private MenuPresenter _presenter;

    private void Awake()
    {
        _presenter = new MenuPresenter(this, new MenuModel());
        
        AnimationUtility.FadeOutAnimation(this, fadeImage, 0.5f, 0, new Color(0, 0, 0), null, () =>
        {
            AnimationUtility.MoveAnimation(this, title, 0.5f, 0f, new Vector2(0, 0));
            AnimationUtility.MoveAnimation(this, mainSelectBar, 0.5f, 0f, new Vector2(0f, 40f));
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) _presenter.MoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) _presenter.MoveRight();
        if (Input.GetKeyDown(KeyCode.Z)) _presenter.Confirm();
    }

    public void UpdateMenuText(string text)
    {
        buttonText.text = text;
    }

    public void MoveIcon(Vector3 position)
    {
        icon.position = position;
    }

    public void RightMoveButton(Vector3 position, Action onPlay, Action[] onComplete)
    {
        AnimationUtility.MoveAnimation(this, interactButton, 0.4f, 0, position, () =>
        {
            AnimationUtility.SlideAnimation(this, interactButtonSlider, 0.2f, 0, 0, 1,0, onPlay, () =>
            {
                onComplete[1]();
                AnimationUtility.SlideAnimation(this, interactButtonSlider, 0.2f, 0, 1, 0,1, null, null);
            });
        }, onComplete[0]);
    }
    
    public void LeftMoveButton(Vector3 position, Action onPlay, Action[] onComplete)
    {
        AnimationUtility.MoveAnimation(this, interactButton, 0.4f, 0, position, () =>
        {
            AnimationUtility.SlideAnimation(this, interactButtonSlider, 0.2f, 0, 0, 1,1, onPlay, () =>
            {
                onComplete[1]();
                AnimationUtility.SlideAnimation(this, interactButtonSlider, 0.2f, 0, 1, 0,0, null, null);
            });
        }, onComplete[0]);
    }

    public void PlayMoveSound()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonClick);
    }

    public void ChangeTo(RectTransform fromBar, RectTransform toBar, Vector2 fromTargetPosition, Vector2 toTargetPosition, Action onTransitionComplete)
    {
        AnimationUtility.MoveAnimation(this, fromBar, 0.5f, 0f, fromTargetPosition, null, () =>
        {
            onTransitionComplete();
            AnimationUtility.MoveAnimation(this, toBar, 0.5f, 0f, toTargetPosition);
        });
    }

    public List<Point> GetPoints() => points;
    public Image GetSliderImage() => interactButtonSlider;
    public RectTransform GetInteractButton() => interactButton;
    public RectTransform GetTitle() => title;
    public RectTransform GetStartPanel() => startPanel;
    public RectTransform GetExitPanel() => exitPanel;
    public RectTransform GetMainSelectBar() => mainSelectBar;
    public RectTransform GetSettingSelectBar() => settingSelectBar;
    public RectTransform GetOtherSelectBar() => otherSelectBar;
}
