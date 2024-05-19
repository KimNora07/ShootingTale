using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public DOTweenAnimation yes_M;
    public DOTweenAnimation no_M;
    public RectTransform icon_M;
    public GameObject iconObj;

    [Space(10)]
    public TMP_Text buttonText;
    public RectTransform button;

    public List<RectTransform> points;
    public List<RectTransform> mainMenuPoints;

    private int position = 0;

    public bool isLeft = false;
    public bool isRight = false;

    public bool isClick = false;

    public MenuType menuType;

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

    private void Update()
    {
        SelectBarControl();          
    }

    /// <summary>
    /// 좌우 방향키로 버튼을 컨트롤
    /// </summary>
    private void SelectBarControl()
    {
        if (!isClick)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (button.position == points[position].position && position > 0)
                {
                    isLeft = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (button.position == points[position].position && position < points.Count - 1)
                {
                    isRight = true;
                }
            }
        }

        if (isLeft)
        {
            //button.position = Vector3.Lerp(button.position, points[position - 1].position, 0.025f);
            button.position = Vector3.MoveTowards(button.position, points[position - 1].position, 0.25f);
            if (button.position == points[position - 1].position)
            {
                isLeft = false;
                position--;
            }
        }

        if (isRight)
        {
            //button.position = Vector3.Lerp(button.position, points[position + 1].position, 0.025f);
            button.position = Vector3.MoveTowards(button.position, points[position + 1].position, 0.25f);
            if (button.position == points[position + 1].position)
            {
                isRight = false;
                position++;
            }
        }     

        if (!isClick)
        {
            if (points[0].position == button.position)
            {
                if (menuType == MenuType.Main)
                {
                    buttonText.text = "시작";
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        isClick = true;
                        StartButton();
                    }
                }

                if (menuType == MenuType.Setting)
                {
                    buttonText.text = "비디오";
                    if (Input.GetKeyDown(KeyCode.Z))
                    {

                    }
                }
            }

            if (points[1].position == button.position)
            {
                if (menuType == MenuType.Main)
                {
                    buttonText.text = "설정";
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        SettingButton();
                    }
                }

                if (menuType == MenuType.Setting)
                {
                    buttonText.text = "오디오";
                    if (Input.GetKeyDown(KeyCode.Z))
                    {

                    }
                }

            }

            if (points[2].position == button.position)
            {
                if (menuType == MenuType.Main)
                {
                    buttonText.text = "나가기";
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        ExitButton();
                    }
                }

                if (menuType == MenuType.Setting)
                {
                    buttonText.text = "나가기";
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        BackButton();
                    }
                }
            }
        }

        if (isClick)
        {
            if(menuType == MenuType.InMain)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (icon_M.position == mainMenuPoints[position].position && position > 0 && iconObj.activeSelf)
                    {
                        icon_M.position = mainMenuPoints[position - 1].position;
                        position--;
                    }
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (icon_M.position == mainMenuPoints[position].position && position < mainMenuPoints.Count - 1 && iconObj.activeSelf)
                    {
                        icon_M.position = mainMenuPoints[position + 1].position;
                        position++;
                    }
                }

                if (icon_M.position == mainMenuPoints[0].position && iconObj.activeSelf)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        SceneManager.LoadScene("10_InGame");
                    }
                }      

                if(icon_M.position == mainMenuPoints[1].position && iconObj.activeSelf)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        
                    }
                }
            }
        }
    }

    /// <summary>
    /// 시작 버튼을 눌렀을 때
    /// </summary>
    public void StartButton()
    {
        title.DOPauseAllById("2");          // Title 둥실둥실 애니메이션 멈춤
        title.DORestartById("1");           // Title을 위로 올림
        menuBackground.DORestartById("0");  // 배경화면을 아래로 내림

        position = 0;
        menuType = MenuType.InMain;
    }

    public void SettingButton()
    {

        menuType = MenuType.Setting;

        Debug.Log(menuType.ToString());

        mainSelectBar.DORestartById("1");
        settingSelectBar.DORestartById("0");
        position = 0;
        button = selectButton_S;
        buttonText = selectButtonText_S;
        InitList(videoPoint, audioPoint, exitPoint_S);

        Debug.Log(menuType.ToString());
    }

    public void ExitButton()
    {
        menuType = MenuType.Exit;
        Debug.Log("게임을 끝냈습니다!");
    }

    public void VideoButton()
    {

    }

    public void AudioButton()
    {

    }

    public void BackButton()
    {
        menuType = MenuType.Main;
        mainSelectBar.DORestartById("0");
        settingSelectBar.DORestartById("1");
        position = 0;
        button = selectButton_M;
        buttonText = selectButtonText_M;
        InitList(startPoint, settingPoint, exitPoint_M);
    }
}
