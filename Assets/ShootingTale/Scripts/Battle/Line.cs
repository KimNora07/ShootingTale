//System


using UnityEngine;
using UnityEngine.Serialization;
//UnityEngine

public class Line : MonoBehaviour
{
    private const float Speed = 150f;
    [FormerlySerializedAs("attackBar")] public PlayerAttackBar playerAttackBar;

    private void Update()
    {
        if (!playerAttackBar.isSelect)
        {
            transform.localPosition += new Vector3(Speed * Time.deltaTime, 0, 0);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                transform.localPosition += new Vector3(0, 0, 0);
                playerAttackBar.SelectedAttack();
            }

            if (transform.localPosition.x >= 84f)
            {
                transform.localPosition += new Vector3(0, 0, 0);
                playerAttackBar.SelectedAttack();
            }
        }
    }
}