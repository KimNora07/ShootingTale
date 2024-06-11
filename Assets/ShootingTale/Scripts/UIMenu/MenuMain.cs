using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuType
{
    Main = 0,
    InMain,
    Setting = 10,
    Video,
    Audio,
    Exit = 20
}

public class MenuMain : MonoBehaviour
{
    
    [Header("NoSelectMainMenu")]
    public DOTweenAnimation title;
    public DOTweenAnimation mainSelectBar;
    public RectTransform startPoint;
    public RectTransform settingPoint;
    public RectTransform exitPoint_M;
    public RectTransform selectButton_M;
    public TMP_Text selectButtonText_M;
    
    [Header("NoSelectSettingMenu")]
    public DOTweenAnimation settingSelectBar;
    public RectTransform videoPoint;
    public RectTransform audioPoint;
    public RectTransform exitPoint_S;
    public RectTransform selectButton_S;
    public TMP_Text selectButtonText_S;

    [Header("SelectMainMenu")]
    public DOTweenAnimation menuBackground;
    public RectTransform icon_M;
    public GameObject iconObj_M;

    [Header("SelectExitMenu")]
    public DOTweenAnimation exitBackground;
    public RectTransform icon_E;
    public GameObject iconObj_E;

    [Space(10)]
    public TMP_Text buttonText;
    public RectTransform button;

    public List<RectTransform> points;
    public List<RectTransform> mainMenuPoints;
    public List<RectTransform> exitMenuPoints;

    public SliderAnimationHandler sliderHandler_M;
    public SliderAnimationHandler sliderHandler_S;

    private int position = 0;

    public bool isLeft = false;
    public bool isRight = false;

    public bool isClick = false;

    public MenuType menuType;

    private readonly string[] mainMenuNames = { "시작", "설정", "나가기" };
    private readonly string[] settingMenuNames = { "비디오", "오디오", "나가기" };

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        menuType = MenuType.Main;
        InitList(startPoint, settingPoint, exitPoint_M);
        button.position = points[0].position;

