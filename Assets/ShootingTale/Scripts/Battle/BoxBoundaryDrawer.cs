using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BoxBoundaryDrawer : MonoBehaviour
{
    public Vector2 size = new(6f, 3f); // width, height
    public Vector3 center = new(0f, -2f, 0f);
    
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 5;
        lineRenderer.loop = false;
        lineRenderer.useWorldSpace = true;

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        DrawBox();
    }

    private void DrawBox()
    {
        float halfWidth = size.x * 0.5f;
        float halfHeight = size.y * 0.5f;
        
        Vector3 topLeft = center + new Vector3(-halfWidth, halfHeight, 0);
        Vector3 topRight = center + new Vector3(halfWidth, halfHeight, 0);
        Vector3 bottomRight = center + new Vector3(halfWidth, -halfHeight, 0);
        Vector3 bottomLeft = center + new Vector3(-halfWidth, -halfHeight, 0);

        lineRenderer.SetPositions(new Vector3[]
        {
            topLeft,
            topRight,
            bottomRight,
            bottomLeft,
            topLeft
        });
    }
}
