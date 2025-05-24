using UnityEngine;

public class BlueHand : MonoBehaviour
{
    public Transform[] patterns;
    public Transform[] wayPoints;

    public GameObject sawbladePrefab;
    public Transform instantiatePosition;

    public bool IsActive;
    public int segmentCount = 20; // Bezier curve�� ���� segment�� ����

    public GameObject blueHandObj;

    public Transform[] points;

    private Animator animator;
    private Vector2 gizmosPosition;
    private LineRenderer lineRenderer;

    private void Update()
    {
        if (gameObject.GetComponent<HandInfo>().hp <= 0 && gameObject.activeSelf) Die();

        if (!IsActive && GameManager.Instance.progressType == ProgressType.Start)
        {
            Init();
            var obj = Instantiate(sawbladePrefab, instantiatePosition);
            obj.transform.position = instantiatePosition.position;
        }
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null) lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    private void OnDrawGizmos()
    {
        if (wayPoints != null && wayPoints.Length == 4) DrawBezierCurveGizmos();
    }

    private void Init()
    {
        for (var i = 0; i < patterns.Length; i++) patterns[i].gameObject.SetActive(false);

        int random = Random.Range(0, patterns.Length);
        patterns[random].gameObject.SetActive(true);

        wayPoints = new Transform[4];

        for (var i = 0; i < 4; i++) wayPoints[i] = patterns[random].gameObject.transform.GetChild(i);
        DrawBezierCurve();
    }

    private void DrawBezierCurve()
    {
        if (lineRenderer == null || wayPoints == null || wayPoints.Length != 4) return;

        var positions = new Vector3[segmentCount + 1];

        for (var i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            positions[i] = CalculateBezierPoint(t, wayPoints[0].position, wayPoints[1].position, wayPoints[2].position,
                wayPoints[3].position);
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    private void DrawBezierCurveGizmos()
    {
        for (var i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            gizmosPosition = CalculateBezierPoint(t, wayPoints[0].position, wayPoints[1].position,
                wayPoints[2].position, wayPoints[3].position);
            Gizmos.DrawSphere(gizmosPosition, 1f);
        }

        Gizmos.DrawLine(new Vector2(wayPoints[0].position.x, wayPoints[0].position.y),
            new Vector2(wayPoints[1].position.x, wayPoints[1].position.y));

        Gizmos.DrawLine(new Vector2(wayPoints[2].position.x, wayPoints[2].position.y),
            new Vector2(wayPoints[3].position.x, wayPoints[3].position.y));
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        var p = uuu * p0; // (1 - t)^3 * p0
        p += 3 * uu * t * p1; // 3 * (1 - t)^2 * t * p1
        p += 3 * u * tt * p2; // 3 * (1 - t) * t^2 * p2
        p += ttt * p3; // t^3 * p3

        return p;
    }

    public void Die()
    {
        blueHandObj.SetActive(false);
        Boss.Instance.isDie = true;
        gameObject.GetComponent<HandInfo>().hp = 0;
        animator.SetBool("IsDie", true);
    }

    public void EndAnimation()
    {
        LoadingManager.LoadScene("999_Ending", "99_Loading");
        gameObject.SetActive(false);
    }
}