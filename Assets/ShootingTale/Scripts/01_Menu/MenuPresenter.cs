using System;
using UnityEngine;

public class MenuPresenter
{
    private readonly IMenuView _view;
    private readonly MenuModel _model;
    private int _position = 0;
    private bool isMoving = false;

    public MenuPresenter(IMenuView view, MenuModel model)
    {
        _view = view;
        _model = model;
        UpdateView();
    }

    public void MoveLeft()
    {
        if (isMoving) return;
        if (_position - 1 < 0) return;
        _position--;
        _view.LeftMoveButton(((MenuView)_view).GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition, () =>
        {
            isMoving = true;
            _view.PlayMoveSound();
        }, new[]
        {
            (Action)(() =>
            {
                isMoving = false;
            }),
            UpdateView
        });
    }

    public void MoveRight()
    {
        if (isMoving) return;
        if(_position + 1 > _model.GetCurrentMenuItems().Length - 1) return;
        _position++;
        _view.RightMoveButton(((MenuView)_view).GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition, () =>
        {
            isMoving = true;
            _view.PlayMoveSound();
        },  new[]
        {
            (Action)(() =>
            {
                isMoving = false;
            }),
            UpdateView
        });
    }

    public void Confirm()
    {
        _view.PlayClickSound();
        string selected = _model.GetCurrentMenuItems()[_position];
        Debug.Log($"선택됨: {selected}");
        
        switch (selected)
        {
            case "Start": 
                _view.ChangeTo(_view.GetTitle(), _view.GetSettingSelectBar(), new Vector2(0, -90), new Vector2(0, 40), () =>
                {
                    
                });
                _view.ChangeTo(_view.GetMainSelectBar(), _view.GetSettingSelectBar(), new Vector2(0, -90), new Vector2(0, 40), () =>
                {
                    
                });
                break;
            case "Setting": 
                _model.CurrentMenuType = MenuType.Setting;
                _position = 0;
                _view.ChangeTo(_view.GetMainSelectBar(), _view.GetSettingSelectBar(), new Vector2(0, -90), new Vector2(0, 40), () =>
                {
                    _view.GetInteractButton().SetParent(_view.GetSettingSelectBar());
                    _view.GetInteractButton().localPosition = _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition;
                    UpdateView();
                });
                
                break;
            case "Other": 
                _model.CurrentMenuType = MenuType.Other;
                _position = 0;
                _view.ChangeTo(_view.GetMainSelectBar(), _view.GetOtherSelectBar(), new Vector2(0, -90), new Vector2(0, 40), () =>
                {
                    _view.GetInteractButton().SetParent(_view.GetOtherSelectBar());
                    _view.GetInteractButton().localPosition = _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition;
                    UpdateView();
                });
                break;
            case "Exit": 
                break;
            case "Video": 
                break;
            case "Audio": 
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
                        _view.ChangeTo(_view.GetSettingSelectBar(), _view.GetMainSelectBar(), new Vector2(0, -160), new Vector2(0, 40), () =>
                        {
                            _model.CurrentMenuType = MenuType.Main;
                            _view.GetInteractButton().SetParent(_view.GetMainSelectBar());
                            _view.GetInteractButton().localPosition = _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition;
                            UpdateView();
                        });
                        break;
                    case MenuType.Other:
                        _view.ChangeTo(_view.GetOtherSelectBar(), _view.GetMainSelectBar(), new Vector2(0, -230), new Vector2(0, 40), () =>
                        {
                            _model.CurrentMenuType = MenuType.Main;
                            _view.GetInteractButton().SetParent(_view.GetMainSelectBar());
                            _view.GetInteractButton().localPosition = _view.GetPoints()[(int)_model.CurrentMenuType].points[_position].localPosition;
                            UpdateView();
                        });
                        break;
                }
                break;
        }
    }

    private void UpdateView()
    {
        _view.UpdateMenuText(_model.GetCurrentMenuItems()[_position]);
        //_view.MoveIcon(((MenuView)_view).GetPoints()[_position].position);
    }
}
