using UnityEngine;
using UnityEngine.UI;

public class PlayerBullet : MonoBehaviour
{
    public float shootSpeed;

    public Text atkText;

    private int _currentBulletAtk;
    private Player _player;

    private void Update()
    {
        var next = new Vector3(0, 1 * shootSpeed * Time.deltaTime, 0);
        transform.position += next;

        if (transform.position.y >= 10.5f) ObjectPool.instance.ResetForPool(gameObject);
    }

    private void OnEnable()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            if (Boss.Instance.boss != null && !Boss.Instance.isDie)
                //Boss.Instance.boss.GetComponent<HandInfo>().hp -= _player.currentPlayerAtk;
            ObjectPool.instance.ResetForPool(gameObject);
        }
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}