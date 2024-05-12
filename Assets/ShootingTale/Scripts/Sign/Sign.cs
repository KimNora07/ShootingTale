using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite normalSprite;
    public Sprite selectedSprite;


    protected virtual void Init()
    {
        spriteRenderer.sprite = normalSprite;
    }
}