        title.DORestartById("0");
        mainSelectBar.DORestartById("0");
    }

    private void InitList(RectTransform point1, RectTransform point2, RectTransform point3)
    {
        points.Clear();
        points.Add(point1);
        points.Add(point2);
        points.Add(point3);

        button.position = points[0].position;
    }

    //private void InitList(params RectTransform[] points)
    //{
    //    this.points.Clear();
    //    for(int i = 0; i < points.Length; i++)
    //    {
    //        this.points.Add(points[i]);
    //    }
    //    button.position = this.points[0].position;
    //}

    private void Update()
    {
        KeyInput();
        ButtonMovement();         
    }

    private void KeyInput()
    {
        if(isClick)
        {
            IsClickHandle();
            IsClickButtonHandle();
        }
        else
        {
            IsNotClickHandle();
            IsNotClickButtonHandle();
        }
    }
    private void IsClickHandle()
    {
        if(menuType == MenuType.InMain)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (icon_M.position == mainMenuPoints[position].position && position > 0 && iconObj_M.activeSelf)
                {
                    icon_M.position = mainMenuPoints[position - 1].position;
                    position--;
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (icon_M.position == mainMenuPoints[position].position && position < mainMenuPoints.Count - 1 && iconObj_M.activeSelf)
                {
                    icon_M.position = mainMenuPoints[position + 1].position;
                    position++;
                }
            }
        }
        else if (menuType == MenuType.Exit)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (icon_E.position == exitMenuPoints[position].position && position > 0 && iconObj_E.activeSelf)
                {
                    icon_E.position = exitMenuPoints[position - 1].position;
                    position--;
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (icon_E.position == exitMenuPoints[position].position && position < exitMenuPoints.Count - 1 && iconObj_E.activeSelf)
                {
                    icon_E.position = exitMenuPoints[position + 1].position;
                    position++;
                }
            }
        }
    }
    private void IsNotClickHandle()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {      
            if (button.position == points[position].position && position > 0)
            {
                MenuTypeSlideAnimation();
                isLeft = true;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        { 
            if (button.position == points[position].position && position < points.Count - 1)
            {
                MenuTypeSlideAnimation();
                isRight = true;
            }
            
        }
    }

    private void ButtonMovement()
    {
        if (isLeft)
        {
            IsLeftHandle();
        }
        if (isRight)
        {
            IsRightHandle();
        }
    }
    private void IsLeftHandle()
    {
        button.position = Vector3.MoveTowards(button.position, points[position - 1].position, 20f * Time.deltaTime);
        if (button.position == points[position - 1].position)
        {
            isLeft = false;
            position--;
        }
    }
    private void IsRightHandle()
    {
        button.position = Vector3.MoveTowards(button.position, points[position + 1].position, 20f * Time.deltaTime);
        if (button.position == points[position + 1].position)
        {
            isRight = false;
            position++;
        }

    }

    private void IsNotClickButtonHandle()
    {
        if (menuType == MenuType.Main) buttonText.text = mainMenuNames[position];
        else if (menuType == MenuType.Setting) buttonText.text = settingMenuNames[position];

        if (!Input.GetKeyDown(KeyCode.Z)) return;

        if (button.position == points[position].position)
        {
            if (menuType == MenuType.Main)
            {
                switch (position)
                {
                    case 0:
                        StartButton();
                        break;
                    case 1:
                        SettingButton();
                        break;
                    case 2:
                        ExitButton();
                        break;
                }
            }
            else if(menuType == MenuType.Setting)
            {
                switch (position)
                {
                    case 0:
                        
                        break;
                    case 1:
                        
                        break;
                    case 2:
                        BackButton();
                        break;
                }
            }
        }
    }
    private void IsClickButtonHandle()
    {
        if (!Input.GetKeyDown(KeyCode.Z)) return;

        if (menuType == MenuType.InMain)
        {
            if (!iconObj_M.activeSelf) return;
            if (icon_M.position == mainMenuPoints[position].position)
            {
                switch (position)
                {
                    case 0:
                        SceneManager.LoadScene("10_InGame");
                        break;
                    case 1:
                        BackButton();
                        break;
                }
            }
        }
        else if (menuType == MenuType.Exit)
        {
            if(!iconObj_E.activeSelf) return;
            if(icon_E.position == exitMenuPoints[position].position)
            {
                switch (position)
                {
                    case 0:
                        Application.Quit();
                        Debug.Log("게임에서 나가셨습니다");
                        break;
                    case 1:
                        BackButton();
                        break;
                }
            }
        }
    }

    public void StartButton()
    {
        isClick = true;
        title.DOPauseAllById("2");          // Title 둥실둥실 애니메이션 멈춤
        title.DORestartById("1");           // Title을 위로 올림
        menuBackground.DORestartById("0");  // 배경화면을 아래로 내림

        position = 0;
        menuType = MenuType.InMain;
    }

    public void SettingButton()
    {
        isClick = false;
        menuType = MenuType.Setting;

        Debug.Log(menuType.ToString());

        mainSelectBar.DORestartById("1");
        settingSelectBar.DORestartById("0");
        position = 0;
        button = selectButton_S;
        buttonText = selectButtonText_S;
        InitList(videoPoint, audioPoint, exitPoint_S);
    }

    public void ExitButton()
    {
        isClick = true;
        title.DOPauseAllById("2");          // Title 둥실둥실 애니메이션 멈춤
        title.DORestartById("1");           // Title을 위로 올림
        exitBackground.DORestartById("0");

        position = 0;
        menuType = MenuType.Exit;
        
    }

    public void VideoButton()
    {

    }

    public void AudioButton()
    {

    }

    public void BackButton()
    {
        if (menuType == MenuType.Setting)
        {
            isClick = false;
            menuType = MenuType.Main;
            mainSelectBar.DORestartById("0");
            settingSelectBar.DORestartById("1");
            position = 0;
            button = selectButton_M;
            buttonText = selectButtonText_M;
            InitList(startPoint, settingPoint, exitPoint_M);
        }
        if(menuType == MenuType.InMain)
        {
            isClick = false;
            menuType = MenuType.Main;
            title.DORestartById("0");
            mainSelectBar.DORestartById("0");
            menuBackground.DORestartById("1");
            position = 0;
            icon_M.position = mainMenuPoints[position].position;
            button = selectButton_M;
            buttonText = selectButtonText_M;
            InitList(startPoint, settingPoint, exitPoint_M);
        }
        if(menuType == MenuType.Exit)
        {
            isClick = false;
            menuType = MenuType.Main;
            title.DORestartById("0");
            mainSelectBar.DORestartById("0");
            exitBackground.DORestartById("1");
            position = 0;
            icon_E.position = exitMenuPoints[position].position;
            button = selectButton_M;
            buttonText = selectButtonText_M;
            InitList(startPoint, settingPoint, exitPoint_M);
        }
    }

    private void MenuTypeSlideAnimation()
    {
        switch (menuType)
        {
            case MenuType.Main:
                sliderHandler_M.StartSlideAnimation();
                break;
            case MenuType.Setting:
                sliderHandler_S.StartSlideAnimation();
                break;
        }
    }
}
