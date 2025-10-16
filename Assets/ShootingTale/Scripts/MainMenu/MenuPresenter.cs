using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

public class MenuPresenter : IMenuPresenter, IMenuCommandHandler
{
    private readonly MenuModel model;
    private readonly MenuView view;
    private readonly MenuCommandExecutor executor;
    private readonly CancellationTokenSource cts = new ();
    private int position;
    private int savePosition;
    private bool isMoving;

    public MenuPresenter(MenuView view, MenuModel model)
    {
        this.view = view ?? throw new ArgumentNullException(nameof(view));
        this.model = model ?? throw new ArgumentNullException(nameof(model));
        executor = new MenuCommandExecutor(this);
    }

    void IStartable.Start()
    {
        Utils.Animation.AnimationUtility.FadeAnimation(view, view.GetFadeImage(), new Color(0, 0, 0), 1, 0, 5, 0.5f, null, null);
        Initialize();
    }

    public void Initialize()
    {
        view.OnLeft += OnLeft;
        view.OnRight += OnRight;
        view.OnConfirm += OnConfirm;

        position = Mathf.Clamp(position, 0, Math.Max(0, model.GetCurrentMenuItems().Length - 1));
    }

    private void OnLeft() => MoveRelative(-1);
    private void OnRight() => MoveRelative(+1);
    private void OnConfirm() => ConfirmFromBar();

    private void MoveRelative(int delta)
    {
        if (model.CurrentMenuState.IsAllLocked() || isMoving) return;
        int newPos = position + delta;
        if (newPos < 0 || newPos >= model.GetCurrentMenuItems().Length) return;
        position = newPos;
        HandleMove(delta);
    }

    private void HandleMove(int delta)
    {
        var type = model.CurrentMenuType;
        var points = view.GetPoints()[(int)type].points;
        var target = points[position];

        if (type.IsTransitionLocked())
        {
            SetIconButtonTo(target, Vector3.zero);
            view.IconMoveAnimation();
            UpdateView();
            return;
        }

        if (type.IsMenuFrozen())
        {
            isMoving = true;
            Action onPlay = null;
            Action[] onComplete = new[]
            {
                (Action)(() => { isMoving = false; }),
                UpdateView
            };

            if (delta < 0)
                view.LeftMoveButton(target.localPosition, onPlay, onComplete);
            else
                view.RightMoveButton(target.localPosition, onPlay, onComplete);
        }
    }

    public void ConfirmFromBar()
    {
        if (model.CurrentMenuState.IsAllLocked()) return;
        var selected = model.GetCurrentMenuItems()[position];
        executor.Execute(selected);
    }

    private void UpdateView()
    {
        var items = model.GetCurrentMenuItems();
        if (items.Length == 0) return;
        view.UpdateMenuText(items[position].ToString());
    }

    private void SetInteractButtonTo(Transform parent, Vector3 localPos)
    {
        var btn = view.GetInteractButton();
        if (btn == null || parent == null) return;
        btn.SetParent(parent, false);
        btn.localPosition = localPos;
    }

    private void SetIconButtonTo(Transform parent, Vector3 localPos)
    {
        var icon = view.GetIconButton();
        if (icon == null || parent == null) return;
        icon.SetParent(parent, false);
        icon.localPosition = localPos;
    }

    public void Dispose()
    {
        view.OnLeft -= OnLeft;
        view.OnRight -= OnRight;
        view.OnConfirm -= OnConfirm;
        cts.Cancel();
        cts.Dispose();
    }
    
    #region ExecuteCommands

