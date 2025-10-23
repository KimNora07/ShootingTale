using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    public TMP_Text playerHpText;
    public PlayerPresenter presenter;

    [Header("AttackBar")] 
    [SerializeField] private GameObject attackBar;
    [SerializeField] private RectTransform line;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;

    private void Awake()
    {
        presenter = new PlayerPresenter(this, new PlayerModel(20));
    }
    
    public void UpdateHealthText(int health) => playerHpText.text = PlayerInfo.Instance.hp.ToString();
    
    
    public Vector3 GetLinePosition() => line.transform.localPosition;
    public GameObject GetLineObject() => line.gameObject;
    
    public void ResetLinePosition() => line.transform.position = startPos.position;
    public void SetActive(bool isActive) => attackBar.SetActive(isActive);

    public void LineMove(Action onPlay, Action onPlaying, Action onComplete)
    {
        AnimationUtility.MoveAnimation(this, line, 3f, 0f, endPos.position, () =>
        {
            onPlay?.Invoke();
        }, onPlaying: () =>
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                AnimationUtility.StopMoveAnimation(this, line);
                onPlaying?.Invoke();
            }
        }, () =>
        {
            onComplete?.Invoke();
        });
    }
}
