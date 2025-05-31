using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scene.Menu
{
    public enum MenuState
    {
        None,
        Selection
    }
    
    public enum MenuType
    {
        Main,
        Setting,
        Other,
        Confirmation,
    }

    public enum MenuCommand
    {
        Start,
        Exit,
        Setting,
        Other,
        Back,
        Video,
        Audio,
        HowTo,
        Credit,
        Yes,
        No
    }
    
    public class MenuCommandExecutor
    {
        private readonly Dictionary<MenuCommand, Action> _commands;

        public MenuCommandExecutor(MenuPresenter presenter)
        {
            _commands = new Dictionary<MenuCommand, Action>
            {
                { MenuCommand.Start, presenter.ExecuteStart },
                { MenuCommand.Exit, presenter.ExecuteExit },
                { MenuCommand.Setting, presenter.ExecuteSetting },
                { MenuCommand.Other, presenter.ExecuteOther },
                { MenuCommand.Back, presenter.ExecuteBack },
                { MenuCommand.Video, presenter.ExecuteVideo },
                { MenuCommand.Audio, presenter.ExecuteAudio },
                { MenuCommand.HowTo, presenter.ExecuteHowTo },
                { MenuCommand.Credit, presenter.ExecuteCredit },
                { MenuCommand.Yes, presenter.ExecuteYes },
                { MenuCommand.No, presenter.ExecuteNo }
            };
        }

        public void Execute(MenuCommand command)
        {
            if (_commands.TryGetValue(command, out var action))
            {
                action?.Invoke();
            }
        }
    }
}

