using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuMain : MonoBehaviour
{
    public RectTransform button;

    public List<RectTransform> points;

    private int position = 0;

    public bool isLeft = false;
    public bool isRight = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (button.position == points[position].position && position > 0)
            {
                isLeft = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (button.position == points[position].position && position < points.Count - 1)
            {
                isRight = true;
            }
        }

        if(isLeft)
        {
            //button.position = Vector3.Lerp(button.position, points[position - 1].position, 0.025f);
            button.position = Vector3.MoveTowards(button.position, points[position - 1].position,0.25f);
            if (button.position == points[position - 1].position)
            {
                Debug.Log(position);
                isLeft = false;
                position--;
                Debug.Log(position);
            }
        }

        if (isRight) 
        {
            //button.position = Vector3.Lerp(button.position, points[position + 1].position, 0.025f);
            button.position = Vector3.MoveTowards(button.position, points[position + 1].position, 0.25f);
            if (button.position == points[position + 1].position)
            {    
                Debug.Log(position);
                isRight = false;
                position++;
                Debug.Log(position);
            }
        }
    }
}
