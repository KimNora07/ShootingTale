using UnityEngine;

public class YellowHandBullet : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float lookThreshold = 5.0f;
    public bool hasLookedAtTarget;

    public float speed = 5.0f;
    private Transform target;

    private void Update()
    {
        DetectPlayerAndShot();

        if (transform.position.y <= -16) Destroy(gameObject);
    }

    private void OnEnable()
    {
        target = YellowHandPattern.Instance.target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (!collision.GetComponent<PlayerInfo>().isInvincibility)
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(1);
                //UIManager.Instance.playerHpText.text = PlayerInfo.Instance.hp.ToString();
                Destroy(gameObject);
            }
    }

    public void DetectPlayerAndShot()
    {
        if (target && !hasLookedAtTarget)
        {
            var direction = target.position - transform.position;

            direction.z = 0;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            var targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

            float currentAngle = Quaternion.Angle(transform.rotation, targetRotation);

            if (currentAngle <= lookThreshold) OnLookAtTarget();
        }

        if (hasLookedAtTarget) transform.position += transform.up * (speed * Time.deltaTime);
    }

    private void OnLookAtTarget()
    {
        hasLookedAtTarget = true;
        target = null;
    }
}