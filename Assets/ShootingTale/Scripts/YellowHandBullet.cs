using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHandBullet : MonoBehaviour
{
    private Transform target;

    public float rotationSpeed = 1.0f;
    public float lookThreshold = 5.0f; // ���� �Ӱ谪 (����: ��)
    public bool hasLookedAtTarget = false;

    public float speed = 5.0f;

    private void OnEnable()
    {
        target = YellowHandPattern.Instance.target;
    }

    private void Update()
    {
        DetectPlayerAndShot();

        // �÷��̾� �þ߿� ������ ���� ��� ����
        if (transform.position.y <= -16)
        {
            Destroy(this.gameObject);
        }
    }

    public void DetectPlayerAndShot()
    {
        if (target != null && !hasLookedAtTarget)
        {
            // ��� ������ ���
            Vector3 direction = target.position - transform.position;

            direction.z = 0; // 2D������ Z���� ����

            // ���� ���͸� ������ ��ȯ
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ���� ������Ʈ�� ȸ���� ���
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            // �ε巴�� ȸ��
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

            // ���� ������Ʈ�� ���� ���Ϳ� ��� ���� ���� ���� ������ ���
            float currentAngle = Quaternion.Angle(transform.rotation, targetRotation);

            // ������ �Ӱ谪 �����̸� (��, ����� �ٶ󺸰� ������) ���ǹ��� ����
            if (currentAngle <= lookThreshold)
            {
                OnLookAtTarget();
            }
        }

        // �ٶ� �� �̵�
        if (hasLookedAtTarget)
        {
            // ���� ȸ���� �������� ������ �̵��մϴ�.
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    private void OnLookAtTarget()
    {
        Debug.Log(target);
        hasLookedAtTarget = true;
        target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerInfo>().isInvincibility)
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(1);
                UIManager.Instance.playerHpText.text = PlayerInfo.Instance.hp.ToString();
                Destroy(this.gameObject);
            }
        }
    }
}
