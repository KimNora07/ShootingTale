using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBullet : MonoBehaviour
{
    public float shootSpeed;

    private int _currentBulletAtk;
    private Player _player;

    public Text atkText;

    private void OnEnable()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        var next = new Vector3(0, 1 * shootSpeed * Time.deltaTime, 0);
        this.transform.position += next;
        
        if(this.transform.position.y >= 10.5f)
        {
            ObjectPool.instance.ResetForPool(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            if(Boss.Instance.boss != null && !Boss.Instance.isDie)
            {
                Boss.Instance.boss.GetComponent<HandInfo>().hp -= _player.currentPlayerAtk;
            }
            ObjectPool.instance.ResetForPool(this.gameObject);
        }
    }
    private void Explode()
    {
        Destroy(this.gameObject);
    }
}
