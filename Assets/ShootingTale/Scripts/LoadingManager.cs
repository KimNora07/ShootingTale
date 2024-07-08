//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    private static string nextScene = null;     // 이동할 다음 씬의 이름을 저장하는 변수
    private static float loadingProgress = 0f;  // 로딩 진행 상황

    /// <summary>
    /// 이동할 다음 씬의 이름을 저장하고, 로딩씬으로 전환
    /// </summary>
    /// <param name="sceneName">이동할 다음 씬의 이름</param>
    /// <param name="loadingScene">로딩 씬 이름</param>
    public static void LoadScene(string sceneName = null, string loadingScene = null)
    {
        nextScene = sceneName;
        SceneManager.LoadScene(loadingScene);
    }

    /// <summary>
    /// 로딩 구현 코루틴
    /// </summary>
    /// <returns>yield return null : 1프레임 지연</returns>
    public static IEnumerator CoLoadSceneProgress()
    {
        // 비동기 씬 전환 방식으로 nextScene으로 이동
        AsyncOperation oper = SceneManager.LoadSceneAsync(nextScene);
        oper.allowSceneActivation = false;

        float time = 0f;

        // 씬 전환이 준비될 때 까지 루프
        while (!oper.isDone)
        {
            yield return null;

            // progress가 0.9 이전까지는 loadingProgress에 progress를 받음
            // 0.9 이상이 되었을 경우에는 직접 loadingProgres가 0.9 ~ 1까지 받음
            // loadingProgress가 1이상이 되었을 경우에는 씬 활성화 여부를 true로 반환
            if (oper.progress < 0.9f)
            {
                loadingProgress = oper.progress;
            }
            else
            {
                time += Time.unscaledDeltaTime;
                loadingProgress = Mathf.Lerp(0.9f, 1f, time);
                if (loadingProgress >= 1f)
                {
                    AudioManager.Instance.musicSource.Stop();
                    AudioManager.Instance.sfxSource.Stop();
                    oper.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    /// <summary>
    /// 버튼에 클릭이벤트를 주고 LoadScene 메소드를 실행
    /// </summary>
    /// <param name="button">클릭 이벤트를 줄 버튼</param>
    /// <param name="sceneName">이동할 다음 씬의 이름</param>
    /// <param name="loadingScene">로딩 씬 이름</param>
    public static void LoadScene(string sceneName = null, string loadingScene = null, Button button = null)
    {
        button.onClick.AddListener(delegate { LoadScene(sceneName, loadingScene); });
    }

    /// <summary>
    /// 좌클릭했을때 LoadScene 메소드를 실행
    /// </summary>
    /// <param name="sceneName">이동할 다음 씬의 이름</param>
    /// <param name="loadingScene">로딩 씬 이름</param>
    /// <param name="useClick">클릭을 사용하여 씬을 로드할지에 대한 여부</param>
    public static void LoadScene(string sceneName = null, string loadingScene = null, bool useClick = false)
    {
        if (useClick && Input.GetMouseButtonDown(0))
        {
            LoadScene(sceneName, loadingScene);
        }
    }
}
