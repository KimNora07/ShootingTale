// System
using System.Collections.Generic;

public enum MenuType
{
    Main, Setting, Other, Exit
}

public class MenuModel 
{
    // Dictionary를 이용한 타입에 맞는 버튼 이름 저장
    public readonly Dictionary<MenuType, string[]> MenuItems = new()
    {
        { MenuType.Main, new[] {"Start", "Setting", "Other", "Exit"}},
        { MenuType.Setting, new[] { "Video", "Audio", "Back" }},
        { MenuType.Other, new[] { "HowTo", "Credit", "Back" }},
    };

    // 메뉴 타입을 Main으로 초기화
    public MenuType CurrentMenuType { get; set; } = MenuType.Main;

    // 메뉴 타입에 맞는 버튼의 이름들을 가져오는 함수(string[] 리턴) 
    public string[] GetCurrentMenuItems() => MenuItems[CurrentMenuType];
}
