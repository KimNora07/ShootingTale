using System;
using UnityEngine;

namespace Scene.Menu
{
    public class MenuPresenter
    {
        private readonly MenuModel _model;
        private readonly IMenuView _view;
        private int _position;
        private bool isMoving;

        public MenuPresenter(IMenuView view, MenuModel model)
        {
            _view = view;
            _model = model;
            UpdateView();
        }

        public void MoveLeft()
        {
            if (_model.CurrentMenuType.IsTransitionLocked()) return;
            if (isMoving) return;
            if (_position - 1 < 0) return;
            _position--;
            _view.LeftMoveButton(
                ((MenuView)_view).GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition, () =>
                {
                    isMoving = true;
                    _view.PlayMoveSound();
                }, new[]
                {
                    (Action)(() => { isMoving = false; }),
                    UpdateView
                });
        }

        public void MoveRight()
        {
            if (_model.CurrentMenuType.IsTransitionLocked()) return;
            if (isMoving) return;
            if (_position + 1 > _model.GetCurrentMenuItems().Length - 1) return;
            _position++;
            _view.RightMoveButton(
                ((MenuView)_view).GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition, () =>
                {
                    isMoving = true;
                    _view.PlayMoveSound();
                }, new[]
                {
                    (Action)(() => { isMoving = false; }),
                    UpdateView
                });
        }

        public void ConfirmFromBar()
        {
            if (_model.CurrentMenuType.IsAllLocked()) return;
            if (_model.CurrentMenuType.IsTransitionLocked()) return;

            _view.PlayClickSound();
            string selected = _model.GetCurrentMenuItems()[_position];
            Debug.Log($"선택됨: {selected}");

            switch (selected)
            {
                case "Start" or "Exit":
                    _model.CurrentMenuType = MenuType.Selection;
                    _view.TitleMoveAnimation(_view.GetTitle(), new Vector2(0, 200));
                    _view.ChangeTo(_view.GetMainSelectBar(), _view.GetMenuPanel(), new Vector2(0, -90),
                        new Vector2(0, -500), () =>
                        {
                            _model.CurrentMenuType = MenuType.Confirmation;
                            _view.UpdateDescriptionText(selected == "Start"
                                ? "Are you ready to play?"
                                : "Exit the game?");
                            _view.GetIconButton().SetParent(_view.GetPoints()[(int)_model.CurrentMenuType].points[0]);
                            _view.GetIconButton().localPosition = Vector3.zero;
                        }, null);
                    break;
                case "Setting":
                    _model.CurrentMenuType = MenuType.Selection;
                    _position = 0;
                    _view.ChangeTo(_view.GetMainSelectBar(), _view.GetSettingSelectBar(), new Vector2(0, -90),
                        new Vector2(0, 40), () =>
                        {
                            _model.CurrentMenuType = MenuType.Setting;
                            _view.GetInteractButton().SetParent(_view.GetSettingSelectBar());
                            _view.GetInteractButton().localPosition = _view.GetPoints()[(int)_model.CurrentMenuType]
                                .points[_position].localPosition;
                            UpdateView();
                        }, null);
                    break;
                case "Other":
                    _model.CurrentMenuType = MenuType.Selection;
                    _position = 0;
                    _view.ChangeTo(_view.GetMainSelectBar(), _view.GetOtherSelectBar(), new Vector2(0, -90),
                        new Vector2(0, 40), () =>
                        {
                            _model.CurrentMenuType = MenuType.Other;
                            _view.GetInteractButton().SetParent(_view.GetOtherSelectBar());
                            _view.GetInteractButton().localPosition = _view.GetPoints()[(int)_model.CurrentMenuType]
                                .points[_position].localPosition;
                            UpdateView();
                        }, null);
                    break;
                case "Video":
                    break;
                case "Audio":
                    _model.CurrentMenuType = MenuType.Selection;
                    _view.TitleMoveAnimation(_view.GetTitle(), new Vector2(0, 200));
                    _view.ChangeTo(_view.GetSettingSelectBar(), _view.GetOtherSelectBar(), new Vector2(0, -90),
                        new Vector2(0, 40), () =>
                        {
                        }, null);
                    break;
                    break;
                case "HowTo":
                    break;
                case "Credit":
                    break;
                case "Back":
                    _position = 0;
                    switch (_model.CurrentMenuType)
                    {
                        case MenuType.Setting:
                            _model.CurrentMenuType = MenuType.Selection;
                            _view.ChangeTo(_view.GetSettingSelectBar(), _view.GetMainSelectBar(), new Vector2(0, -160),
                                new Vector2(0, 40), () =>
                                {
                                    _model.CurrentMenuType = MenuType.Main;
                                    _view.GetInteractButton().SetParent(_view.GetMainSelectBar());
                                    _view.GetInteractButton().localPosition =
                                        _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition;
                                    UpdateView();
                                }, null);
                            break;
                        case MenuType.Other:
                            _model.CurrentMenuType = MenuType.Selection;
                            _view.ChangeTo(_view.GetOtherSelectBar(), _view.GetMainSelectBar(), new Vector2(0, -230),
                                new Vector2(0, 40), () =>
                                {
                                    _model.CurrentMenuType = MenuType.Main;
                                    _view.GetInteractButton().SetParent(_view.GetMainSelectBar());
                                    _view.GetInteractButton().localPosition =
                                        _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition;
                                    UpdateView();
                                }, null);
                            break;
                    }

                    break;
            }
        }

        public void FocusYesButton()
        {
            if (_model.CurrentMenuType.IsAllLocked()) return;
            if (_model.CurrentMenuType.IsMenuFrozen()) return;
            _view.GetIconButton().SetParent(_view.GetPoints()[(int)_model.CurrentMenuType].points[0]);
            _view.GetIconButton().localPosition = Vector3.zero;
            _view.IconMoveAnimation();
        }

        public void FocusNoButton()
        {
            if (_model.CurrentMenuType.IsAllLocked()) return;
            if (_model.CurrentMenuType.IsMenuFrozen()) return;
            _view.GetIconButton().SetParent(_view.GetPoints()[(int)_model.CurrentMenuType].points[1]);
            _view.GetIconButton().localPosition = Vector3.zero;
            _view.IconMoveAnimation();
        }

        public void ConfirmFromConfirmationPanel()
        {
            if (_model.CurrentMenuType.IsAllLocked()) return;
            if (_model.CurrentMenuType.IsMenuFrozen()) return;

            _view.PlayClickSound();
            if (_view.GetIconButton().parent == _view.GetPoints()[(int)_model.CurrentMenuType].points[0])
            {
                // 실행
            }
            else
            {
                _model.CurrentMenuType = MenuType.Selection;
                _view.ChangeTo(_view.GetMenuPanel(), _view.GetMainSelectBar(), new Vector2(0, 0), new Vector2(0, 40),
                    () =>
                    {
                        _model.CurrentMenuType = MenuType.Main;
                        _view.TitleMoveAnimation(_view.GetTitle(), new Vector2(0, 0));
                        UpdateView();
                    }, null);
            }
        }

        private void UpdateView()
        {
            _view.UpdateMenuText(_model.GetCurrentMenuItems()[_position]);
            //_view.MoveIcon(((MenuView)_view).GetPoints()[_position].position);
        }
    }
}