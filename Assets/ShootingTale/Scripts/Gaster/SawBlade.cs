//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;

public class SawBlade : MonoBehaviour
{

    private BlueHand bluehand;

    public float speed = 0.5f;
    private float t = 0f;
    private Vector3 bezierPoisition;

    private void OnEnable()
    {
        bluehand = GameObject.Find("BlueHand").GetComponent<BlueHand>();
        bluehand.IsActive = true;
    }

    private void Update()
    {
        StartCoroutine(BezierCurveStart());
    }

    private IEnumerator BezierCurveStart()
    {
        t += Time.deltaTime * speed;

        while(t < 1f)
        {
            bezierPoisition = Mathf.Pow(1 - t, 3) * bluehand.wayPoints[0].position
                + 3 * t * Mathf.Pow(1 - t, 2) * bluehand.wayPoints[1].position
                + 3 * t * (1 - t) * bluehand.wayPoints[2].position
                + Mathf.Pow(t, 3) * bluehand.wayPoints[3].position;

            transform.position = bezierPoisition;
            yield return new WaitForEndOfFrame();
        }
        t = 0f;
        bluehand.IsActive = false;
        Destroy(this.gameObject);   
    }
}
