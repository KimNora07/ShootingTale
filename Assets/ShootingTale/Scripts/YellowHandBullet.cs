using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHandBullet : MonoBehaviour
{
    private Transform target;

    public float rotationSpeed = 1.0f;
    public float lookThreshold = 5.0f; // 각도 임계값 (단위: 도)
    public bool hasLookedAtTarget = false;

    public float speed = 5.0f;

    private void OnEnable()
    {
        target = YellowHandPattern.Instance.target;
    }

    private void Update()
    {
        DetectPlayerAndShot();

        // 플레이어 시야에 보이지 않을 경우 제거
        if (transform.position.y <= -16)
        {
            Destroy(this.gameObject);
        }
    }

    public void DetectPlayerAndShot()
    {
        if (target != null && !hasLookedAtTarget)
        {
            // 대상 방향을 계산
            Vector3 direction = target.position - transform.position;

            direction.z = 0; // 2D에서는 Z축을 무시

            // 방향 벡터를 각도로 변환
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 현재 오브젝트의 회전을 계산
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            // 부드럽게 회전
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

            // 현재 오브젝트의 전방 벡터와 대상 방향 벡터 간의 각도를 계산
            float currentAngle = Quaternion.Angle(transform.rotation, targetRotation);

            // 각도가 임계값 이하이면 (즉, 대상을 바라보고 있으면) 조건문을 실행
            if (currentAngle <= lookThreshold)
            {
                OnLookAtTarget();
            }
        }

        // 바라본 후 이동
        if (hasLookedAtTarget)
        {
            // 현재 회전된 방향으로 앞으로 이동합니다.
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
