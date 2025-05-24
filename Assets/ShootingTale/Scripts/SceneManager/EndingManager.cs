//System

using UnityEngine;
//UnityEngine

public class EndingManager : MonoBehaviour
{
    public Transform creditPos;
    public float creditSpeed;
    public Transform endPos;

    private void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBGM(AudioManager.Instance.endingBGM);
    }

    private void Update()
    {
        creditPos.position += new Vector3(0, creditSpeed * Time.deltaTime, 0);
        endPos.position += new Vector3(0, creditSpeed * Time.deltaTime, 0);

        if (endPos.position.y >= 0) LoadingManager.LoadScene("00_Splash", "99_Loading");
    }
}