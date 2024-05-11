using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSign : Sign
{
    private void Start()
    {
        this.Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.sprite = selectedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    
}
