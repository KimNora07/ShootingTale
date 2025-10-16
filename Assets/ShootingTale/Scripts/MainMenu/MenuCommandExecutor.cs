using System;
using System.Collections.Generic;
using UnityEngine;

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

public class MenuCommandExecutor : IMenuCommandExecutor
{
    private readonly Dictionary<MenuCommand, Action> _commands;

    public MenuCommandExecutor(IMenuCommandHandler handler)
    {
        _commands = new Dictionary<MenuCommand, Action>
            {
                { MenuCommand.Start, handler.ExecuteStart },
                { MenuCommand.Exit, handler.ExecuteExit },
                { MenuCommand.Setting, handler.ExecuteSetting },
                { MenuCommand.Other, handler.ExecuteOther },
                { MenuCommand.Back, handler.ExecuteBack },
                { MenuCommand.Video, handler.ExecuteVideo },
                { MenuCommand.Audio, handler.ExecuteAudio },
                { MenuCommand.HowTo, handler.ExecuteHowTo },
                { MenuCommand.Credit, handler.ExecuteCredit },
                { MenuCommand.Yes, handler.ExecuteYes },
                { MenuCommand.No, handler.ExecuteNo }
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

