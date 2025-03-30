using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum MenuType
{
    Main = 0,
    InMain,
    Setting = 10,
    Video,
    Audio,
    Other = 20,
    Maker,
    HowToPlay,
    Exit = 30
}

public class MenuMain : MonoBehaviour
{
    [Header("NoSelectMainMenu")]
    public RectTransform title;                 
    public RectTransform mainSelectBar;          
    public RectTransform startPoint;
    public RectTransform settingPoint;
    public RectTransform otherPoint;
    [FormerlySerializedAs("exitPoint_M")] public RectTransform exitPointM;
    [FormerlySerializedAs("selectButton_M")] public RectTransform selectButtonM;
    [FormerlySerializedAs("selectButtonText_M")] public TMP_Text selectButtonTextM;
    
    [Header("NoSelectSettingMenu")]
    public RectTransform settingSelectBar;
    public RectTransform videoPoint;
    public RectTransform audioPoint;
    [FormerlySerializedAs("exitPoint_S")] public RectTransform exitPointS;
    [FormerlySerializedAs("selectButton_S")] public RectTransform selectButtonS;
    [FormerlySerializedAs("selectButtonText_S")] public TMP_Text selectButtonTextS;

    [Header("NoSelectOtherMenu")]
    public RectTransform otherSelectBar;
    [FormerlySerializedAs("howtoplayPoint")] public RectTransform howToPlayPoint;
    [FormerlySerializedAs("howtoplayImage")] public Image howToPlayImage;
    public TMP_Text howToPlayDiagramText;
    public RectTransform makerPoint;
    [FormerlySerializedAs("exitPoint_O")] public RectTransform exitPointO;
    [FormerlySerializedAs("selectButton_O")] public RectTransform selectButtonO;
    [FormerlySerializedAs("selectButtonText_O")] public TMP_Text selectButtonTextO;

    [Header("SelectMainMenu")]
    public RectTransform menuRect;
    [FormerlySerializedAs("icon_M")] public RectTransform iconM;
    [FormerlySerializedAs("iconObj_M")] public GameObject iconObjM;

    [Header("SelectCreatorMenu")]
    public Image makerImage;
    public TMP_Text plannerText;
    public TMP_Text programmerText;
    public TMP_Text artistText;

    [Header("SelectHowToPlayMenu")]
    [FormerlySerializedAs("howtoplayBackground")] public RectTransform howToPlayBackground;

    [Header("SelectAudioMenu")]
    public RectTransform audioRect;

    [Header("SelectExitMenu")]
    public RectTransform exitRect;
    [FormerlySerializedAs("icon_E")] public RectTransform iconE;
    [FormerlySerializedAs("iconObj_E")] public GameObject iconObjE;

    [Space(10)]
    public TMP_Text buttonText;
    public RectTransform button;

    public List<RectTransform> points;
    public List<RectTransform> mainMenuPoints;
    public List<RectTransform> otherMenuPoints;
    public List<RectTransform> exitMenuPoints;

    [FormerlySerializedAs("sliderHandler_M")] public SliderAnimationHandler sliderHandlerM;
    [FormerlySerializedAs("sliderHandler_S")] public SliderAnimationHandler sliderHandlerS;
    [FormerlySerializedAs("sliderHandler_O")] public SliderAnimationHandler sliderHandlerO;

    public Image fadeImage;
    
    private int _position = 0;

    public bool isLeft = false;
    public bool isRight = false;

    public bool isClick = false;

    public MenuType menuType;

    private readonly string[] _mainMenuNames = { "Start", "Setting", "Other", "Exit" };
    private readonly string[] _settingMenuNames = { "Video", "Audio", "Back" };
    private readonly string[] _otherMenuNames = { "HowTo", "Credit", "Back" };
    
    private AnimationManager _animationManager;
    private TextTyping _textTyping;

