// System
using System.Collections.Generic;

public class MenuModel
{
    // Dictionary를 이용한 타입에 맞는 버튼 이름 저장
    public readonly Dictionary<MenuType, MenuCommand[]> MenuItems = new()
        {
            { MenuType.Main, new[] { MenuCommand.Start, MenuCommand.Setting, MenuCommand.Other, MenuCommand.Exit } },
            { MenuType.Setting, new[] { MenuCommand.Video, MenuCommand.Audio, MenuCommand.Back } },
            { MenuType.Other, new[] { MenuCommand.HowTo, MenuCommand.Credit, MenuCommand.Back } },
            { MenuType.Confirmation, new[] { MenuCommand.Yes, MenuCommand.No } }
        };

    // 메뉴 타입을 Main으로 초기화
    public MenuType CurrentMenuType { get; set; } = MenuType.Main;
    public MenuState CurrentMenuState { get; set; } = MenuState.None;

    // 메뉴 타입에 맞는 버튼의 이름들을 가져오는 함수(string[] 리턴) 
    public MenuCommand[] GetCurrentMenuItems()
    {
        return MenuItems[CurrentMenuType];
    }
}