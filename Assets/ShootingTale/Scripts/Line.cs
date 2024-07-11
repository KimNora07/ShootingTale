//System
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


//UnityEngine
using UnityEngine;

public class Line : MonoBehaviour
{
    public AttackBar attackBar;
    private float speed = 150f;

    private void Update()
    {
        if (!attackBar.isSelect)
        {
            this.transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                this.transform.localPosition += new Vector3(0, 0, 0);
                attackBar.SelectedAttack();
            }
            if (this.transform.localPosition.x >= 84f)
            {
                this.transform.localPosition += new Vector3(0, 0, 0);
                attackBar.SelectedAttack();
            }
        }

    }
}
