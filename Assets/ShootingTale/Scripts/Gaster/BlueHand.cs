using UnityEngine;

public class BlueHand : MonoBehaviour
{
    public Transform[] patterns;
    public Transform[] wayPoints;
    private Vector2 gizmosPosition;

    public GameObject sawbladePrefab;
    public Transform instantiatePosition;

    public bool IsActive = false;

    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();

        Init();
    }

    private void Init()
    {
        for(int i = 0; i < patterns.Length; i++)
        {
            patterns[i].gameObject.SetActive(false);
        }

        int random = Random.Range(0, patterns.Length);
        patterns[random].gameObject.SetActive(true);

        wayPoints = new Transform[4];

        for (int i = 0; i < 4; i++)
        {
            wayPoints[i] = patterns[random].gameObject.transform.GetChild(i);
        }
    }

    private void Update()
    {
        if(this.gameObject.GetComponent<HandInfo>().hp <= 0 && this.gameObject.activeSelf)
        {
            Die();
        }

        if(!IsActive && GameMain.instance.progressType == ProgressType.Start)
        {
            Init();
            GameObject obj = Instantiate(sawbladePrefab, instantiatePosition);
            obj.transform.position = instantiatePosition.position;
        }
    }

    private void OnDrawGizmos()
    {
        if (wayPoints != null && wayPoints.Length == 4)
        {
            for (float t = 0; t < 1; t += 0.05f)
            {
                gizmosPosition =
                    Mathf.Pow(1 - t, 3) * wayPoints[0].position
                    + 3 * t * Mathf.Pow(1 - t, 2) * wayPoints[1].position
                    + 3 * t * (1 - t) * wayPoints[2].position
                    + Mathf.Pow(t, 3) * wayPoints[3].position;

                Gizmos.DrawSphere(gizmosPosition, 1f);
            }

            Gizmos.DrawLine(new Vector2(wayPoints[0].position.x, wayPoints[0].position.y),
                              new Vector2(wayPoints[1].position.x, wayPoints[1].position.y));

            Gizmos.DrawLine(new Vector2(wayPoints[2].position.x, wayPoints[2].position.y),
                              new Vector2(wayPoints[3].position.x, wayPoints[3].position.y));
        }
    }

    public void Die()
    {
        Boss.Instance.isDie = true;
        this.gameObject.GetComponent<HandInfo>().hp = 0;
        animator.SetBool("IsDie", true);
    }

    public void EndAnimation()
    {
        LoadingManager.LoadScene("999_Ending", "99_Loading");
        this.gameObject.SetActive(false);
    }
}

