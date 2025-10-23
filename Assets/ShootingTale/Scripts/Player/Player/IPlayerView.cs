using System;
using System.Collections;
using UnityEngine;

public interface IPlayerView
{
    void UpdateHealthText(int health);
    
    // AttackBar
    Vector3 GetLinePosition();
    GameObject GetLineObject();
    
    void ResetLinePosition();
    void SetActive(bool isActive);
    void LineMove(Action onPlay, Action onPlaying, Action onComplete);
}
