//System

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//UnityEngine

public class LoadingManager : MonoBehaviour
{
    private static string nextScene; // �̵��� ���� ���� �̸��� �����ϴ� ����
    private static float loadingProgress; // �ε� ���� ��Ȳ

    /// <summary>
    ///     �̵��� ���� ���� �̸��� �����ϰ�, �ε������� ��ȯ
    /// </summary>
    /// <param name="sceneName">�̵��� ���� ���� �̸�</param>
    /// <param name="loadingScene">�ε� �� �̸�</param>
    public static void LoadScene(string sceneName = null, string loadingScene = null)
    {
        nextScene = sceneName;
        SceneManager.LoadScene(loadingScene);
    }

    /// <summary>
    ///     �ε� ���� �ڷ�ƾ
    /// </summary>
    /// <returns>yield return null : 1������ ����</returns>
    public static IEnumerator CoLoadSceneProgress()
    {
        // �񵿱� �� ��ȯ ������� nextScene���� �̵�
        var oper = SceneManager.LoadSceneAsync(nextScene);
        oper.allowSceneActivation = false;

        var time = 0f;

        // �� ��ȯ�� �غ�� �� ���� ����
        while (!oper.isDone)
        {
            yield return null;

            // progress�� 0.9 ���������� loadingProgress�� progress�� ����
            // 0.9 �̻��� �Ǿ��� ��쿡�� ���� loadingProgres�� 0.9 ~ 1���� ����
            // loadingProgress�� 1�̻��� �Ǿ��� ��쿡�� �� Ȱ��ȭ ���θ� true�� ��ȯ
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
                    if (AudioManager.Instance)
                    {
                        AudioManager.Instance.musicSource.Stop();
                        AudioManager.Instance.sfxSource.Stop();
                    }

                    oper.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    /// <summary>
    ///     ��ư�� Ŭ���̺�Ʈ�� �ְ� LoadScene �޼ҵ带 ����
    /// </summary>
    /// <param name="button">Ŭ�� �̺�Ʈ�� �� ��ư</param>
    /// <param name="sceneName">�̵��� ���� ���� �̸�</param>
    /// <param name="loadingScene">�ε� �� �̸�</param>
    public static void LoadScene(string sceneName = null, string loadingScene = null, Button button = null)
    {
        button?.onClick.AddListener(delegate { LoadScene(sceneName, loadingScene); });
    }

    /// <summary>
    ///     ��Ŭ�������� LoadScene �޼ҵ带 ����
    /// </summary>
    /// <param name="sceneName">�̵��� ���� ���� �̸�</param>
    /// <param name="loadingScene">�ε� �� �̸�</param>
    /// <param name="useClick">Ŭ���� ����Ͽ� ���� �ε������� ���� ����</param>
    public static void LoadScene(string sceneName = null, string loadingScene = null, bool useClick = false)
    {
        if (useClick && Input.GetMouseButtonDown(0)) LoadScene(sceneName, loadingScene);
    }
}