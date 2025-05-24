//System

using UnityEngine;
//UnityEngine

public enum SniperBulletPosition
{
    None,
    Left,
    Right
}

public class SniperBullet : MonoBehaviour
{
    public SniperBulletPosition SniperBulletPosition = SniperBulletPosition.None;
    public float moveSpeed = 30f;

    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (SniperBulletPosition == SniperBulletPosition.Left)
            transform.position += Vector3.right * (moveSpeed * Time.deltaTime);
        else if (SniperBulletPosition == SniperBulletPosition.Right)
            transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

        if (transform.position.x > 25f) Destroy(gameObject);
        else if (transform.position.x < -25f) Destroy(gameObject);
    }

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (!collision.GetComponent<PlayerInfo>().isInvincibility)
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(1);
                UIManager.Instance.playerHpText.text = PlayerInfo.Instance.hp.ToString();
                Destroy(gameObject);
            }
    }
}