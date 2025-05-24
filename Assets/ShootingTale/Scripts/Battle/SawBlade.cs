//System

using System.Collections;
using UnityEngine;
//UnityEngine

public class SawBlade : MonoBehaviour
{
    public float speed = 0.5f;
    public int segmentCount = 20; // Bezier curve를 나눌 segment의 개수
    public float lineWidth = 0.1f; // LineRenderer의 굵기
    private Vector3 bezierPosition;

    private BlueHand bluehand;

    private LineRenderer lineRenderer;
    private float t;

    private void OnEnable()
    {
        bluehand = GameObject.Find("BlueHand").GetComponent<BlueHand>();
        bluehand.IsActive = true;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 1;
        lineRenderer.startWidth = lineWidth; // 시작 굵기 설정
        lineRenderer.endWidth = lineWidth; // 끝 굵기 설정
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        DrawBezierCurve();
        StartCoroutine(BezierCurveStart());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (!collision.GetComponent<PlayerInfo>().isInvincibility)
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(1);
                UIManager.Instance.playerHpText.text = PlayerInfo.Instance.hp.ToString();
            }
    }

    private void DrawBezierCurve()
    {
        var positions = new Vector3[segmentCount + 1];

        for (var i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            positions[i] = CalculateBezierPoint(t, bluehand.wayPoints[0].position, bluehand.wayPoints[1].position,
                bluehand.wayPoints[2].position, bluehand.wayPoints[3].position);
        }

        lineRenderer.SetPositions(positions);
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

    private IEnumerator BezierCurveStart()
    {
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            bezierPosition = CalculateBezierPoint(t, bluehand.wayPoints[0].position, bluehand.wayPoints[1].position,
                bluehand.wayPoints[2].position, bluehand.wayPoints[3].position);
            transform.position = bezierPosition;
            yield return null;
        }

        t = 0f;
        bluehand.IsActive = false;
        Destroy(gameObject);
    }
}