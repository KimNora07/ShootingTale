using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBullet : MonoBehaviour
{
    public float shootSpeed;

    private int currentBulletAtk;
    private Player player;

    public Text atkText;

    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        Vector3 next = new Vector3(0, 1 * shootSpeed * Time.deltaTime, 0);
        this.transform.position += next;

        // ī�޶� ���� ���� �� ����
        if(this.transform.position.y >= 10.5f)
        {
            ObjectPool.instance.ResetForPool(this.gameObject);
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� Ÿ������ �� �Ѿ� ����
        if (collision.gameObject.CompareTag("Heart"))
        {
            if(Boss.Instance.boss != null && !Boss.Instance.isDie)
            {
                Vector3 pos = DamageTextController.Instance.uiCamera.transform.position;
                Boss.Instance.boss.GetComponent<HandInfo>().hp -= player.currentPlayerAtk;
                DamageTextController.Instance.CreateDamageText(pos, currentBulletAtk);
            }
            ObjectPool.instance.ResetForPool(this.gameObject);
        }
    }
    private void Explode()
    {
        Destroy(this.gameObject);
    }
}
