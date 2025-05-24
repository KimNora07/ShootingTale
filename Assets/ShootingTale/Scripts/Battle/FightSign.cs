using UnityEngine;

public class FightSign : Sign
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantsManager.PlayerStr)) SpriteRenderer.sprite = selectedSprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantsManager.PlayerStr)) SpriteRenderer.sprite = normalSprite;
    }
}