    public void ExecuteStart()
    {
        savePosition = position;
        position = 0;
        model.CurrentMenuState = MenuState.Selection;
        view.TitleMoveAnimation(view.GetTitle(), new Vector2(0, 200));
        view.ChangeTo(view.GetMainSelectBar(), view.GetMenuPanel(), new Vector2(0, -200),
            new Vector2(0, -500), () =>
            {
                model.CurrentMenuState = MenuState.None;
                model.CurrentMenuType = MenuType.Confirmation;
                view.UpdateDescriptionText("Are you ready to play?");
                SetIconButtonTo(view.GetPoints()[(int)model.CurrentMenuType].points[0], Vector3.zero);
            }, null);
    }
    public void ExecuteExit()
    {
        savePosition = position;
        position = 0;
        view.TitleMoveAnimation(view.GetTitle(), new Vector2(0, 200));
        view.ChangeTo(view.GetMainSelectBar(), view.GetMenuPanel(), new Vector2(0, -200),
            new Vector2(0, -500), () =>
            {
                model.CurrentMenuState = MenuState.None;
                model.CurrentMenuType = MenuType.Confirmation;
                view.UpdateDescriptionText("Exit the model?");
                SetIconButtonTo(view.GetPoints()[(int)model.CurrentMenuType].points[0], Vector3.zero);
            }, null);
    }
    public void ExecuteSetting()
    {
        model.CurrentMenuState = MenuState.Selection;
        position = 0;
        view.ChangeTo(view.GetMainSelectBar(), view.GetSettingSelectBar(), new Vector2(0, -200),
            new Vector2(0, 200), () =>
            {
                model.CurrentMenuState = MenuState.None;
                model.CurrentMenuType = MenuType.Setting;
                SetInteractButtonTo(view.GetSettingSelectBar(),
                    view.GetPoints()[(int)model.CurrentMenuType].points[position].localPosition);
                UpdateView();
            }, null);
    }
    public void ExecuteOther()
    {
        model.CurrentMenuState = MenuState.Selection;
        position = 0;
        view.ChangeTo(view.GetMainSelectBar(), view.GetOtherSelectBar(), new Vector2(0, -200),
            new Vector2(0, 200), () =>
            {
                model.CurrentMenuState = MenuState.None;
                model.CurrentMenuType = MenuType.Other;
                SetInteractButtonTo(view.GetOtherSelectBar(),
                    view.GetPoints()[(int)model.CurrentMenuType].points[position].localPosition);
                UpdateView();
            }, null);
    }
    public void ExecuteBack()
    {
        position = 0;
        switch (model.CurrentMenuType)
        {
            case MenuType.Setting:
                model.CurrentMenuState = MenuState.Selection;
                view.ChangeTo(view.GetSettingSelectBar(), view.GetMainSelectBar(), new Vector2(0, -200),
                    new Vector2(0, 200), () =>
                    {
                        model.CurrentMenuState = MenuState.None;
                        model.CurrentMenuType = MenuType.Main;
                        SetInteractButtonTo(view.GetMainSelectBar(),
                            view.GetPoints()[(int)model.CurrentMenuType].points[position].localPosition);
                        UpdateView();
                    }, null);
                break;
            case MenuType.Other:
                model.CurrentMenuState = MenuState.Selection;
                view.ChangeTo(view.GetOtherSelectBar(), view.GetMainSelectBar(), new Vector2(0, -200),
                    new Vector2(0, 200), () =>
                    {
                        model.CurrentMenuState = MenuState.None;
                        model.CurrentMenuType = MenuType.Main;
                        SetInteractButtonTo(view.GetMainSelectBar(),
                            view.GetPoints()[(int)model.CurrentMenuType].points[position].localPosition);
                        UpdateView();
                    }, null);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    public void ExecuteVideo()
    {

    }
    public void ExecuteAudio()
    {
        // _model.CurrentMenuType = MenuType.Selection;
        // _view.TitleMoveAnimation(_view.GetTitle(), new Vector2(0, 200));
        // _view.ChangeTo(_view.GetSettingSelectBar(), _view.GetOtherSelectBar(), new Vector2(0, -90),
        //     new Vector2(0, 40), null, null);
    }
    public void ExecuteHowTo()
    {

    }
    public void ExecuteCredit()
    {

    }
    public void ExecuteYes()
    {
        //view.FadeIn(1.5f, 0.1f, null, view.ChangeToBattleScene);
    }
    public void ExecuteNo()
    {
        position = savePosition;
        model.CurrentMenuState = MenuState.Selection;
        view.ChangeTo(view.GetMenuPanel(), view.GetMainSelectBar(), new Vector2(0, 300), new Vector2(0, 200),
            () =>
            {
                model.CurrentMenuState = MenuState.None;
                model.CurrentMenuType = MenuType.Main;
                view.TitleMoveAnimation(view.GetTitle(), new Vector2(0, -300));
                UpdateView();
            }, null);
    }
    #endregion
}