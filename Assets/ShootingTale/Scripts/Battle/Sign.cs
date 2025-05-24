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
    public Sprite normalSprite;
    public Sprite selectedSprite;
    protected SpriteRenderer SpriteRenderer;

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