    private void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
        _textTyping = GetComponent<TextTyping>();
        _animationManager.FadeOutAnimation(fadeImage, 2.5f, 0, new Color(0, 0, 0));
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM(AudioManager.Instance.menuBGM);
        _position = 0;
        Init();
    }

    private void Init()
    {
        menuType = MenuType.Main;
        InitList(startPoint, settingPoint, otherPoint, exitPointM);
        button.position = points[0].position;

        _animationManager.MoveAnimation(title, 0.5f, 0.5f, new Vector2(0, 0));
        _animationManager.MoveAnimation(mainSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
    }

    //private void InitList(RectTransform point1, RectTransform point2, RectTransform point3)
    //{
    //    points.Clear();
    //    points.Add(point1);
    //    points.Add(point2);
    //    points.Add(point3);

    //    button.position = points[0].position;
    //}

    private void InitList(params RectTransform[] pointArray)
    {
        this.points.Clear();
        foreach (var t in pointArray)
        {
            this.points.Add(t);
        }
        button.position = this.points[0].position;
    }

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
            IsClickButtonHandle_2();
        }
        else
        {
            IsNotClickHandle();
            IsNotClickButtonHandle();
        }
    }
    
    private void IsClickHandle()
    {
        switch (menuType)
        {
            case MenuType.InMain when Input.GetKeyDown(KeyCode.LeftArrow):
            {
                if (iconM.position == mainMenuPoints[_position].position && _position > 0 && iconObjM.activeSelf)
                {
                    AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
                    iconM.position = mainMenuPoints[_position - 1].position;
                    _position--;
                }
                break;
            }
            case MenuType.InMain:
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (iconM.position == mainMenuPoints[_position].position && _position < mainMenuPoints.Count - 1 && iconObjM.activeSelf)
                    {
                        AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
                        iconM.position = mainMenuPoints[_position + 1].position;
                        _position++;
                    }
                }
                break;
            }
            case MenuType.Exit:
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (iconE.position == exitMenuPoints[_position].position && _position > 0 && iconObjE.activeSelf)
                    {
                        AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
                        iconE.position = exitMenuPoints[_position - 1].position;
                        _position--;
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (iconE.position == exitMenuPoints[_position].position && _position < exitMenuPoints.Count - 1 && iconObjE.activeSelf)
                    {
                        AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
                        iconE.position = exitMenuPoints[_position + 1].position;
                        _position++;
                    }
                }
                break;
            }
        }
    }
    
    private void IsNotClickHandle()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (button.position != points[_position].position || _position <= 0) return;
            AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
            MenuTypeSlideAnimation();
            isLeft = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (button.position != points[_position].position || _position >= points.Count - 1) return;
            AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
            MenuTypeSlideAnimation();
            isRight = true;
        }
    }

    private void ButtonMovement()
    {
        if (isLeft) IsLeftHandle();
        if (isRight) IsRightHandle();
    }
    
    private void IsLeftHandle()
    {
        button.position = Vector3.MoveTowards(button.position, points[_position - 1].position, 20f * Time.deltaTime);
        if (button.position != points[_position - 1].position) return;
        isLeft = false;
        _position--;
    }
    
    private void IsRightHandle()
    {
        button.position = Vector3.MoveTowards(button.position, points[_position + 1].position, 20f * Time.deltaTime);
        if (button.position != points[_position + 1].position) return;
        isRight = false;
        _position++;
    }

    private void IsNotClickButtonHandle()
    {
        buttonText.text = menuType switch
        {
            MenuType.Main => _mainMenuNames[_position],
            MenuType.Setting => _settingMenuNames[_position],
            MenuType.Other => _otherMenuNames[_position],
            var _ => buttonText.text
        };

        if (!Input.GetKeyDown(KeyCode.Z)) return;

        AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonClick);
        if (button.position != points[_position].position) return;
        switch (menuType)
        {
            case MenuType.Main:
                switch (_position)
                {
                    case 0:
                        StartButton();
                        break;
                    case 1:
                        SettingButton();
                        break;
                    case 2:
                        OtherButton();
                        break;
                    case 3:
                        ExitButton();
                        break;
                }
                break;
            case MenuType.Setting:
                switch (_position)
                {
                    case 0:
                        // VideoButton
                        break;
                    case 1:
                        AudioButton();
                        break;
                    case 2:
                        BackButton();
                        break;
                }

                break;
            case MenuType.Other:
                switch (_position)
                {
                    case 0:
                        HowtoPlayButton();
                        break;
                    case 1:
                        MakerButton();
                        break;
                    case 2:
                        BackButton();
                        break;
                }
                break;
        }
    }
    
    private void IsClickButtonHandle()
    {
        if (!Input.GetKeyDown(KeyCode.Z)) return;

        AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonClick);
        switch (menuType)
        {
            case MenuType.InMain when !iconObjM.activeSelf:
                return;
            case MenuType.InMain:
            {
                if (iconM.position == mainMenuPoints[_position].position)
                {
                    switch (_position)
                    {
                        case 0:
                            LoadingManager.LoadScene("10_InGame", "99_Loading");
                            break;
                        case 1:
                            BackButton();
                            break;
                    }
                }

                break;
            }
            case MenuType.Exit when !iconObjE.activeSelf: return;
            case MenuType.Exit:
            {
                if(iconE.position == exitMenuPoints[_position].position)
                {
                    switch (_position)
                    {
                        case 0:
                            Application.Quit();
                            break;
                        case 1:
                            BackButton();
                            break;
                    }
                }
                break;
            }
        }
    }

    private void IsClickButtonHandle_2()
    {
        if (!Input.GetKeyDown(KeyCode.X)) return;

        if(menuType == MenuType.Audio)
        {
            BackButton();
        }
    }

    private void StartButton()
    {
        isClick = true;
        _animationManager.MoveAnimation(title, 0.5f, 0f, new Vector2(0, 200));
        _animationManager.MoveAnimation(mainSelectBar, 0.5f, 0f, new Vector2(0f, -130f));  
        _animationManager.MoveAnimation(menuRect, 0.5f, 0.5f, new Vector2(0, 0));

        _position = 0;
        menuType = MenuType.InMain;
    }

    private void SettingButton()
    {
        isClick = false;
        menuType = MenuType.Setting;
        _animationManager.MoveAnimation(mainSelectBar, 0.5f, 0f, new Vector2(0f, -130f));
        _animationManager.MoveAnimation(settingSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
        _position = 0;
        button = selectButtonS;
        buttonText = selectButtonTextS;
        InitList(videoPoint, audioPoint, exitPointS);
    }

    private void OtherButton()
    {
        isClick = false;
        menuType = MenuType.Other;
        _animationManager.MoveAnimation(mainSelectBar, 0.5f, 0f, new Vector2(0f, -130f));
        _animationManager.MoveAnimation(otherSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
        _position = 0;
        button = selectButtonO;
        buttonText = selectButtonTextO;
        InitList(howToPlayPoint, makerPoint, exitPointO);
    }

    private void ExitButton()
    {
        isClick = true;
        _animationManager.MoveAnimation(title, 0.5f, 0f, new Vector2(0f, 200f));
        _animationManager.MoveAnimation(mainSelectBar, 0.5f, 0f, new Vector2(0f, -130f));   
        _animationManager.MoveAnimation(exitRect, 0.5f, 0.5f, new Vector2(0, 0));
        _position = 0;
        menuType = MenuType.Exit;
    }

    private void VideoButton()
    {

    }

    private void AudioButton()
    {
        isClick = true;
        _animationManager.MoveAnimation(title, 0.5f, 0f, new Vector2(0f, 200f));
        _animationManager.MoveAnimation(settingSelectBar, 0.5f, 0f, new Vector2(0f, -200f));    
        _animationManager.MoveAnimation(audioRect, 0.5f, 0.5f, new Vector2(0, 0));
        _position = 0;
        menuType = MenuType.Audio;
    }

    private void BackButton()
    {
        isClick = false;
        _position = 0;
        button = selectButtonM;
        buttonText = selectButtonTextM;
        
        switch (menuType)
        {
            case MenuType.Setting:
                MoveMenu(MenuType.Main, title, mainSelectBar, settingSelectBar, new Vector2(0, 0), new Vector2(0f, 0f), new Vector2(0f, -200f));
                InitList(startPoint, settingPoint, otherPoint, exitPointM);
                break;
            case MenuType.InMain:
                MoveMenu(MenuType.Main, title, mainSelectBar, menuRect, new Vector2(0, 0), new Vector2(0, 0),new Vector2(0, 550));
                iconM.position = mainMenuPoints[_position].position;
                InitList(startPoint, settingPoint, otherPoint, exitPointM);
                break;
            case MenuType.Other:
                MoveMenu(MenuType.Main, title, mainSelectBar, otherSelectBar,new Vector2(0f, 0f),  new Vector2(0f, 0f), new Vector2(0f, -270f));
                InitList(startPoint, settingPoint, otherPoint, exitPointM);
                break;
            case MenuType.Exit:
                MoveMenu(MenuType.Main, title, mainSelectBar, exitRect, new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 850));
                iconE.position = exitMenuPoints[_position].position;
                InitList(startPoint, settingPoint, otherPoint, exitPointM);
                break;
            case MenuType.Audio:
                AudioManager.Instance.ApplyChanges();
                MoveMenu(MenuType.Setting, title, settingSelectBar, audioRect, new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 1150));
                button = selectButtonS;
                buttonText = selectButtonTextS;
                InitList(videoPoint, audioPoint, exitPointS);
                break;
            default:
                break;
        }
    }
    
    private void MoveMenu(MenuType targetMenuType, RectTransform titleTransform, RectTransform selectBar, RectTransform background, Vector2 targetTitlePos, Vector2 targetSelectBarPos, Vector2 targetBackgroundPos)
    {
        menuType = targetMenuType;
        
        if (!titleTransform) return;
        _animationManager.MoveAnimation(titleTransform, 0.5f, 0.5f, targetTitlePos);
        
        if (!selectBar) return;
        _animationManager.MoveAnimation(selectBar, 0.5f, 0.5f, targetSelectBarPos);

        if (!background) return;
        _animationManager.MoveAnimation(background, 0.5f, 0f, targetBackgroundPos);
    }

    private void MenuTypeSlideAnimation()
    {
        switch (menuType)
        {
            case MenuType.Main:
                sliderHandlerM.StartSlideAnimation();
                break;
            case MenuType.Setting:
                sliderHandlerS.StartSlideAnimation();
                break;
            case MenuType.Other:
                sliderHandlerO.StartSlideAnimation();
                break;
        }
    }

    private void HowtoPlayButton()
    {
        isClick = true;
        _animationManager.MoveAnimation(title, 0.5f, 0f, new Vector2(0, 200));      
        _animationManager.MoveAnimation(otherSelectBar, 0.5f, 0f, new Vector2(0f, -270f));
        _animationManager.FadeInAnimation(howToPlayImage, 1, 0.5f, new Color(1, 1, 1), () =>
        {
            _animationManager.FadeInAnimation(howToPlayDiagramText, 1, 0.5f, new Color(0.333f, 0.333f, 0.333f), null, null);
        }, () =>
        {
            _textTyping.StartTyping();
        });

        _position = 0;
        menuType = MenuType.HowToPlay;
    }

    public void EndHowToPlay()
    {
        isClick = false;
        menuType = MenuType.Other;
        _animationManager.MoveAnimation(title, 0.5f, 0.5f, new Vector2(0, 0));
        _animationManager.MoveAnimation(otherSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
        _position = 0;
        button = selectButtonO;
        buttonText = selectButtonTextO;
        InitList(howToPlayPoint, makerPoint, exitPointO);

    }

    private void MakerButton()
    {
        isClick = true;
        _animationManager.MoveAnimation(title, 0.5f, 0f, new Vector2(0, 200));       
        _animationManager.MoveAnimation(otherSelectBar, 0.5f, 0f, new Vector2(0f, -270f));
        
        _animationManager.FadeInAnimation(makerImage, 1, 0.5f, new Color(1, 1, 1), () =>
        {
            _animationManager.FadeInAnimation(plannerText, 1, 0, new Color(0, 0, 0), null, null);
            _animationManager.FadeInAnimation(programmerText, 1, 0, new Color(0, 0, 0), null, null);
            _animationManager.FadeInAnimation(artistText, 1, 0, new Color(0, 0, 0), null, null);
        }, () =>
        {
            _animationManager.FadeOutAnimation(makerImage, 1, 2f, new Color(1, 1, 1), () =>
            {
                _animationManager.FadeOutAnimation(plannerText, 1, 0, new Color(0, 0, 0), null, null);
                _animationManager.FadeOutAnimation(programmerText, 1, 0, new Color(0, 0, 0), null, null);
                _animationManager.FadeOutAnimation(artistText, 1, 0, new Color(0, 0, 0), null, null);
            }, EndHowToPlay);
        });

        _position = 0;
        menuType = MenuType.Maker;
    }

    public void EndMaker()
    {
        isClick = false;
        menuType= MenuType.Other;
        _animationManager.MoveAnimation(title, 0.5f, 0.5f, new Vector2(0, 0));
        _animationManager.MoveAnimation(otherSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
        _position = 0;
        button = selectButtonO;
        buttonText = selectButtonTextO;
        InitList(howToPlayPoint, makerPoint, exitPointO);
    }
}
