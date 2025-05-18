//System
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


//UnityEngine
using UnityEngine;
using UnityEngine.Serialization;

public class Line : MonoBehaviour
{
    [FormerlySerializedAs("attackBar")] public PlayerAttackBar playerAttackBar;
    private const float Speed = 150f;

    private void Update()
    {
        if (!playerAttackBar.isSelect)
        {
            this.transform.localPosition += new Vector3(Speed * Time.deltaTime, 0, 0);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                this.transform.localPosition += new Vector3(0, 0, 0);
                playerAttackBar.SelectedAttack();
            }
            if (this.transform.localPosition.x >= 84f)
            {
                this.transform.localPosition += new Vector3(0, 0, 0);
                playerAttackBar.SelectedAttack();
            }
        }
    }
}
