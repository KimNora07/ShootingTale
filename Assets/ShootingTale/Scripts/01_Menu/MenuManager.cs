// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.Serialization;
// using UnityEngine.UI;
//
// public class MenuManager : MonoBehaviour
// {
//     [Header("SelectBar")] 
//     [SerializeField] private RectTransform mainSelectBar;
//     [SerializeField] private RectTransform settingSelectBar;
//     [SerializeField] private RectTransform otherSelectBar;
//     
//     [Header("MainBarPoint")]
//     [SerializeField] private RectTransform mainStartPoint;
//     [SerializeField] private RectTransform mainSettingPoint;
//     [SerializeField] private RectTransform mainOtherPoint;
//     [SerializeField] private RectTransform mainExitPoint;
//     
//     [Header("SettingBarPoint")]
//     [SerializeField] private RectTransform settingVideoPoint;
//     [SerializeField] private RectTransform settingAudioPoint;
//     [SerializeField] private RectTransform settingExitPoint;
//     
//     [Header("OtherBarPoint")]
//     [SerializeField] private RectTransform otherHowToPlayPoint;
//     [SerializeField] private RectTransform otherCreatorPoint;
//     [SerializeField] private RectTransform otherExitPoint;
//     
//     
//     [Header("NoSelectMainMenu")]
//     public RectTransform title;                 
//     [FormerlySerializedAs("selectButton_M")] public RectTransform selectButtonM;
//     [FormerlySerializedAs("selectButtonText_M")] public TMP_Text selectButtonTextM;
//     
//     [Header("NoSelectSettingMenu")]
//     [FormerlySerializedAs("selectButton_S")] public RectTransform selectButtonS;
//     [FormerlySerializedAs("selectButtonText_S")] public TMP_Text selectButtonTextS;
//
//     [Header("NoSelectOtherMenu")]
//     [FormerlySerializedAs("howtoplayImage")] public Image howToPlayImage;
//     public TMP_Text howToPlayDiagramText;
//     [FormerlySerializedAs("selectButton_O")] public RectTransform selectButtonO;
//     [FormerlySerializedAs("selectButtonText_O")] public TMP_Text selectButtonTextO;
//
//     [Header("SelectMainMenu")]
//     public RectTransform menuRect;
//     [FormerlySerializedAs("icon_M")] public RectTransform iconM;
//     [FormerlySerializedAs("iconObj_M")] public GameObject iconObjM;
//
//     [Header("SelectCreatorMenu")]
//     public Image makerImage;
//     public TMP_Text plannerText;
//     public TMP_Text programmerText;
//     public TMP_Text artistText;
//
//     [Header("SelectHowToPlayMenu")]
//     [FormerlySerializedAs("howtoplayBackground")] public RectTransform howToPlayBackground;
//
//     [Header("SelectAudioMenu")]
//     public RectTransform audioRect;
//
//     [Header("SelectExitMenu")]
//     public RectTransform exitRect;
//     [FormerlySerializedAs("icon_E")] public RectTransform iconE;
//     [FormerlySerializedAs("iconObj_E")] public GameObject iconObjE;
//
//     [Space(10)]
//     public TMP_Text buttonText;
//     public RectTransform button;
//
//     public List<RectTransform> points;
//     public List<RectTransform> mainMenuPoints;
//     public List<RectTransform> otherMenuPoints;
//     public List<RectTransform> exitMenuPoints;
//
//     [FormerlySerializedAs("sliderHandler_M")] public SliderAnimationHandler sliderHandlerM;
//     [FormerlySerializedAs("sliderHandler_S")] public SliderAnimationHandler sliderHandlerS;
//     [FormerlySerializedAs("sliderHandler_O")] public SliderAnimationHandler sliderHandlerO;
//
//     public Image fadeImage;
//     
//     private int _position = 0;
//
//     public bool isLeft = false;
//     public bool isRight = false;
//
//     public bool isClick = false;
//
//     public MenuType menuType;
//
//     private readonly string[] _mainMenuNames = { "Start", "Setting", "Other", "Exit" };
//     private readonly string[] _settingMenuNames = { "Video", "Audio", "Back" };
//     private readonly string[] _otherMenuNames = { "HowTo", "Credit", "Back" };
//     
//    private TextTyping _textTyping;
//
//     private void Awake()
//     {
//         _textTyping = GetComponent<TextTyping>();
//         AnimationUtility.FadeOutAnimation(this, fadeImage, 2.5f, 0, new Color(0, 0, 0), null, null);
//     }
//
//     private void Start()
//     {
//         AudioManager.Instance.PlayBGM(AudioManager.Instance.menuBGM);
//         _position = 0;
//         Init();
//     }
//
//     private void Init()
//     {
//         menuType = MenuType.Main;
//         InitList(mainStartPoint, mainSettingPoint, mainOtherPoint, mainExitPoint);
//         button.position = points[0].position;
//
//         AnimationUtility.MoveAnimation(this, title, 0.5f, 0.5f, new Vector2(0, 0));
//         AnimationUtility.MoveAnimation(this, mainSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
//     }
//
//     private void InitList(params RectTransform[] pointArray)
//     {
//         this.points.Clear();
//         foreach (var t in pointArray)
//         {
//             this.points.Add(t);
//         }
//         button.position = this.points[0].position;
//     }
//
//     private void Update()
//     {
//         KeyInput();
//         ButtonMovement();         
//     }
//
//     private void KeyInput()
//     {
//         if(isClick)
//         {
//             IsClickHandle();
//             IsClickButtonHandle();
//             IsClickButtonHandle_2();
//         }
//         else
//         {
//             IsNotClickHandle();
//             IsNotClickButtonHandle();
//         }
//     }
//     
//     private void IsClickHandle()
//     {
//         switch (menuType)
//         {
//             case MenuType.InMain when Input.GetKeyDown(KeyCode.LeftArrow):
//             {
//                 if (iconM.position == mainMenuPoints[_position].position && _position > 0 && iconObjM.activeSelf)
//                 {
//                     AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
//                     iconM.position = mainMenuPoints[_position - 1].position;
//                     _position--;
//                 }
//                 break;
//             }
//             case MenuType.InMain when Input.GetKeyDown(KeyCode.RightArrow):
//             {
//                 if (iconM.position == mainMenuPoints[_position].position && _position < mainMenuPoints.Count - 1 && iconObjM.activeSelf)
//                 {
//                     AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
//                     iconM.position = mainMenuPoints[_position + 1].position;
//                     _position++;
//                 }
//                 break;
//             }
//             case MenuType.Exit when Input.GetKeyDown(KeyCode.LeftArrow):
//             {
//                 if (iconE.position == exitMenuPoints[_position].position && _position > 0 && iconObjE.activeSelf)
//                 {
//                     AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
//                     iconE.position = exitMenuPoints[_position - 1].position;
//                     _position--;
//                 }
//                 break;
//             }
//             case MenuType.Exit when Input.GetKeyDown(KeyCode.RightArrow):
//             {
//                 if (iconE.position == exitMenuPoints[_position].position && _position < exitMenuPoints.Count - 1 && iconObjE.activeSelf)
//                 {
//                     AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
//                     iconE.position = exitMenuPoints[_position + 1].position;
//                     _position++;
//                 }
//                 break;
//             }
//         }
//     }
//     
//     private void IsNotClickHandle()
//     {
//         if (Input.GetKeyDown(KeyCode.LeftArrow))
//         {
//             if (button.position != points[_position].position || _position <= 0) return;
//             AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
//             MenuTypeSlideAnimation();
//             isLeft = true;
//         }
//         else if (Input.GetKeyDown(KeyCode.RightArrow))
//         {
//             if (button.position != points[_position].position || _position >= points.Count - 1) return;
//             AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
//             MenuTypeSlideAnimation();
//             isRight = true;
//         }
//     }
//
//     private void ButtonMovement()
//     {
//         if (isLeft) IsLeftHandle();
//         if (isRight) IsRightHandle();
//     }
//     
//     private void IsLeftHandle()
//     {
//         button.position = Vector3.MoveTowards(button.position, points[_position - 1].position, 20f * Time.deltaTime);
//         if (button.position != points[_position - 1].position) return;
//         isLeft = false;
//         _position--;
//     }
//     
//     private void IsRightHandle()
//     {
//         button.position = Vector3.MoveTowards(button.position, points[_position + 1].position, 20f * Time.deltaTime);
//         if (button.position != points[_position + 1].position) return;
//         isRight = false;
//         _position++;
//     }
//
//     private void IsNotClickButtonHandle()
//     {
//         buttonText.text = menuType switch
//         {
//             MenuType.Main => _mainMenuNames[_position],
//             MenuType.Setting => _settingMenuNames[_position],
//             MenuType.Other => _otherMenuNames[_position],
//             var _ => buttonText.text
//         };
//
//         if (!Input.GetKeyDown(KeyCode.Z)) return;
//
//         AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonClick);
//         if (button.position != points[_position].position) return;
//         switch (menuType)
//         {
//             case MenuType.Main:
//                 switch (_position)
//                 {
//                     case 0:
//                         StartButton();
//                         break;
//                     case 1:
//                         SettingButton();
//                         break;
//                     case 2:
//                         OtherButton();
//                         break;
//                     case 3:
//                         ExitButton();
//                         break;
//                 }
//                 break;
//             case MenuType.Setting:
//                 switch (_position)
//                 {
//                     case 0:
//                         // VideoButton
//                         break;
//                     case 1:
//                         AudioButton();
//                         break;
//                     case 2:
//                         BackButton();
//                         break;
//                 }
//
//                 break;
//             case MenuType.Other:
//                 switch (_position)
//                 {
//                     case 0:
//                         HowtoPlayButton();
//                         break;
//                     case 1:
//                         MakerButton();
//                         break;
//                     case 2:
//                         BackButton();
//                         break;
//                 }
//                 break;
//         }
//     }
//     
//     private void IsClickButtonHandle()
//     {
//         if (!Input.GetKeyDown(KeyCode.Z)) return;
//
//         AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonClick);
//         switch (menuType)
//         {
//             case MenuType.InMain when !iconObjM.activeSelf:
//                 return;
//             case MenuType.InMain:
//             {
//                 if (iconM.position == mainMenuPoints[_position].position)
//                 {
//                     switch (_position)
//                     {
//                         case 0:
//                             LoadingManager.LoadScene("10_InGame", "99_Loading");
//                             break;
//                         case 1:
//                             BackButton();
//                             break;
//                     }
//                 }
//
//                 break;
//             }
//             case MenuType.Exit when !iconObjE.activeSelf: return;
//             case MenuType.Exit:
//             {
//                 if(iconE.position == exitMenuPoints[_position].position)
//                 {
//                     switch (_position)
//                     {
//                         case 0:
//                             Application.Quit();
//                             break;
//                         case 1:
//                             BackButton();
//                             break;
//                     }
//                 }
//                 break;
//             }
//         }
//     }
//
//     private void IsClickButtonHandle_2()
//     {
//         if (!Input.GetKeyDown(KeyCode.X)) return;
//
//         if(menuType == MenuType.Audio)
//         {
//             BackButton();
//         }
//     }
//
//     private void StartButton()
//     {
//         isClick = true;
//         AnimationUtility.MoveAnimation(this, title, 0.5f, 0f, new Vector2(0, 200));
//         AnimationUtility.MoveAnimation(this, mainSelectBar, 0.5f, 0f, new Vector2(0f, -130f));  
//         AnimationUtility.MoveAnimation(this, menuRect, 0.5f, 0.5f, new Vector2(0, 0));
//
//         _position = 0;
//         menuType = MenuType.InMain;
//     }
//
//     private void SettingButton()
//     {
//         isClick = false;
//         menuType = MenuType.Setting;
//         AnimationUtility.MoveAnimation(this, mainSelectBar, 0.5f, 0f, new Vector2(0f, -130f));
//         AnimationUtility.MoveAnimation(this, settingSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
//         _position = 0;
//         button = selectButtonS;
//         buttonText = selectButtonTextS;
//         InitList(settingVideoPoint, settingAudioPoint, settingExitPoint);
//     }
//
//     private void OtherButton()
//     {
//         isClick = false;
//         menuType = MenuType.Other;
//         AnimationUtility.MoveAnimation(this, mainSelectBar, 0.5f, 0f, new Vector2(0f, -130f));
//         AnimationUtility.MoveAnimation(this, otherSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
//         _position = 0;
//         button = selectButtonO;
//         buttonText = selectButtonTextO;
//         InitList(otherHowToPlayPoint, otherCreatorPoint, otherExitPoint);
//     }
//
//     private void ExitButton()
//     {
//         isClick = true;
//         AnimationUtility.MoveAnimation(this, title, 0.5f, 0f, new Vector2(0f, 200f));
//         AnimationUtility.MoveAnimation(this, mainSelectBar, 0.5f, 0f, new Vector2(0f, -130f));   
//         AnimationUtility.MoveAnimation(this, exitRect, 0.5f, 0.5f, new Vector2(0, 0));
//         _position = 0;
//         menuType = MenuType.Exit;
//     }
//
//     private void VideoButton()
//     {
//
//     }
//
//     private void AudioButton()
//     {
//         isClick = true;
//         AnimationUtility.MoveAnimation(this, title, 0.5f, 0f, new Vector2(0f, 200f));
//         AnimationUtility.MoveAnimation(this, settingSelectBar, 0.5f, 0f, new Vector2(0f, -200f));    
//         AnimationUtility.MoveAnimation(this, audioRect, 0.5f, 0.5f, new Vector2(0, 0));
//         _position = 0;
//         menuType = MenuType.Audio;
//     }
//
//     private void BackButton()
//     {
//         isClick = false;
//         _position = 0;
//         button = selectButtonM;
//         buttonText = selectButtonTextM;
//         
//         switch (menuType)
//         {
//             case MenuType.Setting:
//                 MoveMenu(MenuType.Main, title, mainSelectBar, settingSelectBar, new Vector2(0, 0), new Vector2(0f, 0f), new Vector2(0f, -200f));
//                 InitList(mainStartPoint, mainSettingPoint, mainOtherPoint, mainExitPoint);
//                 break;
//             case MenuType.InMain:
//                 MoveMenu(MenuType.Main, title, mainSelectBar, menuRect, new Vector2(0, 0), new Vector2(0, 0),new Vector2(0, 550));
//                 iconM.position = mainMenuPoints[_position].position;
//                 InitList(mainStartPoint, mainSettingPoint, mainOtherPoint, mainExitPoint);
//                 break;
//             case MenuType.Other:
//                 MoveMenu(MenuType.Main, title, mainSelectBar, otherSelectBar,new Vector2(0f, 0f),  new Vector2(0f, 0f), new Vector2(0f, -270f));
//                 InitList(mainStartPoint, mainSettingPoint, mainOtherPoint, mainExitPoint);
//                 break;
//             case MenuType.Exit:
//                 MoveMenu(MenuType.Main, title, mainSelectBar, exitRect, new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 850));
//                 iconE.position = exitMenuPoints[_position].position;
//                 InitList(mainStartPoint, mainSettingPoint, mainOtherPoint, mainExitPoint);
//                 break;
//             case MenuType.Audio:
//                 AudioManager.Instance.ApplyChanges();
//                 MoveMenu(MenuType.Setting, title, settingSelectBar, audioRect, new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 1150));
//                 button = selectButtonS;
//                 buttonText = selectButtonTextS;
//                 InitList(settingVideoPoint, settingAudioPoint, settingExitPoint);
//                 break;
//             default:
//                 break;
//         }
//     }
//     
//     private void MoveMenu(MenuType targetMenuType, RectTransform titleTransform, RectTransform selectBar, RectTransform background, Vector2 targetTitlePos, Vector2 targetSelectBarPos, Vector2 targetBackgroundPos)
//     {
//         menuType = targetMenuType;
//         
//         if (!titleTransform) return;
//         AnimationUtility.MoveAnimation(this, titleTransform, 0.5f, 0.5f, targetTitlePos);
//         
//         if (!selectBar) return;
//         AnimationUtility.MoveAnimation(this, selectBar, 0.5f, 0.5f, targetSelectBarPos);
//
//         if (!background) return;
//         AnimationUtility.MoveAnimation(this, background, 0.5f, 0f, targetBackgroundPos);
//     }
//
//     private void MenuTypeSlideAnimation()
//     {
//         switch (menuType)
//         {
//             case MenuType.Main:
//                 sliderHandlerM.StartSlideAnimation();
//                 break;
//             case MenuType.Setting:
//                 sliderHandlerS.StartSlideAnimation();
//                 break;
//             case MenuType.Other:
//                 sliderHandlerO.StartSlideAnimation();
//                 break;
//         }
//     }
//
//     private void HowtoPlayButton()
//     {
//         isClick = true;
//         AnimationUtility.MoveAnimation(this, title, 0.5f, 0f, new Vector2(0, 200));      
//         AnimationUtility.MoveAnimation(this, otherSelectBar, 0.5f, 0f, new Vector2(0f, -270f));
//         AnimationUtility.FadeInAnimation(this, howToPlayImage, 1, 0.5f, new Color(1, 1, 1), () =>
//         {
//             AnimationUtility.FadeInAnimation(this, howToPlayDiagramText, 1, 0.5f, new Color(0.333f, 0.333f, 0.333f), null, null);
//         }, () =>
//         {
//             _textTyping.StartTyping();
//         });
//
//         _position = 0;
//         menuType = MenuType.HowToPlay;
//     }
//
//     public void EndHowToPlay()
//     {
//         isClick = false;
//         menuType = MenuType.Other;
//         AnimationUtility.MoveAnimation(this, title, 0.5f, 0.5f, new Vector2(0, 0));
//         AnimationUtility.MoveAnimation(this, otherSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
//         _position = 0;
//         button = selectButtonO;
//         buttonText = selectButtonTextO;
//         InitList(otherHowToPlayPoint, otherCreatorPoint, otherExitPoint);
//     }
//
//     private void MakerButton()
//     {
//         isClick = true;
//         AnimationUtility.MoveAnimation(this, title, 0.5f, 0f, new Vector2(0, 200));       
//         AnimationUtility.MoveAnimation(this, otherSelectBar, 0.5f, 0f, new Vector2(0f, -270f));
//         
//         AnimationUtility.FadeInAnimation(this, makerImage, 1, 0.5f, new Color(1, 1, 1), () =>
//         {
//             AnimationUtility.FadeInAnimation(this, plannerText, 1, 0, new Color(0, 0, 0), null, null);
//             AnimationUtility.FadeInAnimation(this, programmerText, 1, 0, new Color(0, 0, 0), null, null);
//             AnimationUtility.FadeInAnimation(this, artistText, 1, 0, new Color(0, 0, 0), null, null);
//         }, () =>
//         {
//             AnimationUtility.FadeOutAnimation(this, makerImage, 1, 2f, new Color(1, 1, 1), () =>
//             {
//                 AnimationUtility.FadeOutAnimation(this, plannerText, 1, 0, new Color(0, 0, 0), null, null);
//                 AnimationUtility.FadeOutAnimation(this, programmerText, 1, 0, new Color(0, 0, 0), null, null);
//                 AnimationUtility.FadeOutAnimation(this, artistText, 1, 0, new Color(0, 0, 0), null, null);
//             }, EndHowToPlay);
//         });
//
//         _position = 0;
//         menuType = MenuType.Maker;
//     }
//
//     public void EndMaker()
//     {
//         isClick = false;
//         menuType= MenuType.Other;
//         AnimationUtility.MoveAnimation(this, title, 0.5f, 0.5f, new Vector2(0, 0));
//         AnimationUtility.MoveAnimation(this, otherSelectBar, 0.5f, 0.5f, new Vector2(0f, 0f));
//         _position = 0;
//         button = selectButtonO;
//         buttonText = selectButtonTextO;
//         InitList(otherHowToPlayPoint, otherCreatorPoint, otherExitPoint);
//     }
// }
