using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float shootSpeed;
    public float bulletAtk;

    private void Update()
    {
        Vector3 next = new Vector3(0, 1 * shootSpeed * Time.deltaTime, 0);
        this.transform.position += next;

        // 카메라를 벗어 났을 때 제거
        if(this.transform.position.y >= 10.5f)
        {
            ObjectPool.instance.ResetForPool(this.gameObject);
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적을 타격했을 때 총알 제거
        if (collision.gameObject.CompareTag("Heart"))
        {
            if(Boss.Instance.boss != null)
            {
                Boss.Instance.boss.GetComponent<HandInfo>().hp -= bulletAtk;
            }
            ObjectPool.instance.ResetForPool(this.gameObject);
        }
    }
    
    private void Explode()
    {
        Destroy(this.gameObject);
    }
}
