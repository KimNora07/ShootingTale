// System

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// UnityEngine

namespace Scene.Menu
{
    [Serializable]
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

    [SerializeField] private RectTransform iconButton;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private RectTransform title;
    [SerializeField] private RectTransform menuPanel;
    [SerializeField] private RectTransform mainSelectBar;
    [SerializeField] private RectTransform settingSelectBar;
    [SerializeField] private RectTransform otherSelectBar;
    [SerializeField] private Image fadeImage;

    private MenuPresenter _presenter;

    private void Awake()
    {
        _presenter = new MenuPresenter(this, new MenuModel());

        AnimationUtility.FadeOutAnimation(this, fadeImage, 2f, 0.5f, new Color(0, 0, 0), null, () =>
        {
            AnimationUtility.MoveAnimation(this, title, 0.5f, 0f, new Vector2(0, 0));
            AnimationUtility.MoveAnimation(this, mainSelectBar, 0.5f, 0f, new Vector2(0f, 40f));
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _presenter.MoveLeft();
            _presenter.FocusYesButton();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _presenter.MoveRight();
            _presenter.FocusNoButton();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            _presenter.ConfirmFromBar();
            _presenter.ConfirmFromConfirmationPanel();
        }
    }

    public void UpdateMenuText(string text)
    {
        buttonText.text = text;
    }

    public void UpdateDescriptionText(string text)
    {
        descriptionText.text = text;
    }

    public void RightMoveButton(Vector3 position, Action onPlay, Action[] onComplete)
    {
        AnimationUtility.MoveAnimation(this, interactButton, 0.3f, 0, position, () =>
        {
            AnimationUtility.SlideAnimation(this, interactButtonSlider, 0.15f, 0, 0, 1, 0, onPlay, () =>
            {
                onComplete[1]?.Invoke();
                AnimationUtility.SlideAnimation(this, interactButtonSlider, 0.15f, 0, 1, 0, 1, null, null);
            });
        }, () => { onComplete[0]?.Invoke(); });
        ;
    }

    public void LeftMoveButton(Vector3 position, Action onPlay, Action[] onComplete)
    {
        AnimationUtility.MoveAnimation(this, interactButton, 0.3f, 0, position, () =>
        {
            AnimationUtility.SlideAnimation(this, interactButtonSlider, 0.15f, 0, 0, 1, 1, onPlay, () =>
            {
                onComplete[1]?.Invoke();
                AnimationUtility.SlideAnimation(this, interactButtonSlider, 0.15f, 0, 1, 0, 0, null, null);
            });
        }, () => { onComplete[0]?.Invoke(); });
    }

    public void PlayMoveSound()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonClick);
    }

    public void ChangeTo(RectTransform fromTarget, RectTransform toTarget, Vector2 fromTargetPosition,
        Vector2 toTargetPosition, Action onTransitionComplete, Action onComplete)
    {
        AnimationUtility.MoveAnimation(this, fromTarget, 0.5f, 0f, fromTargetPosition, null, () =>
        {
            onTransitionComplete?.Invoke();
            AnimationUtility.MoveAnimation(this, toTarget, 0.5f, 0f, toTargetPosition, null,
                () => { onComplete?.Invoke(); });
        });
    }

    public void TitleMoveAnimation(RectTransform target, Vector2 targetPosition)
    {
        AnimationUtility.MoveAnimation(this, target, 0.5f, 0f, targetPosition);
    }

    public void IconMoveAnimation()
    {
        AnimationUtility.ScaleYAnimation(this, iconButton.gameObject, 1.75f, 1f, 0.15f, 0,
            () => { AnimationUtility.ScaleXAnimation(this, iconButton.gameObject, 0.25f, 1f, 0.15f, 0, null, null); },
            () => { iconButton.localScale = new Vector3(1f, 1f, 1f); });
    }

    public List<Point> GetPoints()
    {
        return points;
    }

    public Image GetSliderImage()
    {
        return interactButtonSlider;
    }

    public RectTransform GetInteractButton()
    {
        return interactButton;
    }

    public RectTransform GetIconButton()
    {
        return iconButton;
    }

    public RectTransform GetTitle()
    {
        return title;
    }

    public RectTransform GetMenuPanel()
    {
        return menuPanel;
    }

    public RectTransform GetMainSelectBar()
    {
        return mainSelectBar;
    }

    public RectTransform GetSettingSelectBar()
    {
        return settingSelectBar;
    }

    public RectTransform GetOtherSelectBar()
    {
        return otherSelectBar;
    }
}
}

