using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ProgressType
{
    Wait,
    Start,
    Pause,
    Die
}

public class Sign : MonoBehaviour
{
    protected SpriteRenderer SpriteRenderer;

    public Sprite normalSprite;
    public Sprite selectedSprite;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Init();
    }
    
    private void Init()
    {
        SpriteRenderer.sprite = normalSprite;
    }
}
