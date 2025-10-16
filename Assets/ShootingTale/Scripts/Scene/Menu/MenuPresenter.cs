using System;
using UnityEngine;

namespace Scene.Menu
{
    public class MenuPresenter
    {
        private readonly MenuModel _model;
        private readonly IMenuView _view;
        private readonly MenuCommandExecutor _executor;
        private int _position;
        private int _savePosition;  
        private bool isMoving;

        public MenuPresenter(IMenuView view, 
            MenuModel model)
        {
            _view = view;
            _model = model;
            _executor = new MenuCommandExecutor(this);
            UpdateView();
        }

        public void MoveLeft()
        {
            if (_model.CurrentMenuState.IsAllLocked() || isMoving || _position - 1 < 0) return;
            _position--;
            
            if (_model.CurrentMenuType.IsTransitionLocked())
            {
                SetIconButtonTo(_view.GetPoints()[(int)_model.CurrentMenuType].points[_position], Vector3.zero);
                _view.IconMoveAnimation();
            }
            else if (_model.CurrentMenuType.IsMenuFrozen())
            {
                _view.LeftMoveButton(
                    _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition, () =>
                    {
                        isMoving = true;
                        //_view.PlayMoveSound();
                    }, new[]
                    {
                        (Action)(() => { isMoving = false; }),
                        UpdateView
                    });
            }
        }

        public void MoveRight()
        {
            if (_model.CurrentMenuState.IsAllLocked() || isMoving || _position + 1 > _model.GetCurrentMenuItems().Length - 1) return;
            _position++;

            if (_model.CurrentMenuType.IsTransitionLocked())
            {
                SetIconButtonTo(_view.GetPoints()[(int)_model.CurrentMenuType].points[_position], Vector3.zero);
                _view.IconMoveAnimation();
            }
            else if (_model.CurrentMenuType.IsMenuFrozen())
            {
                _view.RightMoveButton(
                    _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition, () =>
                    {
                        isMoving = true;
                        //_view.PlayMoveSound();
                    }, new[]
                    {
                        (Action)(() => { isMoving = false; }),
                        UpdateView
                    });
            }
        }

        public void ConfirmFromBar()
        {
            if (_model.CurrentMenuState.IsAllLocked()) return;

            //_view.PlayClickSound();
            MenuCommand selected = _model.GetCurrentMenuItems()[_position];
            
            _executor.Execute(selected);
        }

        private void UpdateView()
        {
            _view.UpdateMenuText(_model.GetCurrentMenuItems()[_position].ToString());
        }
        
        private void SetInteractButtonTo(Transform t, Vector3 vec)
        {
            _view.GetInteractButton().SetParent(t);
            _view.GetInteractButton().localPosition = vec;
        }
        private void SetIconButtonTo(Transform t, Vector3 vec)
        {
            _view.GetIconButton().SetParent(t);
            _view.GetIconButton().localPosition = vec;
        }
        
        #region ExecuteCommands

        public void ExecuteStart()
        {
            _savePosition = _position;
            _position = 0;
            _model.CurrentMenuState = MenuState.Selection;
            _view.TitleMoveAnimation(_view.GetTitle(), new Vector2(0, 200));
            _view.ChangeTo(_view.GetMainSelectBar(), _view.GetMenuPanel(), new Vector2(0, -90),
                new Vector2(0, -500), () =>
                {
                    _model.CurrentMenuState = MenuState.None;
                    _model.CurrentMenuType = MenuType.Confirmation;
                    _view.UpdateDescriptionText("Are you ready to play?");
                    SetIconButtonTo(_view.GetPoints()[(int)_model.CurrentMenuType].points[0], Vector3.zero);
                }, null);
        }
        public void ExecuteExit()
        {
            _savePosition = _position;
            _position = 0;
            _view.TitleMoveAnimation(_view.GetTitle(), new Vector2(0, 200));
            _view.ChangeTo(_view.GetMainSelectBar(), _view.GetMenuPanel(), new Vector2(0, -90),
                new Vector2(0, -500), () =>
                {
                    _model.CurrentMenuState = MenuState.None;
                    _model.CurrentMenuType = MenuType.Confirmation;
                    _view.UpdateDescriptionText("Exit the game?");
                    SetIconButtonTo(_view.GetPoints()[(int)_model.CurrentMenuType].points[0], Vector3.zero);
                }, null);
        }
        public void ExecuteSetting()
        {
            _model.CurrentMenuState = MenuState.Selection;
            _position = 0;
            _view.ChangeTo(_view.GetMainSelectBar(), _view.GetSettingSelectBar(), new Vector2(0, -90),
                new Vector2(0, 40), () =>
                {
                    _model.CurrentMenuState = MenuState.None;
                    _model.CurrentMenuType = MenuType.Setting;
                    SetInteractButtonTo(_view.GetSettingSelectBar(), 
                        _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition);
                    UpdateView();
                }, null);
        }
        public void ExecuteOther()
        {
            _model.CurrentMenuState = MenuState.Selection;
            _position = 0;
            _view.ChangeTo(_view.GetMainSelectBar(), _view.GetOtherSelectBar(), new Vector2(0, -90),
                new Vector2(0, 40), () =>
                {
                    _model.CurrentMenuState = MenuState.None;
                    _model.CurrentMenuType = MenuType.Other;
                    SetInteractButtonTo(_view.GetOtherSelectBar(), 
                        _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition);
                    UpdateView();
                }, null);
        }
        public void ExecuteBack()
        {
            _position = 0;
            switch (_model.CurrentMenuType)
            {
                case MenuType.Setting:
                    _model.CurrentMenuState = MenuState.Selection;
                    _view.ChangeTo(_view.GetSettingSelectBar(), _view.GetMainSelectBar(), new Vector2(0, -160),
                        new Vector2(0, 40), () =>
                        {
                            _model.CurrentMenuState = MenuState.None;
                            _model.CurrentMenuType = MenuType.Main;
                            SetInteractButtonTo(_view.GetMainSelectBar(), 
                                _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition);
                            UpdateView();
                        }, null);
                    break;
                case MenuType.Other:
                    _model.CurrentMenuState = MenuState.Selection;
                    _view.ChangeTo(_view.GetOtherSelectBar(), _view.GetMainSelectBar(), new Vector2(0, -230),
                        new Vector2(0, 40), () =>
                        {
                            _model.CurrentMenuState = MenuState.None;
                            _model.CurrentMenuType = MenuType.Main;
                            SetInteractButtonTo(_view.GetMainSelectBar(), 
                                _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition);
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
            _view.FadeIn(1.5f, 0.1f, null, _view.ChangeToBattleScene);
        }
        public void ExecuteNo()
        {
            _position = _savePosition;
            _model.CurrentMenuState = MenuState.Selection;
            _view.ChangeTo(_view.GetMenuPanel(), _view.GetMainSelectBar(), new Vector2(0, 0), new Vector2(0, 40),
                () =>
                {
                    _model.CurrentMenuState = MenuState.None;
                    _model.CurrentMenuType = MenuType.Main;
                    _view.TitleMoveAnimation(_view.GetTitle(), new Vector2(0, 0));
                    UpdateView();
                }, null);
        }
        
        #endregion
    }
}