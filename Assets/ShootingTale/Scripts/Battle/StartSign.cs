//UnityEngine

using UnityEngine;

public class StartSign : Sign
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) SpriteRenderer.sprite = selectedSprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) SpriteRenderer.sprite = normalSprite;
    }
}